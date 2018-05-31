namespace EmPuzzle
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbGameGrid = new System.Windows.Forms.PictureBox();
            this.pbScreenshot = new System.Windows.Forms.PictureBox();
            this.btnSaveSnap = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.cbCapture = new System.Windows.Forms.CheckBox();
            this.ofLoadImage = new System.Windows.Forms.OpenFileDialog();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbGameGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenshot)).BeginInit();
            this.SuspendLayout();
            // 
            // pbGameGrid
            // 
            this.pbGameGrid.Location = new System.Drawing.Point(12, 120);
            this.pbGameGrid.Name = "pbGameGrid";
            this.pbGameGrid.Size = new System.Drawing.Size(420, 300);
            this.pbGameGrid.TabIndex = 0;
            this.pbGameGrid.TabStop = false;
            // 
            // pbScreenshot
            // 
            this.pbScreenshot.Location = new System.Drawing.Point(12, 426);
            this.pbScreenshot.Name = "pbScreenshot";
            this.pbScreenshot.Size = new System.Drawing.Size(289, 401);
            this.pbScreenshot.TabIndex = 1;
            this.pbScreenshot.TabStop = false;
            // 
            // btnSaveSnap
            // 
            this.btnSaveSnap.Location = new System.Drawing.Point(308, 427);
            this.btnSaveSnap.Name = "btnSaveSnap";
            this.btnSaveSnap.Size = new System.Drawing.Size(124, 37);
            this.btnSaveSnap.TabIndex = 2;
            this.btnSaveSnap.Text = "Take Screenshot";
            this.btnSaveSnap.UseVisualStyleBackColor = true;
            this.btnSaveSnap.Click += new System.EventHandler(this.btnSaveSnap_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(307, 787);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(124, 40);
            this.btnLoadFile.TabIndex = 4;
            this.btnLoadFile.Text = "Load FIle";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // cbCapture
            // 
            this.cbCapture.AutoSize = true;
            this.cbCapture.Location = new System.Drawing.Point(308, 470);
            this.cbCapture.Name = "cbCapture";
            this.cbCapture.Size = new System.Drawing.Size(63, 17);
            this.cbCapture.TabIndex = 5;
            this.cbCapture.Text = "Capture";
            this.cbCapture.UseVisualStyleBackColor = true;
            this.cbCapture.CheckedChanged += new System.EventHandler(this.cbCapture_CheckedChanged);
            // 
            // ofLoadImage
            // 
            this.ofLoadImage.FileName = "openFileDialog1";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(308, 601);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(124, 139);
            this.checkedListBox1.TabIndex = 6;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 839);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.cbCapture);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnSaveSnap);
            this.Controls.Add(this.pbScreenshot);
            this.Controls.Add(this.pbGameGrid);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbGameGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenshot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGameGrid;
        private System.Windows.Forms.PictureBox pbScreenshot;
        private System.Windows.Forms.Button btnSaveSnap;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.CheckBox cbCapture;
        private System.Windows.Forms.OpenFileDialog ofLoadImage;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}

