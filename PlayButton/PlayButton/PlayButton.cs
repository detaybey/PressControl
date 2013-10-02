using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayButton
{
    public partial class PlayButton: Button
    {
        private bool isOn { get; set; }
        private Type type { get; set; }

        public bool IsOn
        {
            get { return isOn; }
            set { isOn = value; Invalidate(); }
        }

        public Type Type
        {
            get { return type; }
            set { type = value; Invalidate(); }
        }

        public PlayButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackColor = Color.Transparent;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.type = global::PlayButton.Type.Play;
            this.MouseHover += toggleButton_MouseHover;
            this.MouseEnter += toggleButton_MouseEnter;
        }


        void toggleButton_MouseEnter(object sender, EventArgs e)
        {
            return;
        }

        void toggleButton_MouseHover(object sender, EventArgs e)
        {
            return;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            this.Width = 48;
            this.Height = 47;
            base.OnPaint(pe);
            pe.Graphics.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(0, 0, this.Width, this.Height));

            var name = "";
            switch (this.type)
            {
                case global::PlayButton.Type.Pause:
                    name = "PAUSE";
                    break;
                case global::PlayButton.Type.Play:
                    name = "PLAY";
                    break;
                case global::PlayButton.Type.PlayLoop:
                    name = "PLAYLOOP";
                    break;
                case global::PlayButton.Type.Stop:
                    name = "STOP";
                    break;
            }

            if (this.isOn && this.type != global::PlayButton.Type.Stop)
            {
                name += "_on.png";
            }
            else
            {
                name += ".png";
            }
            pe.Graphics.DrawImage(imageList1.Images[name], new Point(0, 0));
        }
    }

    public enum Type
    {
        Play = 1,
        PlayLoop = 2,
        Pause = 3,
        Stop = 4
    }
}
