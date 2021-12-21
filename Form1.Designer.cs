
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
            this.canvas = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numWdt = new System.Windows.Forms.NumericUpDown();
            this.numHgh = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numPosX = new System.Windows.Forms.NumericUpDown();
            this.numPosY = new System.Windows.Forms.NumericUpDown();
            this.objects = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWdt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHgh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosY)).BeginInit();
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
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.Window;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(208, 54);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(450, 450);
            this.canvas.TabIndex = 1;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.numWdt);
            this.flowLayoutPanel2.Controls.Add(this.numHgh);
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.numPosX);
            this.flowLayoutPanel2.Controls.Add(this.numPosY);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(58, 54);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(133, 230);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Color:";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.button1);
            this.flowLayoutPanel3.Controls.Add(this.button2);
            this.flowLayoutPanel3.Controls.Add(this.button3);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 23);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(102, 34);
            this.flowLayoutPanel3.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 28);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(37, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(28, 28);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.ForestGreen;
            this.button3.Location = new System.Drawing.Point(71, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 28);
            this.button3.TabIndex = 0;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "Size (width; height):";
            // 
            // numWdt
            // 
            this.numWdt.Location = new System.Drawing.Point(3, 103);
            this.numWdt.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numWdt.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numWdt.Name = "numWdt";
            this.numWdt.Size = new System.Drawing.Size(48, 27);
            this.numWdt.TabIndex = 3;
            this.numWdt.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numHgh
            // 
            this.numHgh.Location = new System.Drawing.Point(57, 103);
            this.numHgh.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numHgh.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHgh.Name = "numHgh";
            this.numHgh.Size = new System.Drawing.Size(48, 27);
            this.numHgh.TabIndex = 4;
            this.numHgh.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Location (x; y)";
            // 
            // numPosX
            // 
            this.numPosX.Location = new System.Drawing.Point(3, 156);
            this.numPosX.Name = "numPosX";
            this.numPosX.Size = new System.Drawing.Size(48, 27);
            this.numPosX.TabIndex = 7;
            // 
            // numPosY
            // 
            this.numPosY.Location = new System.Drawing.Point(57, 156);
            this.numPosY.Name = "numPosY";
            this.numPosY.Size = new System.Drawing.Size(48, 27);
            this.numPosY.TabIndex = 8;
            // 
            // objects
            // 
            this.objects.FormattingEnabled = true;
            this.objects.ItemHeight = 20;
            this.objects.Location = new System.Drawing.Point(11, 320);
            this.objects.Name = "objects";
            this.objects.Size = new System.Drawing.Size(180, 184);
            this.objects.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 543);
            this.Controls.Add(this.objects);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numWdt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHgh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosY)).EndInit();
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
        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numWdt;
        private System.Windows.Forms.NumericUpDown numHgh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numPosX;
        private System.Windows.Forms.NumericUpDown numPosY;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox objects;
    }
}

