namespace University_MPT_Lab2_GeneticAlgorithm
{
    partial class formMain
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
            this.splitCVerticalLevel1 = new System.Windows.Forms.SplitContainer();
            this.bClearLog = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.nudStep = new System.Windows.Forms.NumericUpDown();
            this.lStep = new System.Windows.Forms.Label();
            this.nudYMax = new System.Windows.Forms.NumericUpDown();
            this.lYmax = new System.Windows.Forms.Label();
            this.nudYMin = new System.Windows.Forms.NumericUpDown();
            this.lYmin = new System.Windows.Forms.Label();
            this.nudXMax = new System.Windows.Forms.NumericUpDown();
            this.lXmax = new System.Windows.Forms.Label();
            this.nudXMin = new System.Windows.Forms.NumericUpDown();
            this.lXmin = new System.Windows.Forms.Label();
            this.lEquation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitCVerticalLevel1)).BeginInit();
            this.splitCVerticalLevel1.Panel1.SuspendLayout();
            this.splitCVerticalLevel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXMin)).BeginInit();
            this.SuspendLayout();
            // 
            // splitCVerticalLevel1
            // 
            this.splitCVerticalLevel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitCVerticalLevel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCVerticalLevel1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitCVerticalLevel1.IsSplitterFixed = true;
            this.splitCVerticalLevel1.Location = new System.Drawing.Point(0, 0);
            this.splitCVerticalLevel1.Name = "splitCVerticalLevel1";
            // 
            // splitCVerticalLevel1.Panel1
            // 
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.bClearLog);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.rtbLog);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.progressBar1);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.button1);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.button2);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.nudStep);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.lStep);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.nudYMax);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.lYmax);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.nudYMin);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.lYmin);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.nudXMax);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.lXmax);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.nudXMin);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.lXmin);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.lEquation);
            this.splitCVerticalLevel1.Size = new System.Drawing.Size(1264, 681);
            this.splitCVerticalLevel1.SplitterDistance = 236;
            this.splitCVerticalLevel1.TabIndex = 0;
            // 
            // bClearLog
            // 
            this.bClearLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bClearLog.Location = new System.Drawing.Point(12, 633);
            this.bClearLog.Name = "bClearLog";
            this.bClearLog.Size = new System.Drawing.Size(211, 35);
            this.bClearLog.TabIndex = 15;
            this.bClearLog.Text = "Clear log";
            this.bClearLog.UseVisualStyleBackColor = true;
            this.bClearLog.Click += new System.EventHandler(this.bClearLog_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(12, 318);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(211, 309);
            this.rtbLog.TabIndex = 14;
            this.rtbLog.Text = "";
            this.rtbLog.TextChanged += new System.EventHandler(this.rtbLog_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 248);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(211, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(12, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open 3D graph window";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(12, 207);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 35);
            this.button2.TabIndex = 12;
            this.button2.Text = "Calculate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // nudStep
            // 
            this.nudStep.DecimalPlaces = 2;
            this.nudStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nudStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudStep.Location = new System.Drawing.Point(103, 175);
            this.nudStep.Name = "nudStep";
            this.nudStep.Size = new System.Drawing.Size(120, 26);
            this.nudStep.TabIndex = 11;
            this.nudStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // lStep
            // 
            this.lStep.AutoSize = true;
            this.lStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lStep.Location = new System.Drawing.Point(12, 177);
            this.lStep.Name = "lStep";
            this.lStep.Size = new System.Drawing.Size(43, 20);
            this.lStep.TabIndex = 10;
            this.lStep.Text = "Step";
            // 
            // nudYMax
            // 
            this.nudYMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nudYMax.Location = new System.Drawing.Point(103, 143);
            this.nudYMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudYMax.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.nudYMax.Name = "nudYMax";
            this.nudYMax.Size = new System.Drawing.Size(120, 26);
            this.nudYMax.TabIndex = 9;
            this.nudYMax.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lYmax
            // 
            this.lYmax.AutoSize = true;
            this.lYmax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lYmax.Location = new System.Drawing.Point(12, 145);
            this.lYmax.Name = "lYmax";
            this.lYmax.Size = new System.Drawing.Size(53, 20);
            this.lYmax.TabIndex = 8;
            this.lYmax.Text = "Y max";
            // 
            // nudYMin
            // 
            this.nudYMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nudYMin.Location = new System.Drawing.Point(103, 111);
            this.nudYMin.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudYMin.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.nudYMin.Name = "nudYMin";
            this.nudYMin.Size = new System.Drawing.Size(120, 26);
            this.nudYMin.TabIndex = 7;
            this.nudYMin.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            // 
            // lYmin
            // 
            this.lYmin.AutoSize = true;
            this.lYmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lYmin.Location = new System.Drawing.Point(12, 113);
            this.lYmin.Name = "lYmin";
            this.lYmin.Size = new System.Drawing.Size(49, 20);
            this.lYmin.TabIndex = 6;
            this.lYmin.Text = "Y min";
            // 
            // nudXMax
            // 
            this.nudXMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nudXMax.Location = new System.Drawing.Point(103, 79);
            this.nudXMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudXMax.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.nudXMax.Name = "nudXMax";
            this.nudXMax.Size = new System.Drawing.Size(120, 26);
            this.nudXMax.TabIndex = 5;
            this.nudXMax.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lXmax
            // 
            this.lXmax.AutoSize = true;
            this.lXmax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lXmax.Location = new System.Drawing.Point(12, 81);
            this.lXmax.Name = "lXmax";
            this.lXmax.Size = new System.Drawing.Size(53, 20);
            this.lXmax.TabIndex = 4;
            this.lXmax.Text = "X max";
            // 
            // nudXMin
            // 
            this.nudXMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nudXMin.Location = new System.Drawing.Point(103, 47);
            this.nudXMin.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudXMin.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.nudXMin.Name = "nudXMin";
            this.nudXMin.Size = new System.Drawing.Size(120, 26);
            this.nudXMin.TabIndex = 3;
            this.nudXMin.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            // 
            // lXmin
            // 
            this.lXmin.AutoSize = true;
            this.lXmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lXmin.Location = new System.Drawing.Point(12, 49);
            this.lXmin.Name = "lXmin";
            this.lXmin.Size = new System.Drawing.Size(49, 20);
            this.lXmin.TabIndex = 2;
            this.lXmin.Text = "X min";
            // 
            // lEquation
            // 
            this.lEquation.AutoSize = true;
            this.lEquation.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lEquation.Location = new System.Drawing.Point(12, 9);
            this.lEquation.Name = "lEquation";
            this.lEquation.Size = new System.Drawing.Size(211, 25);
            this.lEquation.TabIndex = 0;
            this.lEquation.Text = "z(x, y) = sin(x) + cos(y)";
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.splitCVerticalLevel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "formMain";
            this.Text = "Genetic Algorithm";
            this.Load += new System.EventHandler(this.formMain_Load);
            this.splitCVerticalLevel1.Panel1.ResumeLayout(false);
            this.splitCVerticalLevel1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCVerticalLevel1)).EndInit();
            this.splitCVerticalLevel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXMin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitCVerticalLevel1;
        private Label lEquation;
        private Button button1;
        private NumericUpDown nudStep;
        private Label lStep;
        private NumericUpDown nudYMax;
        private Label lYmax;
        private NumericUpDown nudYMin;
        private Label lYmin;
        private NumericUpDown nudXMax;
        private Label lXmax;
        private NumericUpDown nudXMin;
        private Label lXmin;
        private Button button2;
        private ProgressBar progressBar1;
        private RichTextBox rtbLog;
        private Button bClearLog;
    }
}