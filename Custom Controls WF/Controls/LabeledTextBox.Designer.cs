namespace Custom_Controls_WF.Controls
{
    partial class LabeledTextBox
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
            this.txbValue = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.GroupBox();
            this.lblTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbValue
            // 
            this.txbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbValue.Location = new System.Drawing.Point(9, 19);
            this.txbValue.Name = "txbValue";
            this.txbValue.Size = new System.Drawing.Size(85, 25);
            this.txbValue.TabIndex = 0;
            this.txbValue.Text = "Значение";
            this.txbValue.TextChanged += new System.EventHandler(this.txbValue_TextChanged);
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(9, 47);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(85, 18);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "label1";
            // 
            // lblTitle
            // 
            this.lblTitle.Controls.Add(this.lblError);
            this.lblTitle.Controls.Add(this.txbValue);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.TabStop = false;
            this.lblTitle.Text = "Заголовок";
            // 
            // LabeledTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Name = "LabeledTextBox";
            this.Size = new System.Drawing.Size(100, 70);
            this.lblTitle.ResumeLayout(false);
            this.lblTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txbValue;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.GroupBox lblTitle;
    }
}
