
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
            this.flpCol = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBlck = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnGrn = new System.Windows.Forms.Button();
            this.flpThck = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.numThck = new System.Windows.Forms.NumericUpDown();
            this.flpSz = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.numHgh = new System.Windows.Forms.NumericUpDown();
            this.numWdt = new System.Windows.Forms.NumericUpDown();
            this.flpP1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.numPosX = new System.Windows.Forms.NumericUpDown();
            this.numPosY = new System.Windows.Forms.NumericUpDown();
            this.flpP2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.nump2X = new System.Windows.Forms.NumericUpDown();
            this.nump2Y = new System.Windows.Forms.NumericUpDown();
            this.flpP3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.nump3X = new System.Windows.Forms.NumericUpDown();
            this.nump3Y = new System.Windows.Forms.NumericUpDown();
            this.lvObj = new System.Windows.Forms.ListBox();
            this.btnTrsh = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flpCol.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flpThck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThck)).BeginInit();
            this.flpSz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHgh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWdt)).BeginInit();
            this.flpP1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosY)).BeginInit();
            this.flpP2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nump2X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump2Y)).BeginInit();
            this.flpP3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nump3X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump3Y)).BeginInit();
            this.menuStrip1.SuspendLayout();
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
            this.toolTip1.SetToolTip(this.btnSctn, "Tap 2 times on the easel");
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
            this.toolTip1.SetToolTip(this.btnTrn, "Tap 3 times on the easel");
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
            this.canvas.Location = new System.Drawing.Point(197, 54);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(500, 536);
            this.canvas.TabIndex = 1;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.flpCol);
            this.flowLayoutPanel2.Controls.Add(this.flpThck);
            this.flowLayoutPanel2.Controls.Add(this.flpSz);
            this.flowLayoutPanel2.Controls.Add(this.flpP1);
            this.flowLayoutPanel2.Controls.Add(this.flpP2);
            this.flowLayoutPanel2.Controls.Add(this.flpP3);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(58, 54);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(133, 426);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // flpCol
            // 
            this.flpCol.Controls.Add(this.label1);
            this.flpCol.Controls.Add(this.flowLayoutPanel3);
            this.flpCol.Location = new System.Drawing.Point(3, 3);
            this.flpCol.Name = "flpCol";
            this.flpCol.Size = new System.Drawing.Size(129, 61);
            this.flpCol.TabIndex = 12;
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
            // flpThck
            // 
            this.flpThck.BackColor = System.Drawing.Color.White;
            this.flpThck.Controls.Add(this.label6);
            this.flpThck.Controls.Add(this.numThck);
            this.flpThck.Location = new System.Drawing.Point(3, 70);
            this.flpThck.Name = "flpThck";
            this.flpThck.Size = new System.Drawing.Size(129, 60);
            this.flpThck.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 25);
            this.label6.TabIndex = 10;
            this.label6.Text = "Thickness:";
            // 
            // numThck
            // 
            this.numThck.Location = new System.Drawing.Point(3, 28);
            this.numThck.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numThck.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThck.Name = "numThck";
            this.numThck.Size = new System.Drawing.Size(48, 27);
            this.numThck.TabIndex = 5;
            this.numThck.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // flpSz
            // 
            this.flpSz.Controls.Add(this.label2);
            this.flpSz.Controls.Add(this.numHgh);
            this.flpSz.Controls.Add(this.numWdt);
            this.flpSz.Location = new System.Drawing.Point(3, 136);
            this.flpSz.Name = "flpSz";
            this.flpSz.Size = new System.Drawing.Size(129, 70);
            this.flpSz.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "Size (width; height):";
            // 
            // numHgh
            // 
            this.numHgh.Location = new System.Drawing.Point(3, 43);
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
            // numWdt
            // 
            this.numWdt.Location = new System.Drawing.Point(57, 43);
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
            // flpP1
            // 
            this.flpP1.Controls.Add(this.label3);
            this.flpP1.Controls.Add(this.numPosX);
            this.flpP1.Controls.Add(this.numPosY);
            this.flpP1.Location = new System.Drawing.Point(3, 212);
            this.flpP1.Name = "flpP1";
            this.flpP1.Size = new System.Drawing.Size(129, 54);
            this.flpP1.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Location (x; y)";
            // 
            // numPosX
            // 
            this.numPosX.Location = new System.Drawing.Point(3, 23);
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
            this.numPosY.Location = new System.Drawing.Point(57, 23);
            this.numPosY.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numPosY.Name = "numPosY";
            this.numPosY.Size = new System.Drawing.Size(48, 27);
            this.numPosY.TabIndex = 8;
            // 
            // flpP2
            // 
            this.flpP2.Controls.Add(this.label4);
            this.flpP2.Controls.Add(this.nump2X);
            this.flpP2.Controls.Add(this.nump2Y);
            this.flpP2.Location = new System.Drawing.Point(3, 272);
            this.flpP2.Name = "flpP2";
            this.flpP2.Size = new System.Drawing.Size(129, 73);
            this.flpP2.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 40);
            this.label4.TabIndex = 5;
            this.label4.Text = "2nd additional point";
            // 
            // nump2X
            // 
            this.nump2X.Location = new System.Drawing.Point(3, 43);
            this.nump2X.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nump2X.Name = "nump2X";
            this.nump2X.Size = new System.Drawing.Size(48, 27);
            this.nump2X.TabIndex = 5;
            this.nump2X.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nump2Y
            // 
            this.nump2Y.Location = new System.Drawing.Point(57, 43);
            this.nump2Y.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nump2Y.Name = "nump2Y";
            this.nump2Y.Size = new System.Drawing.Size(48, 27);
            this.nump2Y.TabIndex = 5;
            this.nump2Y.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // flpP3
            // 
            this.flpP3.Controls.Add(this.label5);
            this.flpP3.Controls.Add(this.nump3X);
            this.flpP3.Controls.Add(this.nump3Y);
            this.flpP3.Location = new System.Drawing.Point(3, 351);
            this.flpP3.Name = "flpP3";
            this.flpP3.Size = new System.Drawing.Size(129, 68);
            this.flpP3.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 40);
            this.label5.TabIndex = 5;
            this.label5.Text = "3rd additional point";
            // 
            // nump3X
            // 
            this.nump3X.Location = new System.Drawing.Point(3, 43);
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
            this.nump3Y.Location = new System.Drawing.Point(57, 43);
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
            this.lvObj.Location = new System.Drawing.Point(11, 486);
            this.lvObj.Name = "lvObj";
            this.lvObj.Size = new System.Drawing.Size(180, 104);
            this.lvObj.TabIndex = 3;
            this.lvObj.SelectedIndexChanged += new System.EventHandler(this.lvObj_SelectedIndexChanged);
            // 
            // btnTrsh
            // 
            this.btnTrsh.BackgroundImage = global::oopLab6.Properties.Resources.Trash_can;
            this.btnTrsh.Location = new System.Drawing.Point(159, 558);
            this.btnTrsh.Name = "btnTrsh";
            this.btnTrsh.Size = new System.Drawing.Size(32, 32);
            this.btnTrsh.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnTrsh, "Delete element");
            this.btnTrsh.UseVisualStyleBackColor = true;
            this.btnTrsh.Click += new System.EventHandler(this.btnTrsh_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(948, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            this.toolTip1.SetToolTip(this.menuStrip1, "Clear the easel");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(130, 24);
            this.toolStripMenuItem1.Text = "Clear everything";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 653);
            this.Controls.Add(this.btnTrsh);
            this.Controls.Add(this.lvObj);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flpCol.ResumeLayout(false);
            this.flpCol.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flpThck.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numThck)).EndInit();
            this.flpSz.ResumeLayout(false);
            this.flpSz.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHgh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWdt)).EndInit();
            this.flpP1.ResumeLayout(false);
            this.flpP1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPosY)).EndInit();
            this.flpP2.ResumeLayout(false);
            this.flpP2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nump2X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump2Y)).EndInit();
            this.flpP3.ResumeLayout(false);
            this.flpP3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nump3X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nump3Y)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numThck;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.FlowLayoutPanel flpCol;
        private System.Windows.Forms.FlowLayoutPanel flpThck;
        private System.Windows.Forms.FlowLayoutPanel flpSz;
        private System.Windows.Forms.FlowLayoutPanel flpP1;
        private System.Windows.Forms.FlowLayoutPanel flpP2;
        private System.Windows.Forms.FlowLayoutPanel flpP3;
    }
}

