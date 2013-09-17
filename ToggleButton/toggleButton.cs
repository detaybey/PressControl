using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToggleButton
{
    public partial class toggleButton : Button
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

        public toggleButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackColor = Color.Transparent;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            //this.Click += new System.EventHandler(this.ButtonClick);
            this.type = ToggleButton.Type.SawTooth;
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

        //private void ButtonClick(object sender, EventArgs e)
        //{
        //    this.isOn = !this.isOn;
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            this.Width = 74;
            this.Height = 77;
            base.OnPaint(pe);
            pe.Graphics.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(0, 0, this.Width, this.Height));

            var name = "";
            switch (this.type)
            {
                case ToggleButton.Type.SawTooth:
                    name = "saw";
                    break;
                case ToggleButton.Type.Square:
                    name = "square";
                    break;
                case ToggleButton.Type.Triangle:
                    name = "triangle";
                    break;
            }
            if (this.isOn)
            {
                name += "_on.png";
            }
            else
            {
                name += "_off.png";
            }
            pe.Graphics.DrawImage(imageList.Images[name], new Point(0, 0));
        }
    }

    public enum Type
    {
        SawTooth = 1,
        Triangle = 2,
        Square = 3,
    }
}
