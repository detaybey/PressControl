
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
        public bool Loop { get; set; }
        public bool Playing { get; set; }


        public NewForm NewForm { get; set; }

        public App()
        {
            InitializeComponent();
            NewForm = new NewForm(this);
 
            this.Playing = false;
            this.Loop = false;
        }

        public void SetDataForm(WaveSegment segment)
        {
            graph1.Waves.Add(segment);
            graph1.WaveData.AddRange(segment.Data);
            graph1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
      
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm.ShowDialog(this);
        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.Playing = true;
            this.Loop = false;
            btnPlay.IsOn = true;
            btnPause.IsOn = false;
            btnPlayLoop.IsOn = false;
            graph1.Start();
        }

        private void btnPlayLoop_Click(object sender, EventArgs e)
        {
            this.Playing = true;
            this.Loop = true;
            btnPlay.IsOn = false;
            btnPause.IsOn = false;
            btnPlayLoop.IsOn = true;
            graph1.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this.Playing = false;
            btnPlay.IsOn = false;
            btnPause.IsOn = true;
            btnPlayLoop.IsOn = false;
            graph1.Stop();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Playing = false;
            btnPlay.IsOn = false;
            btnPause.IsOn = false;
            btnPlayLoop.IsOn = false;
            graph1.Stop();
        }
    }

}
