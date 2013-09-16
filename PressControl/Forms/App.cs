
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
            graph1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
      
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm.ShowDialog(this);
        }
    }

}
