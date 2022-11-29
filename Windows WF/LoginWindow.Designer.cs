namespace BaseWindow
{
    partial class LoginWindow
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
            this.gbPrimary = new System.Windows.Forms.GroupBox();
            this.txbPrimary = new System.Windows.Forms.TextBox();
            this.btnPrimary = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbSecondary = new System.Windows.Forms.GroupBox();
            this.txbSecondary = new System.Windows.Forms.TextBox();
            this.btnSecondary = new System.Windows.Forms.Button();
            this.gbPrimary.SuspendLayout();
            this.gbSecondary.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPrimary
            // 
            this.gbPrimary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPrimary.Controls.Add(this.txbPrimary);
            this.gbPrimary.Location = new System.Drawing.Point(14, 59);
            this.gbPrimary.Margin = new System.Windows.Forms.Padding(5);
            this.gbPrimary.Name = "gbPrimary";
            this.gbPrimary.Size = new System.Drawing.Size(256, 40);
            this.gbPrimary.TabIndex = 0;
            this.gbPrimary.TabStop = false;
            this.gbPrimary.Text = "Логин";
            // 
            // txbPrimary
            // 
            this.txbPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbPrimary.Location = new System.Drawing.Point(3, 16);
            this.txbPrimary.Name = "txbPrimary";
            this.txbPrimary.Size = new System.Drawing.Size(250, 20);
            this.txbPrimary.TabIndex = 0;
            this.txbPrimary.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPrimary
            // 
            this.btnPrimary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrimary.Location = new System.Drawing.Point(14, 163);
            this.btnPrimary.Name = "btnPrimary";
            this.btnPrimary.Size = new System.Drawing.Size(258, 40);
            this.btnPrimary.TabIndex = 1;
            this.btnPrimary.Text = "Primary";
            this.btnPrimary.UseVisualStyleBackColor = true;
            this.btnPrimary.Click += new System.EventHandler(this.btnPriamry_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(14, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(258, 45);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbSecondary
            // 
            this.gbSecondary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSecondary.Controls.Add(this.txbSecondary);
            this.gbSecondary.Location = new System.Drawing.Point(14, 109);
            this.gbSecondary.Margin = new System.Windows.Forms.Padding(5);
            this.gbSecondary.Name = "gbSecondary";
            this.gbSecondary.Size = new System.Drawing.Size(256, 40);
            this.gbSecondary.TabIndex = 4;
            this.gbSecondary.TabStop = false;
            this.gbSecondary.Text = "Пароль";
            // 
            // txbSecondary
            // 
            this.txbSecondary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbSecondary.Location = new System.Drawing.Point(3, 16);
            this.txbSecondary.Name = "txbSecondary";
            this.txbSecondary.Size = new System.Drawing.Size(250, 20);
            this.txbSecondary.TabIndex = 0;
            this.txbSecondary.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSecondary
            // 
            this.btnSecondary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSecondary.Location = new System.Drawing.Point(14, 209);
            this.btnSecondary.Name = "btnSecondary";
            this.btnSecondary.Size = new System.Drawing.Size(258, 40);
            this.btnSecondary.TabIndex = 5;
            this.btnSecondary.Text = "Secondary";
            this.btnSecondary.UseVisualStyleBackColor = true;
            this.btnSecondary.Click += new System.EventHandler(this.btnSecondary_Click);
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnSecondary);
            this.Controls.Add(this.gbSecondary);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnPrimary);
            this.Controls.Add(this.gbPrimary);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "LoginWindow";
            this.Text = "WindowTitle";
            this.gbPrimary.ResumeLayout(false);
            this.gbPrimary.PerformLayout();
            this.gbSecondary.ResumeLayout(false);
            this.gbSecondary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPrimary;
        private System.Windows.Forms.TextBox txbPrimary;
        private System.Windows.Forms.Button btnPrimary;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gbSecondary;
        private System.Windows.Forms.TextBox txbSecondary;
        private System.Windows.Forms.Button btnSecondary;
    }
}