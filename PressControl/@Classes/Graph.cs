﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PressControl
{
    public partial class Graph : UserControl
    {
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

        public DataForm DataForm { get; set; }

        public Timer Timer { get; set; }
        public int X1 = 30;
        public int DataXOffset = 0;
        public int DataYOffset = 0;
        public DateTime startTime;
        public DateTime endTime;
        public TimeSpan ts_timeElapsed;
        public PointF timerPoint;

        public Graph()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

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

            Timer = new Timer();
            Timer.Interval = 30;
            Timer.Tick += Timer_Tick;
            Timer.Start();
            startTime = DateTime.Now;
            timerPoint = new PointF(this.Width - 50, this.Height - 8);
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (this.DataForm == null)
            {
                return;
            }
            DataXOffset = DataXOffset + 1;
            if (DataXOffset > this.Width - 20)
            {
                DataXOffset = 0;
                startTime = DateTime.Now;
            }
            DataYOffset = Convert.ToInt32(this.DataForm.GetValue(DataXOffset));
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
            pe.Graphics.DrawString("-100", MiniFont, Brush, new Point(X1 - 30, Y1));
            pe.Graphics.DrawString("0", MiniFont, Brush, new Point(X1 - 15, Y1 + (H1 / 2) - 7));
            pe.Graphics.DrawString("+100", MiniFont, Brush, new Point(X1 - 28, Y1 + H1 - 14));
            // ticks on timeline
            for (var j = X1; j < X2; j = j + Timer.Interval/5)
            {
                var tickSize = (j % Timer.Interval == 0) ? 9 : 2;
                pe.Graphics.DrawLine(ThinPen, new Point(j, Y1 + (H1 / 2) - tickSize), new Point(j, Y1 + (H1 / 2) + tickSize));
                pe.Graphics.DrawLine(ThinnestPen, new Point(j, Y1), new Point(j, Y1 + H1));
            }

            pe.Graphics.DrawRectangle(BorderPen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));

            if (this.DataForm == null)
            {
                return;
            }

            int x0 = X1;
            int y0 = Y1 + (H1 / 2);
            for (float x = X1; x <= X2; x += 0.25f)
            {
                var y = Y1 + (H1 / 2) + Convert.ToInt32(this.DataForm.GetValue(x - X1));
                //                pe.Graphics.FillRectangle(DataBrush, x, y, 1, 1);
                pe.Graphics.DrawLine(DataPen, x0, y0, x, y);
                x0 = Convert.ToInt16(x);
                y0 = y;
            }

            pe.Graphics.DrawLine(TimePen, DataXOffset + X1, 0, DataXOffset + X1, H1);
            pe.Graphics.FillEllipse(CursorPen, DataXOffset + X1-5, DataYOffset+ (H1/2)-5, 10, 10);

            pe.Graphics.DrawString(ts_timeElapsed.ToString(), MiniFont, Brush, timerPoint);
            pe.Graphics.DrawString(DataYOffset.ToString(), MiniFont, Brush, new PointF(timerPoint.X + 70, timerPoint.Y - 10));

        }
    }


}
