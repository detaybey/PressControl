using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PressControl
{
    public static class Tools
    {
        public static Point Transform(this Point point, Point origin)
        {
            return new Point(origin.X + point.X, (origin.Y - point.Y));
        }

    }

    public partial class App : Form
    {

        public PulseGraph PulseGraph { get; set; }

        public App()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            PulseGraph = new PulseGraph(this, Type.Triangle, 100, 3);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            PulseGraph.Draw();

        }
    }

    public enum Type
    {
        Square,
        Triangle,
        Sawtooth
    }

    public class PulseGraph
    {
        public const int X1 = 50;
        public const int X2 = 650;
        public const int W1 = X2 - X1;
        public const int Y1 = 40;
        public const int H1 = 200;
        public const int H2 = 200;
        public const int Y2 = Y1 + H1 + 40;

        public Point Origin1 { get; set; }
        public Point Origin2 { get; set; }

        public Graphics g { get; set; }
        public App Base { get; set; }
        public Font Font { get; set; }
        public Font MiniFont { get; set; }
        public Brush Brush { get; set; }
        public Brush BgBrush { get; set; }

        public Pen BorderPen { get; set; }
        public Pen ThinPen { get; set; }
        public Pen DataPen { get; set; }

        public Type Type { get; set; }
        public Int16 Peak { get; set; }
        public Double Seconds { get; set; }

        public List<Point> Points { get; set; }

        private Point P1 { get; set; }
        private Point P2 { get; set; }

        public PulseGraph(App app, Type type, Int16 peak, double seconds)
        {
            this.Base = app;
            BorderPen = new Pen(Color.Gray);
            ThinPen = new Pen(Color.LightGray);
            DataPen = new Pen(Color.DarkOrange);

            Font = new Font("Verdana", 12);
            MiniFont = new Font("Tahoma", 8);
            Brush = new SolidBrush(Color.Black);
            BgBrush = new SolidBrush(Color.White);
            g = Base.CreateGraphics();
            Points = new List<Point>();

            Type = type;
            Peak = peak;
            Seconds = seconds;
            GeneratePoints();
        }

       
        public void Draw()
        {
            g.FillRectangle(BgBrush, new Rectangle(0, 0, 800, 600));

            // TOP BAR
            Origin1 = new Point(X1, Y1 + (H1 / 2));
            // Label
            g.DrawString("İSTENİLEN", Font, Brush, new Point(X1, Y1 - 25));
            // middle line
            g.DrawLine(ThinPen, new Point(X1, Y1 + (H1 / 2)), new Point(X2, Y1 + (H1 / 2)));
            // left labels
            g.DrawString("+100", MiniFont, Brush, new Point(X1 - 35, Y1 - 2));
            g.DrawString("0", MiniFont, Brush, new Point(X1 - 15, Y1 + (H1 / 2) - 7));
            g.DrawString("-100", MiniFont, Brush, new Point(X1 - 30, Y1 + H1 - 12));
            // ticks on timeline
            for (var j = X1; j < X2; j = j + 5)
            {
                var tickSize = (j % 25 == 0) ? 5 : 2;
                g.DrawLine(ThinPen, new Point(j, Y1 + (H1 / 2) - tickSize), new Point(j, Y1 + (H1 / 2) + tickSize));
            }

            P1 = new Point(0,0);
            var pointIndex = 0;
            var repeatX = 0;
            for (var j = X1; j < X2; j = j + 25)
            {
                P2 = Points[pointIndex];
                g.DrawLine(DataPen, new Point(P1.X + repeatX, P1.Y).Transform(Origin1), new Point(P2.X + repeatX, P2.Y).Transform(Origin1));
                P1 = new Point(P2.X, P2.Y);
                pointIndex += 1;
                if (pointIndex == (Points.Count ))
                {
                    pointIndex = 0;
                    repeatX = repeatX + Convert.ToInt16(this.Seconds * 25) * 3;
                }
            }

            // border of graph
            g.DrawRectangle(BorderPen, X1, Y1, W1, H1);

            // TOP BAR
            Origin2 = new Point(X2, Y2 + (H2 / 2));

            // label
            g.DrawString("ÖLÇÜLEN", Font, Brush, new Point(X1, Y2 - 25));
            // middle line
            g.DrawLine(ThinPen, new Point(X1, Y2 + (H2 / 2)), new Point(X2, Y2 + (H2 / 2)));
            // left labels
            g.DrawString("+100", MiniFont, Brush, new Point(X1 - 35, Y2 - 2));
            g.DrawString("0", MiniFont, Brush, new Point(X1 - 15, Y2 + (H2 / 2) - 7));
            g.DrawString("-100", MiniFont, Brush, new Point(X1 - 30, Y2 + H2 - 12));
            // ticks on timeline
            for (var j = X1; j < X2; j = j + 5)
            {
                var tickSize = (j % 25 == 0) ? 5 : 2;
                g.DrawLine(ThinPen, new Point(j, Y2 + (H2 / 2) - tickSize), new Point(j, Y2 + (H2 / 2) + tickSize));
            }
            // border of graph
            g.DrawRectangle(BorderPen, X1, Y2, W1, H2);

           
         

        }


        public void GeneratePoints()
        {
            Points.Clear();

            switch (this.Type)
            {
                case PressControl.Type.Triangle:
                    Points.Add(new Point(Convert.ToInt16(this.Seconds * 25), this.Peak));
                    Points.Add(new Point(Convert.ToInt16(this.Seconds * 75), this.Peak * -1));
                    break;

                case PressControl.Type.Sawtooth:

                    Points.Add(new Point(Convert.ToInt16(this.Seconds * 25), this.Peak));
                    Points.Add(new Point(Convert.ToInt16(this.Seconds * 25), 0));
                    break;

                case PressControl.Type.Square:
                    Points.Add(new Point(Convert.ToInt16(this.Seconds * 0), this.Peak));
                    Points.Add(new Point(Convert.ToInt16(this.Seconds * 25), this.Peak));
                    Points.Add(new Point(Convert.ToInt16(this.Seconds * 50), 0));
                    Points.Add(new Point(Convert.ToInt16(this.Seconds * 75), 0));
                    break;

            }

        }
    }
}
