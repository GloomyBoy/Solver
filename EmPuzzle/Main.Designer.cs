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
            this.clbSwaps = new System.Windows.Forms.CheckedListBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btnSavePreview = new System.Windows.Forms.Button();
            this.cbTitanColor = new System.Windows.Forms.ComboBox();
            this.pbEnemies = new System.Windows.Forms.PictureBox();
            this.cbRec = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbGameGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenshot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnemies)).BeginInit();
            this.SuspendLayout();
            // 
            // pbGameGrid
            // 
            this.pbGameGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGameGrid.Location = new System.Drawing.Point(12, 105);
            this.pbGameGrid.Name = "pbGameGrid";
            this.pbGameGrid.Size = new System.Drawing.Size(420, 300);
            this.pbGameGrid.TabIndex = 0;
            this.pbGameGrid.TabStop = false;
            // 
            // pbScreenshot
            // 
            this.pbScreenshot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbScreenshot.Location = new System.Drawing.Point(438, 39);
            this.pbScreenshot.Name = "pbScreenshot";
            this.pbScreenshot.Size = new System.Drawing.Size(234, 366);
            this.pbScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbScreenshot.TabIndex = 1;
            this.pbScreenshot.TabStop = false;
            this.pbScreenshot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbScreenshot_MouseDown);
            this.pbScreenshot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbScreenshot_MouseUp);
            // 
            // btnSaveSnap
            // 
            this.btnSaveSnap.Location = new System.Drawing.Point(548, 411);
            this.btnSaveSnap.Name = "btnSaveSnap";
            this.btnSaveSnap.Size = new System.Drawing.Size(124, 22);
            this.btnSaveSnap.TabIndex = 2;
            this.btnSaveSnap.Text = "Take Screenshot";
            this.btnSaveSnap.UseVisualStyleBackColor = true;
            this.btnSaveSnap.Click += new System.EventHandler(this.btnSaveSnap_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(548, 610);
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
            this.cbCapture.Location = new System.Drawing.Point(548, 558);
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
            // clbSwaps
            // 
            this.clbSwaps.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clbSwaps.FormattingEnabled = true;
            this.clbSwaps.Location = new System.Drawing.Point(12, 411);
            this.clbSwaps.Name = "clbSwaps";
            this.clbSwaps.Size = new System.Drawing.Size(420, 244);
            this.clbSwaps.TabIndex = 6;
            this.clbSwaps.SelectedIndexChanged += new System.EventHandler(this.clbSwaps_SelectedIndexChanged);
            // 
            // pbPreview
            // 
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Location = new System.Drawing.Point(548, 439);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(124, 113);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 7;
            this.pbPreview.TabStop = false;
            // 
            // btnSavePreview
            // 
            this.btnSavePreview.Location = new System.Drawing.Point(548, 581);
            this.btnSavePreview.Name = "btnSavePreview";
            this.btnSavePreview.Size = new System.Drawing.Size(122, 23);
            this.btnSavePreview.TabIndex = 8;
            this.btnSavePreview.Text = "Save";
            this.btnSavePreview.UseVisualStyleBackColor = true;
            this.btnSavePreview.Click += new System.EventHandler(this.btnSavePreview_Click);
            // 
            // cbTitanColor
            // 
            this.cbTitanColor.FormattingEnabled = true;
            this.cbTitanColor.Location = new System.Drawing.Point(12, 12);
            this.cbTitanColor.Name = "cbTitanColor";
            this.cbTitanColor.Size = new System.Drawing.Size(121, 21);
            this.cbTitanColor.TabIndex = 9;
            // 
            // pbEnemies
            // 
            this.pbEnemies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbEnemies.Location = new System.Drawing.Point(12, 39);
            this.pbEnemies.Name = "pbEnemies";
            this.pbEnemies.Size = new System.Drawing.Size(420, 60);
            this.pbEnemies.TabIndex = 10;
            this.pbEnemies.TabStop = false;
            // 
            // cbRec
            // 
            this.cbRec.AutoSize = true;
            this.cbRec.Location = new System.Drawing.Point(618, 558);
            this.cbRec.Name = "cbRec";
            this.cbRec.Size = new System.Drawing.Size(48, 17);
            this.cbRec.TabIndex = 11;
            this.cbRec.Text = "REC";
            this.cbRec.UseVisualStyleBackColor = true;
            this.cbRec.CheckedChanged += new System.EventHandler(this.cbRec_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 665);
            this.Controls.Add(this.cbRec);
            this.Controls.Add(this.pbEnemies);
            this.Controls.Add(this.cbTitanColor);
            this.Controls.Add(this.btnSavePreview);
            this.Controls.Add(this.pbPreview);
            this.Controls.Add(this.clbSwaps);
            this.Controls.Add(this.cbCapture);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnSaveSnap);
            this.Controls.Add(this.pbScreenshot);
            this.Controls.Add(this.pbGameGrid);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbGameGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenshot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnemies)).EndInit();
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
        private System.Windows.Forms.CheckedListBox clbSwaps;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Button btnSavePreview;
        private System.Windows.Forms.ComboBox cbTitanColor;
        private System.Windows.Forms.PictureBox pbEnemies;
        private System.Windows.Forms.CheckBox cbRec;
    }
}

