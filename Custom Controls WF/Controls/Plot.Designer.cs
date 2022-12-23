namespace Custom_Controls_WF
{
    partial class Plot
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.lblCoords = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMain.Location = new System.Drawing.Point(4, 0);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(600, 600);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            this.pbMain.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pbMain_LoadCompleted);
            this.pbMain.Click += new System.EventHandler(this.pbMain_Click);
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
            // 
            // lblCoords
            // 
            this.lblCoords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCoords.Font = new System.Drawing.Font("Segoe UI Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCoords.Location = new System.Drawing.Point(0, 577);
            this.lblCoords.Name = "lblCoords";
            this.lblCoords.Size = new System.Drawing.Size(600, 23);
            this.lblCoords.TabIndex = 1;
            this.lblCoords.Text = "lblCoords";
            this.lblCoords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Plot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCoords);
            this.Controls.Add(this.pbMain);
            this.Name = "Plot";
            this.Size = new System.Drawing.Size(600, 600);
            this.Load += new System.EventHandler(this.Plot_Load);
            this.Resize += new System.EventHandler(this.Plot_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Label lblCoords;
    }
}
