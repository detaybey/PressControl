using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PressControl
{
    public partial class Graph : UserControl
    {
        public const int OUTERWIDTH = 96;
        private int ScrollOffset = 0;
        public string Name { get; set; }
        public App Base { get; set; }
        public Font MiniFont { get; set; }
        public Brush Brush { get; set; }
        public Brush BgBrush { get; set; }
        public Brush DataBrush { get; set; }
        public Pen BorderPen { get; set; }
        public Pen ThinPen { get; set; }
        public Pen DataPen { get; set; }
        public Pen ReadPen1 { get; set; }
        public Pen ReadPen2 { get; set; }
        public Pen ThinnestPen { get; set; }
        public Pen TimePen { get; set; }
        public Brush CursorPen { get; set; }
        public Brush SecondsBrush { get; set; }
        public SuperTimer Timer { get; set; }

        public int X1 = 30;
        public double DataXOffset = 0;
        public int DataYOffset = 0;
        public DateTime startTime;
        public DateTime endTime;
        public TimeSpan ts_timeElapsed;
        public PointF timerPoint;
        public byte[] DataBuffer;
        public bool Changed { get; set; }
        public List<double> WaveData { get; set; }
        public List<double> ReadData { get; set; }

        public Stream Logstream { get; set; }

        /// <summary>
        /// Initialize graph component.
        /// </summary>
        public Graph()
        {

            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // set arrays
            DataBuffer = new byte[5];
            WaveData = new List<double>();
            ReadData = new List<double>();

            // visual styling
            BorderPen = new Pen(Color.Gray);
            ThinPen = new Pen(Color.LightGray);
            ThinnestPen = new Pen(Color.FromArgb(100, 210, 210, 210));
            DataPen = new Pen(Color.LightGreen, 3f);
            MiniFont = new Font("Tahoma", 8);
            Brush = new SolidBrush(Color.Black);
            SecondsBrush = new SolidBrush(Color.Gray);
            BgBrush = new SolidBrush(Color.White);
            DataBrush = new SolidBrush(Color.LightGreen);
            ReadPen1 = new Pen(Color.Magenta, 2);
            ReadPen2 = new Pen(Color.Lime, 1);

            TimePen = new Pen(Color.Red);
            CursorPen = new SolidBrush(Color.Orange);

            // timer for sending data over time (every 20ms)

            Timer = new SuperTimer();
            Timer.Mode = TimerMode.Periodic;
            Timer.Period = 20;   //         50 times a second , 1000/50
            Timer.Resolution = 1;
            Timer.SynchronizingObject = this;
            Timer.Tick += new System.EventHandler(this.Timer_Tick);
            startTime = DateTime.Now;
            timerPoint = new PointF(this.Width - 50, this.Height - 8);
            Changed = false;

        }

        // sets the link between the app and the graph component
        public void SetBase(App app)
        {
            this.Base = app;
        }


        public void OpenLog()
        {
            if (Logstream.CanWrite)
            {
                Logstream.Flush();
            }
            Logstream = new FileStream(this.Name + ".log", FileMode.OpenOrCreate);

        }
        // loads a saved signal data
        public void Load(string name)
        {
            this.Name = System.IO.Path.GetFileName(name);
            this.Base.Text = this.Base.AppName + this.Name;
            using (var stream = new FileStream(name, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                this.WaveData = (List<double>)bf.Deserialize(stream);
                this.ReadData = new List<double>();
                foreach (var i in this.WaveData)
                {
                    this.ReadData.Add(0);
                }
                this.Refresh();
            }
        }

        // saves the signal data
        public void Save()
        {
            SaveAs(this.Name);
        }

        // saves the signal data with a different name
        public void SaveAs(string name)
        {
            if (this.WaveData.Count == 0)
            {
                return;
            }

            this.Name = System.IO.Path.GetFileName(name);
            this.Base.Text = this.Base.AppName + this.Name;

            using (var stream = new FileStream(name, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(stream, this.WaveData);
            }
        }

        // starts sending the signal 
        public void Start()
        {
            startTime = DateTime.Now;
            if (this.WaveData.Count == 0)
            {
                return;
            }
            Timer.Start();
        }

        // pauses sending the signal
        public void Pause()
        {
            Timer.Stop();
        }

        // stops the signal
        public void Stop()
        {
            this.DataXOffset = 0;
            this.Refresh();
            Timer.Stop();
        }

        /// <summary>
        /// run every 20ms and send the data to port (if it's open)
        /// </summary>
        void Timer_Tick(object sender, EventArgs e)
        {

            if (this.WaveData.Count == 0)
            {
                return;
            }
            var maxX = this.WaveData.Count;

            DataXOffset = DataXOffset + 1;
            if (DataXOffset >= maxX)
            {
                DataXOffset = 0;
                startTime = DateTime.Now;
                if (!Base.Loop)
                {
                    Stop();
                    this.Base.Playing = false;
                }
            }
            DataYOffset = Convert.ToInt32(this.WaveData[Convert.ToInt32(DataXOffset)]);

            // send the current data to port if the port is available
            if (this.Base.DataPort.IsOpen)
            {
                DataBuffer[0] = 0;
                DataBuffer[1] = (byte)(DataYOffset + 110);
                DataBuffer[2] = (byte)(this.Base.Playing == true ? 2 : 1);
                DataBuffer[3] = (byte)(this.Base.manuelBasinc.Value + 110);
                DataBuffer[4] = 255;
                this.Base.DataPort.Write(DataBuffer, 0, 5);
                //this.Base.RelayData(DataYOffset);
            }
            endTime = DateTime.Now;
            ts_timeElapsed = (endTime - startTime);
            this.Refresh();

        }

        /// <summary>
        /// canvas refresh event. all the bar-painting occurs here.
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {

            // Calling the base class OnPaint
            base.OnPaint(pe);
            var X2 = this.Width - 1;
            var Y1 = 1;
            var H1 = this.Height - 2;

            if (this.WaveData.Count > this.Width - X1)
            {
                if (this.Base.Playing)
                {
                    if (this.DataXOffset <= (this.Width / 2))
                    {
                        scrollBarEx1.Value = 0;
                    }

                    if (this.DataXOffset > (this.Width / 2))
                    {
                        scrollBarEx1.Value = Convert.ToInt16(this.DataXOffset) - (this.Width / 2);
                    }

                    if (this.DataXOffset > (this.WaveData.Count - this.Width))
                    {
                        scrollBarEx1.Value = this.WaveData.Count;
                    }
                }
                scrollBarEx1.Left = 30;
                scrollBarEx1.Width = this.Width - 32;
                scrollBarEx1.Top = this.Height - 20;
                scrollBarEx1.Visible = true;
                scrollBarEx1.Minimum = 0;
                scrollBarEx1.Maximum = this.WaveData.Count - this.Width - 1;
    
            }
            else
            {
                scrollBarEx1.Visible = false;
            }

            pe.Graphics.FillRectangle(BgBrush, new Rectangle(0, 0, this.Width, this.Height));

            // middle line
            pe.Graphics.DrawLine(ThinPen, new Point(X1, Y1 + (H1 / 2)), new Point(X2, Y1 + (H1 / 2)));
            // left labels
            pe.Graphics.DrawString("+100", MiniFont, Brush, new Point(X1 - 30, Y1));
            //            pe.Graphics.DrawString("0", MiniFont, Brush, new Point(X1 - 15, Y1 + (H1 / 2) - 7));
            pe.Graphics.DrawString("-100", MiniFont, Brush, new Point(X1 - 28, Y1 + H1 - 14));
            // ticks on timeline
            for (var j = X1; j < X2 + 51; j = j + 50)
            {
                var Xoffset = (ScrollOffset % 50);

                //var tickSize = (j % (500 / Timer.Interval) == 0) ? 9 : 2;
                if (j - Xoffset >= X1)
                {
                    pe.Graphics.DrawLine(ThinPen, new Point(j - Xoffset, Y1 + (H1 / 2) - 9), new Point(j - Xoffset, Y1 + (H1 / 2) + 9));
                    pe.Graphics.DrawLine(ThinnestPen, new Point(j - Xoffset, Y1), new Point(j - Xoffset, Y1 + H1));
                    if ((j - X1) % 250 == 0)
                    {
                        var secs = ((j - X1) / 50) + (ScrollOffset / 250) * 5;
                        pe.Graphics.DrawString(secs.ToString(), MiniFont, SecondsBrush, new Point(j - (ScrollOffset % 250), Y1 + (H1 / 2)));
                    }
                }
            }

            pe.Graphics.DrawRectangle(BorderPen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));

            // if there is no wave data, return.
            if (this.WaveData.Count == 0)
            {
                return;
            }

            double x0 = X1;
            int y0 = Convert.ToInt16(Y1 + (H1 / 2) - this.WaveData[0 + ScrollOffset]);
            var ry0 = Convert.ToInt16(Y1 + (H1 / 2) - this.ReadData[0 + ScrollOffset]);

            int y;
            short ry;
            for (var j = 0; j < this.Width - 1; j++)
            {
                if ((j + ScrollOffset) >= this.WaveData.Count)
                {
                    break;
                }
                double x = x0 + 1;
                y = Convert.ToInt16(Y1 + (H1 / 2) - this.WaveData[j + ScrollOffset]);
                ry = Convert.ToInt16(Y1 + (H1 / 2) - this.ReadData[j + ScrollOffset]);
                pe.Graphics.DrawLine(DataPen, Convert.ToInt32(x0), y0, Convert.ToInt32(x), y);
                pe.Graphics.DrawLine(ReadPen1, Convert.ToInt32(x0), ry0, Convert.ToInt32(x), ry);

                x0 = Convert.ToInt16(x);
                y0 = y;
                ry0 = ry;
            }

            if (this.Base.Playing)
            {
                pe.Graphics.DrawLine(TimePen, Convert.ToInt32(DataXOffset + X1 - ScrollOffset), 0, Convert.ToInt32(DataXOffset + X1 - ScrollOffset), H1);
                pe.Graphics.FillEllipse(CursorPen, Convert.ToInt32(DataXOffset + X1 - 5 - ScrollOffset), (H1 / 2) - 5 - DataYOffset, 10, 10);

                this.Base.WaveTime.Text = ts_timeElapsed.ToString();
                this.Base.WaveValue.Text = DataYOffset.ToString();
            }

        }

        public void AddData(double data, bool resetOnMax = true)
        {
            this.WaveData.Add(data);
            if (resetOnMax)
            {
                if (this.WaveData.Count > this.Width - X1)
                {
                    this.WaveData.Clear();
                }
            }
            this.Invoke((MethodInvoker)delegate
            {
                this.Refresh();
            });
        }


        private void scrollBarEx1_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollOffset = scrollBarEx1.Value;
            this.Refresh();
        }
    }


}
