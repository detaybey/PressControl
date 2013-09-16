using System;
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
        public Point Origin { get; set; }      
        public App Base { get; set; }
        public Font MiniFont { get; set; }
        public Brush Brush { get; set; }
        public Brush BgBrush { get; set; }

        public Pen BorderPen { get; set; }
        public Pen ThinPen { get; set; }
        public Pen DataPen { get; set; }
        public Pen ThinnestPen { get; set; }

        public DataForm DataForm { get; set; }
  
        public Graph()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            BorderPen = new Pen(Color.Gray);
            ThinPen = new Pen(Color.LightGray);
            ThinnestPen = new Pen(Color.FromArgb(100, 210, 210, 210));
            DataPen = new Pen(Color.DarkOrange, 2f);
            MiniFont = new Font("Tahoma", 8);
            Brush = new SolidBrush(Color.Black);
            BgBrush = new SolidBrush(Color.White);
      
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);
            var X1 = 30;
            var X2 = this.Width - 1;
            var Y1 = 1;
            var H1 = this.Height-2;

            pe.Graphics.FillRectangle(BgBrush, new Rectangle(0, 0, this.Width, this.Height));

            // middle line
            pe.Graphics.DrawLine(ThinPen, new Point(X1, Y1 + (H1 / 2)), new Point(X2, Y1 + (H1 / 2)));
            // left labels
            pe.Graphics.DrawString("+100", MiniFont, Brush, new Point(X1 - 30, Y1));
            pe.Graphics.DrawString("0", MiniFont, Brush, new Point(X1 - 15, Y1 + (H1 / 2) - 7));
            pe.Graphics.DrawString("-100", MiniFont, Brush, new Point(X1 - 28, Y1 + H1 - 14));
            // ticks on timeline
            for (var j = X1; j < X2; j = j + 5)
            {
                var tickSize = (j % 25 == 0) ? 5 : 2;
                pe.Graphics.DrawLine(ThinPen, new Point(j, Y1 + (H1 / 2) - tickSize), new Point(j, Y1 + (H1 / 2) + tickSize));
                pe.Graphics.DrawLine(ThinnestPen, new Point(j, Y1), new Point(j, Y1 + H1));
            }

            pe.Graphics.DrawRectangle(BorderPen, new Rectangle(0, 0, this.Width-1, this.Height-1));

            if (this.DataForm == null)
            {
                return;
            }
         
            var prev = new Point(X1, H1 / 2);
            for (var x = X1; x < X2; x += 1)
            {
                var y = Convert.ToInt32(this.DataForm.GetValue(x));
                pe.Graphics.DrawLine(DataPen, prev, new Point(x, y));
                prev = new Point(x, y);
            }
        }
    }


}
