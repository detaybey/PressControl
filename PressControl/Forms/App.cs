
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
        public string AppName = "UMPC v1.1 ";
        public NewForm NewForm { get; set; }
        public Timer ConnectionTimer { get; set; }
        public SuperTimer ReadTimer { get; set; }
        public List<int> ReadBuffer { get; set; }

        private int LastReadValue = 0;
        private int LastReadValue2 = 0;
        private int LastReadValue3 = 0;
        private int LastReadValue4 = 0;

        private int LastXOffset = 0;

        public App()
        {
            InitializeComponent();
            NewForm = new NewForm(this);

            SetAvailableComPorts();

            this.ReadBuffer = new List<int>();
            this.Playing = false;
            this.Loop = false;

            this.graph1.SetBase(this);

            ConnectionTimer = new Timer();
            ConnectionTimer.Tick += ConnectionTimer_Tick;
            ConnectionTimer.Interval = 1000 * 1;
            ConnectionTimer.Start();

            DataPort.BaudRate = 57600;

            ReadTimer = new SuperTimer();
            ReadTimer.Mode = TimerMode.Periodic;
            ReadTimer.Tick += ReadTimer_Tick;
            ReadTimer.Period = 20;
            ReadTimer.Resolution = 1;
            ReadTimer.SynchronizingObject = this.graph1;

        }

        void ReadTimer_Tick(object sender, EventArgs e)
        {
            if (graph1.ReadData.Count > graph1.DataXOffset)
            {
                for (var j = LastXOffset; j <= graph1.DataXOffset;j++)
                {
                    graph1.ReadData[j] = LastReadValue;
                }
                LastXOffset = Convert.ToInt16(graph1.DataXOffset);
            }
        }


        void ConnectionTimer_Tick(object sender, EventArgs e)
        {
            PrintConnectionStatus();
        }

        public void SetDataForm(WaveSegment segment)
        {
            graph1.WaveData.AddRange(segment.Data);
            foreach (var i in segment.Data)
            {
                graph1.ReadData.Add(0);
            }
            graph1.Changed = true;
            saveMenu.Enabled = true;
            saveAsMenu.Enabled = true;
            graph1.Refresh();
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
            while (port.BytesToRead > 0)
            {
                LastReadValue2 = port.ReadByte() - 110;

                if (LastReadValue2 == LastReadValue3 && LastReadValue2 == LastReadValue4)
                {
                    LastReadValue = LastReadValue3;
                }
                LastReadValue4 = LastReadValue3;
                LastReadValue3 = LastReadValue2;

                //if (LastReadValue != 5)
                //{
                //    throw new Exception("5 degil!");
                //}
                incomingData.Text = LastReadValue.ToString();

                //this.ReadBuffer.Add(port.ReadByte());
                //if (this.ReadBuffer.Count == 3)
                //{
                //    if (this.ReadBuffer[0] == 0 && this.ReadBuffer[2] == 255)
                //    {
                //        var value = this.ReadBuffer[1] - 110;
                //        incomingData.Text = value.ToString() + "(" + graph2.WaveData.Count() + ")";
                //        graph2.AddData(value);
                //        this.ReadBuffer.Clear();
                //    }
                //}
            }
            try
            {
            }
            catch (Exception)
            {
                LastReadValue = 0;
            }
        }


        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm.ShowDialog(this);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (this.graph1.WaveData.Count() > 0)
            {
                this.Loop = false;
                this.Playing = true;
                btnPlay.IsOn = true;
                btnPause.IsOn = false;
                btnPlayLoop.IsOn = false;
                graph1.Start();
                ReadTimer.Start();
            }
        }

        private void btnPlayLoop_Click(object sender, EventArgs e)
        {
            if (this.graph1.WaveData.Count() > 0)
            {
                this.Playing = true;
                this.Loop = true;
                btnPlay.IsOn = false;
                btnPause.IsOn = false;
                btnPlayLoop.IsOn = true;
                graph1.Start();
                ReadTimer.Start();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this.Playing = false;
            btnPlay.IsOn = false;
            btnPause.IsOn = true;
            btnPlayLoop.IsOn = false;
            graph1.Pause();
            ReadTimer.Stop();
            SendStopData();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Playing = false;
            btnPlay.IsOn = false;
            btnPause.IsOn = false;
            btnPlayLoop.IsOn = false;
            graph1.Stop();
            ReadTimer.Stop();
            SendStopData();
        }


        private void SendStopData()
        {
            if (DataPort.IsOpen)
            {
                for (var j = 0; j < 10; j++)
                {
                    graph1.DataBuffer[0] = 0;
                    graph1.DataBuffer[1] = 0;
                    graph1.DataBuffer[2] = 1;
                    graph1.DataBuffer[3] = (byte)(manuelBasinc.Value + 110);
                    graph1.DataBuffer[4] = 255;
                    DataPort.Write(graph1.DataBuffer, 0, 5);
                }
            }
        }

        private void temizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var msg = MessageBox.Show("Emin misiniz?", "Siliniyor?", MessageBoxButtons.YesNo);
            if (msg == System.Windows.Forms.DialogResult.Yes)
            {
                //graph2.WaveData.Clear();
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
                //graph2.WaveData.Clear();
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
                DataPort.BaudRate = 57600;
                DataPort.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Port kullanımda veya bir problem var");
            }
        }




    }

}
