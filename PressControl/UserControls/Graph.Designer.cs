namespace PressControl
{
    partial class Graph
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.scrollBarEx1 = new CustomScrollBar.ScrollBarEx();
            this.SuspendLayout();
            // 
            // scrollBarEx1
            // 
            this.scrollBarEx1.BorderColor = System.Drawing.Color.White;
            this.scrollBarEx1.Location = new System.Drawing.Point(3, 169);
            this.scrollBarEx1.Name = "scrollBarEx1";
            this.scrollBarEx1.Orientation = CustomScrollBar.ScrollBarOrientation.Horizontal;
            this.scrollBarEx1.Size = new System.Drawing.Size(200, 19);
            this.scrollBarEx1.TabIndex = 0;
            this.scrollBarEx1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarEx1_Scroll);
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scrollBarEx1);
            this.Name = "Graph";
            this.Size = new System.Drawing.Size(671, 191);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomScrollBar.ScrollBarEx scrollBarEx1;

    }
}
