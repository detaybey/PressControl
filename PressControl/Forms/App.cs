
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

            this.graph1.SetBase(this);
            this.graph2.SetBase(this);
        }

        public void SetDataForm(WaveSegment segment)
        {
            graph1.WaveData.AddRange(segment.Data);
            graph1.Changed = true;
            saveMenu.Enabled = true;
            saveAsMenu.Enabled = true;
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
            this.Loop = false;
            this.Playing = true;
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
            graph1.Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Playing = false;
            btnPlay.IsOn = false;
            btnPause.IsOn = false;
            btnPlayLoop.IsOn = false;
            graph1.Stop();
        }

        private void temizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var msg = MessageBox.Show("Emin misiniz?", "Siliniyor?", MessageBoxButtons.YesNo);
            if (msg == System.Windows.Forms.DialogResult.Yes)
            {
                graph1.WaveData.Clear();
                graph1.Changed = false;
                graph1.Refresh();
                saveMenu.Enabled = false;
                saveAsMenu.Enabled = false;
            }
        }

        private void yukleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                graph1.Load(openFileDialog1.FileName);
            }
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = saveFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                graph1.SaveAs(saveFileDialog1.FileName);
            }
        }

        private void cikisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            graph1.Save();
        }


    }

}
