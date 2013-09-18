
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
        public DataForm TestForm { get; set; }
        public NewForm NewForm { get; set; }

        public App()
        {
            InitializeComponent();
            NewForm = new NewForm(this);
        }

        public void SetDataForm(DataForm form)
        {
            graph1.DataForm = form;
            numericUpDown1.Value = form.Interval;
            numericUpDown2.Value = form.Amplitude;
            graph1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
      
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm.ShowDialog(this);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            graph1.DataForm.Interval = Convert.ToInt16(numericUpDown1.Value);
            graph1.Refresh();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            graph1.DataForm.Amplitude = Convert.ToInt16(numericUpDown2.Value);
            graph1.Refresh();
        }
    }

}
