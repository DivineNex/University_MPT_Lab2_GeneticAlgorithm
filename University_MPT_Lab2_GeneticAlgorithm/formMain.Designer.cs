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
            this.button1 = new System.Windows.Forms.Button();
            this.lEquation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitCVerticalLevel1)).BeginInit();
            this.splitCVerticalLevel1.Panel1.SuspendLayout();
            this.splitCVerticalLevel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitCVerticalLevel1
            // 
            this.splitCVerticalLevel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCVerticalLevel1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitCVerticalLevel1.IsSplitterFixed = true;
            this.splitCVerticalLevel1.Location = new System.Drawing.Point(0, 0);
            this.splitCVerticalLevel1.Name = "splitCVerticalLevel1";
            // 
            // splitCVerticalLevel1.Panel1
            // 
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.button1);
            this.splitCVerticalLevel1.Panel1.Controls.Add(this.lEquation);
            this.splitCVerticalLevel1.Size = new System.Drawing.Size(1264, 681);
            this.splitCVerticalLevel1.SplitterDistance = 266;
            this.splitCVerticalLevel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open 3D graph window";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.Name = "formMain";
            this.Text = "Genetic Algorithm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formMain_Load);
            this.splitCVerticalLevel1.Panel1.ResumeLayout(false);
            this.splitCVerticalLevel1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCVerticalLevel1)).EndInit();
            this.splitCVerticalLevel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitCVerticalLevel1;
        private Label lEquation;
        private Button button1;
    }
}