
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
  
    public partial class App : Form
    {

       

        public App()
        {
            InitializeComponent();

             

        }

        private void button1_Click(object sender, EventArgs e)
        {

         
        
        }
    }

 



        //public void GeneratePoints()
        //{
        //    Points.Clear();

        //    switch (this.Type)
        //    {
        //        case PressControl.Type.Triangle:
        //            Points.Add(new Point(Convert.ToInt16(this.Seconds * 25), this.Peak));
        //            Points.Add(new Point(Convert.ToInt16(this.Seconds * 75), this.Peak * -1));
        //            break;

        //        case PressControl.Type.Sawtooth:

        //            Points.Add(new Point(Convert.ToInt16(this.Seconds * 25), this.Peak));
        //            Points.Add(new Point(Convert.ToInt16(this.Seconds * 25), 0));
        //            break;

        //        case PressControl.Type.Square:
        //            Points.Add(new Point(Convert.ToInt16(this.Seconds * 0), this.Peak));
        //            Points.Add(new Point(Convert.ToInt16(this.Seconds * 25), this.Peak));
        //            Points.Add(new Point(Convert.ToInt16(this.Seconds * 50), 0));
        //            Points.Add(new Point(Convert.ToInt16(this.Seconds * 75), 0));
        //            break;

        //    }

        //}
}
