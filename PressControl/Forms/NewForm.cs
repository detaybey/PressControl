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

        private void toggleButton1_Click(object sender, EventArgs e)
        {
            toggleButton1.IsOn = true;
            toggleButton2.IsOn = false;
            toggleButton3.IsOn = false;
        }

        private void toggleButton2_Click(object sender, EventArgs e)
        {
            toggleButton1.IsOn = false;
            toggleButton2.IsOn = true;
            toggleButton3.IsOn = false;
        }

        private void toggleButton3_Click(object sender, EventArgs e)
        {
            toggleButton1.IsOn = false;
            toggleButton2.IsOn = false;
            toggleButton3.IsOn = true;
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
            var dataform = new DataForm()
            {
                Frequency = Convert.ToInt16(numericUpDown1.Value),
                Interval = Convert.ToInt16(numericUpDown2.Value),
            };
            if (toggleButton1.IsOn)
            {
                dataform.Type = SignalType.Sawtooth;
            }
            if (toggleButton2.IsOn)
            {
                dataform.Type = SignalType.Triangle;
            }
            if (toggleButton3.IsOn)
            {
                dataform.Type = SignalType.Square;
            }
            dataform.Type = SignalType.Sine;
            App.SetDataForm(dataform);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 
    }
}
