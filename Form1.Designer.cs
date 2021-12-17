
namespace oopLab6
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnArw = new System.Windows.Forms.Button();
            this.btnSctn = new System.Windows.Forms.Button();
            this.btnElps = new System.Windows.Forms.Button();
            this.btnTrn = new System.Windows.Forms.Button();
            this.btnRct = new System.Windows.Forms.Button();
            this.btnErs = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.btnArw);
            this.flowLayoutPanel1.Controls.Add(this.btnSctn);
            this.flowLayoutPanel1.Controls.Add(this.btnElps);
            this.flowLayoutPanel1.Controls.Add(this.btnTrn);
            this.flowLayoutPanel1.Controls.Add(this.btnRct);
            this.flowLayoutPanel1.Controls.Add(this.btnErs);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 54);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(40, 230);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnArw
            // 
            this.btnArw.Image = global::oopLab6.Properties.Resources.Arrow;
            this.btnArw.Location = new System.Drawing.Point(3, 3);
            this.btnArw.Name = "btnArw";
            this.btnArw.Size = new System.Drawing.Size(32, 32);
            this.btnArw.TabIndex = 1;
            this.btnArw.UseVisualStyleBackColor = true;
            // 
            // btnSctn
            // 
            this.btnSctn.Image = global::oopLab6.Properties.Resources.Section;
            this.btnSctn.Location = new System.Drawing.Point(3, 41);
            this.btnSctn.Name = "btnSctn";
            this.btnSctn.Size = new System.Drawing.Size(32, 32);
            this.btnSctn.TabIndex = 1;
            this.btnSctn.UseVisualStyleBackColor = true;
            // 
            // btnElps
            // 
            this.btnElps.Image = global::oopLab6.Properties.Resources.Circle;
            this.btnElps.Location = new System.Drawing.Point(3, 79);
            this.btnElps.Name = "btnElps";
            this.btnElps.Size = new System.Drawing.Size(32, 32);
            this.btnElps.TabIndex = 1;
            this.btnElps.UseVisualStyleBackColor = true;
            // 
            // btnTrn
            // 
            this.btnTrn.Image = global::oopLab6.Properties.Resources.Triangle;
            this.btnTrn.Location = new System.Drawing.Point(3, 117);
            this.btnTrn.Name = "btnTrn";
            this.btnTrn.Size = new System.Drawing.Size(32, 32);
            this.btnTrn.TabIndex = 1;
            this.btnTrn.UseVisualStyleBackColor = true;
            // 
            // btnRct
            // 
            this.btnRct.Image = global::oopLab6.Properties.Resources.Rectangle;
            this.btnRct.Location = new System.Drawing.Point(3, 155);
            this.btnRct.Name = "btnRct";
            this.btnRct.Size = new System.Drawing.Size(32, 32);
            this.btnRct.TabIndex = 1;
            this.btnRct.UseVisualStyleBackColor = true;
            // 
            // btnErs
            // 
            this.btnErs.Image = global::oopLab6.Properties.Resources.Eraser;
            this.btnErs.Location = new System.Drawing.Point(3, 193);
            this.btnErs.Name = "btnErs";
            this.btnErs.Size = new System.Drawing.Size(32, 32);
            this.btnErs.TabIndex = 1;
            this.btnErs.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(103, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 450);
            this.panel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 543);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSctn;
        private System.Windows.Forms.Button btnElps;
        private System.Windows.Forms.Button btnTrn;
        private System.Windows.Forms.Button btnRct;
        private System.Windows.Forms.Button btnErs;
        private System.Windows.Forms.Button btnArw;
        private System.Windows.Forms.Panel panel1;
    }
}

