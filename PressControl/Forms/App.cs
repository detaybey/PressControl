
using PressControl.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
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
        public string AppName = "UMPC v1.0 ";
        public NewForm NewForm { get; set; }
        public Timer ConnectionTimer { get; set; }

        public SuperTimer TestFeedTimer { get; set; }

        public App()
        {
            InitializeComponent();
            NewForm = new NewForm(this);

            SetAvailableComPorts();

            this.Playing = false;
            this.Loop = false;

            this.graph1.SetBase(this, false);
            this.graph2.SetBase(this, true);

            ConnectionTimer = new Timer();
            ConnectionTimer.Tick += ConnectionTimer_Tick;
            ConnectionTimer.Interval = 1000 * 1;
            ConnectionTimer.Start();

            TestFeedTimer = new SuperTimer();
            TestFeedTimer.Mode = TimerMode.Periodic;
            TestFeedTimer.Resolution = 1;
            TestFeedTimer.SynchronizingObject = this;
            TestFeedTimer.Tick += new System.EventHandler(TestFeedTimer_Tick);
            TestFeedTimer.Period = 20;
            TestFeedTimer.Start();

            DataPort.BaudRate = 57600;
            //for (var j = 0; j < 100; j++)
            //{
            //    graph2.AddData(j);
            //}
        }

        void TestFeedTimer_Tick(object sender, EventArgs e)
        {
            if (this.Playing)
            {                
                var rnd = new Random();    
                var value = -100 + rnd.Next(200);
                var data = Convert.ToInt32(value);
                RelayData(data);
            }
        }

        void ConnectionTimer_Tick(object sender, EventArgs e)
        {
            PrintConnectionStatus();
        }

        public void SetDataForm(WaveSegment segment)
        {
            graph1.WaveData.AddRange(segment.Data);
            graph1.Changed = true;
            saveMenu.Enabled = true;
            saveAsMenu.Enabled = true;
            graph1.Refresh();
        }

        public void RelayData(int data)
        {
            graph2.AddData(data, true);
            graph2.Refresh();
        }

        public void PrintConnectionStatus()
        {
            if (DataPort.IsOpen)
            {
                ConnectionStatus.Text = "Bağlı";
            }
            else
            {
                ConnectionStatus.Text = "-";
            }
        }

        public void SetAvailableComPorts()
        {
            comPortList.Items.Clear();
            var ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                comPortList.Items.Add(port);
            }
            if (ports.Count() == 1)
            {
                comPortList.SelectedIndex = 0;
            }
            DataPort.DataReceived += DataPort_DataReceived;
        }

        void DataPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            var header = port.ReadByte();
            var data = port.ReadByte();
            var finish = port.ReadByte();

            incomingData.Text = data.ToString() + "(" + graph2.WaveData.Count() + ")";
            graph2.AddData(data - 110);
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
                this.Text = this.AppName;
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
            if (graph1.Changed)
            {
                var result = MessageBox.Show("Sinyal yapısında değişiklik var ve kaydetmediniz. Çıkmak istediğinizden emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    return;
                }
            }
            Environment.Exit(0);
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            graph1.Save();
        }

        private void hakkindaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new AboutBox();
            box.Show();
        }


        private void comPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataPort.PortName = comPortList.Text;
                DataPort.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Port kullanımda veya bir problem var");
            }
        }




    }

}
