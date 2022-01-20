
namespace oopLab6
{
    partial class PopupCanvSetSize
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
            this.mtbW = new System.Windows.Forms.MaskedTextBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mtbH = new System.Windows.Forms.MaskedTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mtbW
            // 
            this.mtbW.Location = new System.Drawing.Point(83, 26);
            this.mtbW.Mask = "000";
            this.mtbW.Name = "mtbW";
            this.mtbW.Size = new System.Drawing.Size(63, 27);
            this.mtbW.TabIndex = 0;
            this.mtbW.ValidatingType = typeof(int);
            // 
            // lblWeight
            // 
            this.lblWeight.Location = new System.Drawing.Point(12, 26);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(65, 27);
            this.lblWeight.TabIndex = 1;
            this.lblWeight.Text = "Weight:";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Height:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtbH
            // 
            this.mtbH.Location = new System.Drawing.Point(83, 59);
            this.mtbH.Mask = "000";
            this.mtbH.Name = "mtbH";
            this.mtbH.Size = new System.Drawing.Size(63, 27);
            this.mtbH.TabIndex = 0;
            this.mtbH.ValidatingType = typeof(int);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PopupCanvSetSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(167, 144);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.mtbH);
            this.Controls.Add(this.mtbW);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupCanvSetSize";
            this.Text = "PopupCanvSetSize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MaskedTextBox mtbW;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.MaskedTextBox mtbH;
        private System.Windows.Forms.Button button1;
    }
}