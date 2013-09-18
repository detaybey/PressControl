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
            this.yeniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yukleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kaydetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cikisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yardimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hakkindaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graph2 = new PressControl.Graph();
            this.graph1 = new PressControl.Graph();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
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
            this.menuStrip1.Size = new System.Drawing.Size(743, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yeniToolStripMenuItem,
            this.yukleToolStripMenuItem,
            this.kaydetToolStripMenuItem,
            this.cikisToolStripMenuItem});
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.dosyaToolStripMenuItem.Text = "Dosya";
            // 
            // yeniToolStripMenuItem
            // 
            this.yeniToolStripMenuItem.Name = "yeniToolStripMenuItem";
            this.yeniToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.yeniToolStripMenuItem.Text = "Yeni";
            this.yeniToolStripMenuItem.Click += new System.EventHandler(this.yeniToolStripMenuItem_Click);
            // 
            // yukleToolStripMenuItem
            // 
            this.yukleToolStripMenuItem.Name = "yukleToolStripMenuItem";
            this.yukleToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.yukleToolStripMenuItem.Text = "Yukle";
            // 
            // kaydetToolStripMenuItem
            // 
            this.kaydetToolStripMenuItem.Name = "kaydetToolStripMenuItem";
            this.kaydetToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.kaydetToolStripMenuItem.Text = "Kaydet";
            // 
            // cikisToolStripMenuItem
            // 
            this.cikisToolStripMenuItem.Name = "cikisToolStripMenuItem";
            this.cikisToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.cikisToolStripMenuItem.Text = "Cikis";
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
            // graph2
            // 
            this.graph2.Base = null;
            this.graph2.DataForm = null;
            this.graph2.Location = new System.Drawing.Point(11, 282);
            this.graph2.MiniFont = new System.Drawing.Font("Tahoma", 8F);
            this.graph2.Name = "graph2";
            this.graph2.Size = new System.Drawing.Size(720, 191);
            this.graph2.TabIndex = 2;
            // 
            // graph1
            // 
            this.graph1.Base = null;
            this.graph1.DataForm = null;
            this.graph1.Location = new System.Drawing.Point(11, 49);
            this.graph1.MiniFont = new System.Drawing.Font("Tahoma", 8F);
            this.graph1.Name = "graph1";
            this.graph1.Size = new System.Drawing.Size(720, 204);
            this.graph1.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(13, 490);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(149, 490);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 7;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = global::PressControl.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(743, 530);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem kaydetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cikisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yardimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hakkindaToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;

    }
}