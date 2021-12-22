
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnArw = new System.Windows.Forms.Button();
            this.btnSctn = new System.Windows.Forms.Button();
            this.btnElps = new System.Windows.Forms.Button();
            this.btnTrn = new System.Windows.Forms.Button();
            this.btnRct = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBlck = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnGrn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numWdt = new System.Windows.Forms.NumericUpDown();
            this.numHgh = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numPosX = new System.Windows.Forms.NumericUpDown();
            this.numPosY = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nump2X = new System.Windows.Forms.NumericUpDown();
            this.nump2Y = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nump3X = new System.Windows.Forms.NumericUpDown();
            this.nump3Y = new System.Windows.Forms.NumericUpDown();
            this.lvObj = new System.Windows.Forms.ListBox();
            this.btnTrsh = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWdt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHgh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump2X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump2Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump3X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump3Y)).BeginInit();
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
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 54);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(40, 192);
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
            this.btnArw.Click += new System.EventHandler(this.btnArw_Click);
            // 
            // btnSctn
            // 
            this.btnSctn.Image = global::oopLab6.Properties.Resources.Section;
            this.btnSctn.Location = new System.Drawing.Point(3, 41);
            this.btnSctn.Name = "btnSctn";
            this.btnSctn.Size = new System.Drawing.Size(32, 32);
            this.btnSctn.TabIndex = 1;
            this.btnSctn.UseVisualStyleBackColor = true;
            this.btnSctn.Click += new System.EventHandler(this.btnSctn_Click);
            // 
            // btnElps
            // 
            this.btnElps.Image = global::oopLab6.Properties.Resources.Circle;
            this.btnElps.Location = new System.Drawing.Point(3, 79);
            this.btnElps.Name = "btnElps";
            this.btnElps.Size = new System.Drawing.Size(32, 32);
            this.btnElps.TabIndex = 1;
            this.btnElps.UseVisualStyleBackColor = true;
            this.btnElps.Click += new System.EventHandler(this.btnElps_Click);
            // 
            // btnTrn
            // 
            this.btnTrn.Image = global::oopLab6.Properties.Resources.Triangle;
            this.btnTrn.Location = new System.Drawing.Point(3, 117);
            this.btnTrn.Name = "btnTrn";
            this.btnTrn.Size = new System.Drawing.Size(32, 32);
            this.btnTrn.TabIndex = 1;
            this.btnTrn.UseVisualStyleBackColor = true;
            this.btnTrn.Click += new System.EventHandler(this.btnTrn_Click);
            // 
            // btnRct
            // 
            this.btnRct.Image = global::oopLab6.Properties.Resources.Rectangle;
            this.btnRct.Location = new System.Drawing.Point(3, 155);
            this.btnRct.Name = "btnRct";
            this.btnRct.Size = new System.Drawing.Size(32, 32);
            this.btnRct.TabIndex = 1;
            this.btnRct.UseVisualStyleBackColor = true;
            this.btnRct.Click += new System.EventHandler(this.btnRct_Click);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.Window;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(208, 54);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(500, 500);
            this.canvas.TabIndex = 1;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // flowLayoutPanel2
            // 
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
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Controls.Add(this.nump2X);
            this.flowLayoutPanel2.Controls.Add(this.nump2Y);
            this.flowLayoutPanel2.Controls.Add(this.label5);
            this.flowLayoutPanel2.Controls.Add(this.nump3X);
            this.flowLayoutPanel2.Controls.Add(this.nump3Y);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(58, 54);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(133, 350);
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
            this.flowLayoutPanel3.Controls.Add(this.btnBlck);
            this.flowLayoutPanel3.Controls.Add(this.btnBlue);
            this.flowLayoutPanel3.Controls.Add(this.btnGrn);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 23);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(102, 34);
            this.flowLayoutPanel3.TabIndex = 9;
            // 
            // btnBlck
            // 
            this.btnBlck.BackColor = System.Drawing.Color.Black;
            this.btnBlck.Location = new System.Drawing.Point(3, 3);
            this.btnBlck.Name = "btnBlck";
            this.btnBlck.Size = new System.Drawing.Size(28, 28);
            this.btnBlck.TabIndex = 0;
            this.btnBlck.UseVisualStyleBackColor = false;
            this.btnBlck.Click += new System.EventHandler(this.btnBlck_Click);
            // 
            // btnBlue
            // 
            this.btnBlue.BackColor = System.Drawing.Color.Blue;
            this.btnBlue.Location = new System.Drawing.Point(37, 3);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(28, 28);
            this.btnBlue.TabIndex = 0;
            this.btnBlue.UseVisualStyleBackColor = false;
            this.btnBlue.Click += new System.EventHandler(this.btnBlue_Click);
            // 
            // btnGrn
            // 
            this.btnGrn.BackColor = System.Drawing.Color.ForestGreen;
            this.btnGrn.Location = new System.Drawing.Point(71, 3);
            this.btnGrn.Name = "btnGrn";
            this.btnGrn.Size = new System.Drawing.Size(28, 28);
            this.btnGrn.TabIndex = 0;
            this.btnGrn.UseVisualStyleBackColor = false;
            this.btnGrn.Click += new System.EventHandler(this.btnGrn_Click);
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
            this.numWdt.ValueChanged += new System.EventHandler(this.size_ValueChanged);
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
            this.numHgh.ValueChanged += new System.EventHandler(this.size_ValueChanged);
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
            this.numPosX.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.numPosX.Name = "numPosX";
            this.numPosX.Size = new System.Drawing.Size(48, 27);
            this.numPosX.TabIndex = 7;
            this.numPosX.ValueChanged += new System.EventHandler(this.numP1_ValueChanged);
            // 
            // numPosY
            // 
            this.numPosY.Location = new System.Drawing.Point(57, 156);
            this.numPosY.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numPosY.Name = "numPosY";
            this.numPosY.Size = new System.Drawing.Size(48, 27);
            this.numPosY.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 40);
            this.label4.TabIndex = 5;
            this.label4.Text = "2nd additional point";
            // 
            // nump2X
            // 
            this.nump2X.Location = new System.Drawing.Point(3, 229);
            this.nump2X.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nump2X.Name = "nump2X";
            this.nump2X.Size = new System.Drawing.Size(48, 27);
            this.nump2X.TabIndex = 5;
            // 
            // nump2Y
            // 
            this.nump2Y.Location = new System.Drawing.Point(57, 229);
            this.nump2Y.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nump2Y.Name = "nump2Y";
            this.nump2Y.Size = new System.Drawing.Size(48, 27);
            this.nump2Y.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 40);
            this.label5.TabIndex = 5;
            this.label5.Text = "3rd additional point";
            // 
            // nump3X
            // 
            this.nump3X.Location = new System.Drawing.Point(3, 302);
            this.nump3X.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nump3X.Name = "nump3X";
            this.nump3X.Size = new System.Drawing.Size(48, 27);
            this.nump3X.TabIndex = 5;
            // 
            // nump3Y
            // 
            this.nump3Y.Location = new System.Drawing.Point(57, 302);
            this.nump3Y.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nump3Y.Name = "nump3Y";
            this.nump3Y.Size = new System.Drawing.Size(48, 27);
            this.nump3Y.TabIndex = 5;
            // 
            // lvObj
            // 
            this.lvObj.FormattingEnabled = true;
            this.lvObj.ItemHeight = 20;
            this.lvObj.Location = new System.Drawing.Point(11, 410);
            this.lvObj.Name = "lvObj";
            this.lvObj.Size = new System.Drawing.Size(180, 144);
            this.lvObj.TabIndex = 3;
            // 
            // btnTrsh
            // 
            this.btnTrsh.BackgroundImage = global::oopLab6.Properties.Resources.Trash_can;
            this.btnTrsh.Location = new System.Drawing.Point(159, 565);
            this.btnTrsh.Name = "btnTrsh";
            this.btnTrsh.Size = new System.Drawing.Size(32, 32);
            this.btnTrsh.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnTrsh, "Delete element");
            this.btnTrsh.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 609);
            this.Controls.Add(this.btnTrsh);
            this.Controls.Add(this.lvObj);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numWdt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHgh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump2X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump2Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump3X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump3Y)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSctn;
        private System.Windows.Forms.Button btnElps;
        private System.Windows.Forms.Button btnTrn;
        private System.Windows.Forms.Button btnRct;
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
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnGrn;
        private System.Windows.Forms.ListBox lvObj;
        private System.Windows.Forms.Button btnBlck;
        private System.Windows.Forms.Button btnTrsh;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nump2X;
        private System.Windows.Forms.NumericUpDown nump2Y;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nump3X;
        private System.Windows.Forms.NumericUpDown nump3Y;
    }
}

