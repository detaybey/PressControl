namespace PressControl
{
    partial class App
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dosyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temizleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yeniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yukleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cikisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yardimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hakkindaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonList = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnStop = new PlayButton.PlayButton();
            this.btnPause = new PlayButton.PlayButton();
            this.btnPlayLoop = new PlayButton.PlayButton();
            this.btnPlay = new PlayButton.PlayButton();
            this.graph2 = new PressControl.Graph();
            this.graph1 = new PressControl.Graph();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ISTENILEN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "OLCULEN";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Teal;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyaToolStripMenuItem,
            this.yardimToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1011, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.temizleToolStripMenuItem,
            this.yeniToolStripMenuItem,
            this.yukleToolStripMenuItem,
            this.saveAsMenu,
            this.saveMenu,
            this.cikisToolStripMenuItem});
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.dosyaToolStripMenuItem.Text = "Dosya";
            // 
            // temizleToolStripMenuItem
            // 
            this.temizleToolStripMenuItem.Name = "temizleToolStripMenuItem";
            this.temizleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.temizleToolStripMenuItem.Text = "Temizle";
            this.temizleToolStripMenuItem.Click += new System.EventHandler(this.temizleToolStripMenuItem_Click);
            // 
            // yeniToolStripMenuItem
            // 
            this.yeniToolStripMenuItem.Name = "yeniToolStripMenuItem";
            this.yeniToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.yeniToolStripMenuItem.Text = "Yeni";
            this.yeniToolStripMenuItem.Click += new System.EventHandler(this.yeniToolStripMenuItem_Click);
            // 
            // yukleToolStripMenuItem
            // 
            this.yukleToolStripMenuItem.Name = "yukleToolStripMenuItem";
            this.yukleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.yukleToolStripMenuItem.Text = "Yükle";
            this.yukleToolStripMenuItem.Click += new System.EventHandler(this.yukleToolStripMenuItem_Click);
            // 
            // saveAsMenu
            // 
            this.saveAsMenu.Enabled = false;
            this.saveAsMenu.Name = "saveAsMenu";
            this.saveAsMenu.Size = new System.Drawing.Size(152, 22);
            this.saveAsMenu.Text = "Farklı Kaydet";
            this.saveAsMenu.Click += new System.EventHandler(this.kaydetToolStripMenuItem_Click);
            // 
            // saveMenu
            // 
            this.saveMenu.Enabled = false;
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.Size = new System.Drawing.Size(152, 22);
            this.saveMenu.Text = "Kaydet";
            this.saveMenu.Click += new System.EventHandler(this.saveMenu_Click);
            // 
            // cikisToolStripMenuItem
            // 
            this.cikisToolStripMenuItem.Name = "cikisToolStripMenuItem";
            this.cikisToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cikisToolStripMenuItem.Text = "Çıkış";
            this.cikisToolStripMenuItem.Click += new System.EventHandler(this.cikisToolStripMenuItem_Click);
            // 
            // yardimToolStripMenuItem
            // 
            this.yardimToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hakkindaToolStripMenuItem});
            this.yardimToolStripMenuItem.Name = "yardimToolStripMenuItem";
            this.yardimToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.yardimToolStripMenuItem.Text = "Yardim";
            // 
            // hakkindaToolStripMenuItem
            // 
            this.hakkindaToolStripMenuItem.Name = "hakkindaToolStripMenuItem";
            this.hakkindaToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.hakkindaToolStripMenuItem.Text = "Hakkinda";
            // 
            // buttonList
            // 
            this.buttonList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("buttonList.ImageStream")));
            this.buttonList.TransparentColor = System.Drawing.Color.Transparent;
            this.buttonList.Images.SetKeyName(0, "PAUSE.png");
            this.buttonList.Images.SetKeyName(1, "PAUSE_ON.png");
            this.buttonList.Images.SetKeyName(2, "PLAY.png");
            this.buttonList.Images.SetKeyName(3, "PLAY_ON.png");
            this.buttonList.Images.SetKeyName(4, "PLAYLOOP.png");
            this.buttonList.Images.SetKeyName(5, "PLAYLOOP_ON.png");
            this.buttonList.Images.SetKeyName(6, "stop.png");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "sgnl";
            this.openFileDialog1.Filter = "Signal File (.sgnl)|";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "sgnl";
            this.saveFileDialog1.Filter = "Signal File (.sgnl)|";
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Transparent;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.IsOn = false;
            this.btnStop.Location = new System.Drawing.Point(946, 208);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 47);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "playButton4";
            this.btnStop.Type = PlayButton.Type.Stop;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.IsOn = false;
            this.btnPause.Location = new System.Drawing.Point(946, 155);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(48, 47);
            this.btnPause.TabIndex = 8;
            this.btnPause.Text = "playButton3";
            this.btnPause.Type = PlayButton.Type.Pause;
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlayLoop
            // 
            this.btnPlayLoop.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayLoop.FlatAppearance.BorderSize = 0;
            this.btnPlayLoop.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPlayLoop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPlayLoop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPlayLoop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayLoop.IsOn = false;
            this.btnPlayLoop.Location = new System.Drawing.Point(946, 102);
            this.btnPlayLoop.Name = "btnPlayLoop";
            this.btnPlayLoop.Size = new System.Drawing.Size(48, 47);
            this.btnPlayLoop.TabIndex = 7;
            this.btnPlayLoop.Text = "playButton2";
            this.btnPlayLoop.Type = PlayButton.Type.PlayLoop;
            this.btnPlayLoop.UseVisualStyleBackColor = false;
            this.btnPlayLoop.Click += new System.EventHandler(this.btnPlayLoop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.IsOn = false;
            this.btnPlay.Location = new System.Drawing.Point(946, 49);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(48, 47);
            this.btnPlay.TabIndex = 6;
            this.btnPlay.Text = "playButton1";
            this.btnPlay.Type = PlayButton.Type.Play;
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // graph2
            // 
            this.graph2.Base = null;
            this.graph2.Changed = false;
            this.graph2.Location = new System.Drawing.Point(11, 282);
            this.graph2.MiniFont = new System.Drawing.Font("Tahoma", 8F);
            this.graph2.Name = "graph2";
            this.graph2.Size = new System.Drawing.Size(919, 191);
            this.graph2.TabIndex = 2;
            this.graph2.WaveData = ((System.Collections.Generic.List<double>)(resources.GetObject("graph2.WaveData")));
            // 
            // graph1
            // 
            this.graph1.Base = null;
            this.graph1.Changed = false;
            this.graph1.Location = new System.Drawing.Point(11, 49);
            this.graph1.MiniFont = new System.Drawing.Font("Tahoma", 8F);
            this.graph1.Name = "graph1";
            this.graph1.Size = new System.Drawing.Size(919, 204);
            this.graph1.TabIndex = 1;
            this.graph1.WaveData = ((System.Collections.Generic.List<double>)(resources.GetObject("graph1.WaveData")));
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = global::PressControl.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(1011, 496);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlayLoop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graph2);
            this.Controls.Add(this.graph1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "App";
            this.Text = "UMPC v1.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private Graph graph1;
        private Graph graph2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yeniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yukleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenu;
        private System.Windows.Forms.ToolStripMenuItem cikisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yardimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hakkindaToolStripMenuItem;
        private System.Windows.Forms.ImageList buttonList;
        private PlayButton.PlayButton btnPlay;
        private PlayButton.PlayButton btnPlayLoop;
        private PlayButton.PlayButton btnPause;
        private PlayButton.PlayButton btnStop;
        private System.Windows.Forms.ToolStripMenuItem temizleToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem saveMenu;

    }
}