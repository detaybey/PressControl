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

        public bool Changed { get; set; }

        public List<double> WaveData { get; set; }

        public Graph()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            WaveData = new List<double>();

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

            Timer = new SuperTimer();
            Timer.Mode = TimerMode.Periodic;
            Timer.Period = 20;
            Timer.Resolution = 1;
            Timer.SynchronizingObject = this;
            Timer.Tick += new System.EventHandler(this.Timer_Tick);
            startTime = DateTime.Now;
            timerPoint = new PointF(this.Width - 50, this.Height - 8);
            Changed = false;
        }

        public void SetBase(App app)
        {
            this.Base = app;
        }

        public void Load(string name)
        {
            if (name.ToLower().EndsWith(".sgnl"))
            {
                name = name.ToLower().Replace(".sgnl", "");
            }
            this.Name = name;
            using (var stream = new FileStream(name + ".sgnl", FileMode.Open))
            {
                var bf = new BinaryFormatter();
                this.WaveData = (List<double>)bf.Deserialize(stream);
                this.Refresh();
            }
        }

        public void Save()
        {
            SaveAs(this.Name + ".sgnl");
        }

        public void SaveAs(string name)
        {
            if (this.WaveData.Count == 0)
            {
                return;
            }
            using (var stream = new FileStream(name, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(stream, this.WaveData);
            }
        }

        public void Start()
        {
            startTime = DateTime.Now;
            if (this.WaveData.Count == 0)
            {
                return;
            }
            Timer.Start();
        }

        public void Pause()
        {
            Timer.Stop();
        }

        public void Stop()
        {
            this.DataXOffset = 0;
            this.Refresh();
            Timer.Stop();
        }

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
            endTime = DateTime.Now;
            ts_timeElapsed = (endTime - startTime);
            this.Refresh();            
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);
            var X2 = this.Width - 1;
            var Y1 = 1;
            var H1 = this.Height - 2;

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

            if (this.WaveData.Count == 0)
            {
                return;
            }

            double x0 = X1;
            int y0 = Convert.ToInt16(Y1 + (H1 / 2) - this.WaveData[0]);

            foreach (var value in this.WaveData)
            {
                double x = x0 + 1;
                var y = Convert.ToInt16(Y1 + (H1 / 2) - value);
                pe.Graphics.DrawLine(DataPen, Convert.ToInt32(x0), y0, Convert.ToInt32(x), y);
                x0 = Convert.ToInt16(x);
                y0 = y;
            }

            if (this.Base.Playing)
            {
                pe.Graphics.DrawLine(TimePen, Convert.ToInt32(DataXOffset + X1), 0, Convert.ToInt32(DataXOffset + X1), H1);
                pe.Graphics.FillEllipse(CursorPen, Convert.ToInt32(DataXOffset + X1 - 5), (H1 / 2) - 5 - DataYOffset, 10, 10);

                pe.Graphics.DrawString(ts_timeElapsed.ToString(), MiniFont, Brush, timerPoint);
                pe.Graphics.DrawString(DataYOffset.ToString(), MiniFont, Brush, new PointF(timerPoint.X + 70, timerPoint.Y - 10));
            }
        }
    }


}
