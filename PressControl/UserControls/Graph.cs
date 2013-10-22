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
        public Pen ThinnestPen { get; set; }
        public Pen TimePen { get; set; }
        public Brush CursorPen { get; set; }
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
        public bool ReadOnly { get; set; }

        public List<double> WaveData { get; set; }

        /// <summary>
        /// Initialize graph component.
        /// </summary>
        public Graph()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // set arrays
            DataBuffer = new byte[4];
            WaveData = new List<double>();

            // visual styling
            BorderPen = new Pen(Color.Gray);
            ThinPen = new Pen(Color.LightGray);
            ThinnestPen = new Pen(Color.FromArgb(100, 210, 210, 210));
            DataPen = new Pen(Color.LightGreen, 2f);
            MiniFont = new Font("Tahoma", 8);
            Brush = new SolidBrush(Color.Black);
            BgBrush = new SolidBrush(Color.White);
            DataBrush = new SolidBrush(Color.LightGreen);
            TimePen = new Pen(Color.Red);
            CursorPen = new SolidBrush(Color.Orange);

            // timer for sending data over time (every 20ms)
            if (!this.ReadOnly)
            {
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
        }

        // sets the link between the app and the graph component
        public void SetBase(App app, bool isReadOnly)
        {
            this.Base = app;
            this.ReadOnly = isReadOnly;
        }

        // loads a saved signal data
        public void Load(string name)
        {
            if (!this.ReadOnly)
            {
                this.Name = System.IO.Path.GetFileName(name);
                this.Base.Text = this.Base.AppName + this.Name;
                using (var stream = new FileStream(name, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    this.WaveData = (List<double>)bf.Deserialize(stream);
                    this.Refresh();
                }
            }
        }

        // saves the signal data
        public void Save()
        {
            if (!this.ReadOnly)
            {
                SaveAs(this.Name);
            }
        }

        // saves the signal data with a different name
        public void SaveAs(string name)
        {
            if (!this.ReadOnly)
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
        }

        // starts sending the signal 
        public void Start()
        {
            if (!this.ReadOnly)
            {
                startTime = DateTime.Now;
                if (this.WaveData.Count == 0)
                {
                    return;
                }
                Timer.Start();
            }
        }

        // pauses sending the signal
        public void Pause()
        {
            if (!this.ReadOnly)
            {
                Timer.Stop();
            }
        }

        // stops the signal
        public void Stop()
        {
            if (!this.ReadOnly)
            {
                this.DataXOffset = 0;
                this.Refresh();
                Timer.Stop();
            }
        }

        /// <summary>
        /// run every 20ms and send the data to port (if it's open)
        /// </summary>
        void Timer_Tick(object sender, EventArgs e)
        {
            if (!this.ReadOnly)
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
                    DataBuffer[3] = 255;
                    this.Base.DataPort.Write(DataBuffer, 0, 4);
                    //this.Base.RelayData(DataYOffset);
                }
                endTime = DateTime.Now;
                ts_timeElapsed = (endTime - startTime);
                this.Refresh();
            }
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

            if (this.WaveData.Count > this.Width - OUTERWIDTH)
            {
                scrollBar.Left = 1;
                scrollBar.Width = this.Width - 2;
                scrollBar.Top = this.Height - 20;
                scrollBar.Visible = true;
                scrollBar.Minimum = 0;
                scrollBar.Maximum = this.Width;
                scrollBar.LargeChange = this.WaveData.Count - this.Width - 1;
            }
            else
            {
                scrollBar.Visible = false;
            }

            pe.Graphics.FillRectangle(BgBrush, new Rectangle(0, 0, this.Width, this.Height));

            // middle line
            pe.Graphics.DrawLine(ThinPen, new Point(X1, Y1 + (H1 / 2)), new Point(X2, Y1 + (H1 / 2)));
            // left labels
            pe.Graphics.DrawString("+100", MiniFont, Brush, new Point(X1 - 30, Y1));
            pe.Graphics.DrawString("0", MiniFont, Brush, new Point(X1 - 15, Y1 + (H1 / 2) - 7));
            pe.Graphics.DrawString("-100", MiniFont, Brush, new Point(X1 - 28, Y1 + H1 - 14));
            // ticks on timeline
            for (var j = X1; j < X2; j = j + 50)
            {
                //var tickSize = (j % (500 / Timer.Interval) == 0) ? 9 : 2;
                pe.Graphics.DrawLine(ThinPen, new Point(j, Y1 + (H1 / 2) - 9), new Point(j, Y1 + (H1 / 2) + 9));
                pe.Graphics.DrawLine(ThinnestPen, new Point(j, Y1), new Point(j, Y1 + H1));
            }

            pe.Graphics.DrawRectangle(BorderPen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));

            // if there is no wave data, return.
            if (this.WaveData.Count == 0)
            {
                return;
            }

            double x0 = X1;
            int y0 = Convert.ToInt16(Y1 + (H1 / 2) - this.WaveData[0]);
            for (var j = 0; j < this.Width - 1; j++)
            {
                if ((j + ScrollOffset) >= this.WaveData.Count)
                {
                    break;
                }
                var value = this.WaveData[j + ScrollOffset];
                double x = x0 + 1;
                var y = Convert.ToInt16(Y1 + (H1 / 2) - value);
                pe.Graphics.DrawLine(DataPen, Convert.ToInt32(x0), y0, Convert.ToInt32(x), y);
                x0 = Convert.ToInt16(x);
                y0 = y;
            }

            if (!this.ReadOnly)
            {
                if (this.Base.Playing)
                {
                    pe.Graphics.DrawLine(TimePen, Convert.ToInt32(DataXOffset + X1), 0, Convert.ToInt32(DataXOffset + X1), H1);
                    pe.Graphics.FillEllipse(CursorPen, Convert.ToInt32(DataXOffset + X1 - 5), (H1 / 2) - 5 - DataYOffset, 10, 10);

                    this.Base.WaveTime.Text = ts_timeElapsed.ToString();
                    this.Base.WaveValue.Text = DataYOffset.ToString();
                }
            }
        }

        public void AddData(double data, bool resetOnMax = true)
        {
           this.WaveData.Add(data);
            if (resetOnMax)
            {
                if (this.WaveData.Count > this.Width)
                {
                    this.WaveData.Clear();
                }
            }
        }

        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollOffset = scrollBar.Value;
            this.Refresh();
        }
    }


}
