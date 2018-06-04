namespace Sandbox
{
    partial class Form1
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
            this.pbSRC = new System.Windows.Forms.PictureBox();
            this.pbTemplate = new System.Windows.Forms.PictureBox();
            this.btnLoadSrc = new System.Windows.Forms.Button();
            this.btnLoadTemplate = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.lbWeight = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSRC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSRC
            // 
            this.pbSRC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSRC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSRC.Location = new System.Drawing.Point(12, 12);
            this.pbSRC.Name = "pbSRC";
            this.pbSRC.Size = new System.Drawing.Size(402, 557);
            this.pbSRC.TabIndex = 0;
            this.pbSRC.TabStop = false;
            // 
            // pbTemplate
            // 
            this.pbTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTemplate.Location = new System.Drawing.Point(420, 12);
            this.pbTemplate.Name = "pbTemplate";
            this.pbTemplate.Size = new System.Drawing.Size(227, 171);
            this.pbTemplate.TabIndex = 1;
            this.pbTemplate.TabStop = false;
            // 
            // btnLoadSrc
            // 
            this.btnLoadSrc.Location = new System.Drawing.Point(420, 189);
            this.btnLoadSrc.Name = "btnLoadSrc";
            this.btnLoadSrc.Size = new System.Drawing.Size(75, 23);
            this.btnLoadSrc.TabIndex = 2;
            this.btnLoadSrc.Text = "SRC";
            this.btnLoadSrc.UseVisualStyleBackColor = true;
            this.btnLoadSrc.Click += new System.EventHandler(this.btnLoadSrc_Click);
            // 
            // btnLoadTemplate
            // 
            this.btnLoadTemplate.Location = new System.Drawing.Point(501, 189);
            this.btnLoadTemplate.Name = "btnLoadTemplate";
            this.btnLoadTemplate.Size = new System.Drawing.Size(75, 23);
            this.btnLoadTemplate.TabIndex = 3;
            this.btnLoadTemplate.Text = "TEMPLATE";
            this.btnLoadTemplate.UseVisualStyleBackColor = true;
            this.btnLoadTemplate.Click += new System.EventHandler(this.btnLoadTemplate_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(583, 189);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(64, 23);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lbWeight
            // 
            this.lbWeight.FormattingEnabled = true;
            this.lbWeight.Location = new System.Drawing.Point(420, 219);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(227, 342);
            this.lbWeight.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 581);
            this.Controls.Add(this.lbWeight);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnLoadTemplate);
            this.Controls.Add(this.btnLoadSrc);
            this.Controls.Add(this.pbTemplate);
            this.Controls.Add(this.pbSRC);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSRC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSRC;
        private System.Windows.Forms.PictureBox pbTemplate;
        private System.Windows.Forms.Button btnLoadSrc;
        private System.Windows.Forms.Button btnLoadTemplate;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ListBox lbWeight;
    }
}

