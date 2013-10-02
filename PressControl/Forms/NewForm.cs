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
    public partial class NewForm : Form
    {
        public App App { get; set; }

        public NewForm(App app)
        {
            this.App = app;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = Convert.ToInt32(numericUpDown1.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            numericUpDown2.Value = trackBar2.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar2.Value = Convert.ToInt32(numericUpDown2.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var segment = new WaveSegment(trackBar2.Value, trackBar3.Value, trackBar1.Value);
            this.App.SetDataForm(segment);            
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            numericUpDown3.Value = trackBar3.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            trackBar3.Value = Convert.ToInt32(numericUpDown3.Value);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
 
    }
}
