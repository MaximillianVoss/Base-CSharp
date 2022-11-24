namespace ExcelReader.Forms
{
    partial class OpenFileWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param Title="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbSeparatorType = new System.Windows.Forms.ComboBox();
            this.gvTable = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chbIsHasHeaders = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chbIsHasDescriptions = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTable)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // cbSeparatorType
            // 
            this.cbSeparatorType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSeparatorType.Enabled = false;
            this.cbSeparatorType.FormattingEnabled = true;
            this.cbSeparatorType.Location = new System.Drawing.Point(3, 16);
            this.cbSeparatorType.Name = "cbSeparatorType";
            this.cbSeparatorType.Size = new System.Drawing.Size(770, 21);
            this.cbSeparatorType.TabIndex = 2;
            this.cbSeparatorType.SelectedIndexChanged += new System.EventHandler(this.cbSeparatorType_SelectedIndexChanged);
            // 
            // gvTable
            // 
            this.gvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvTable.Location = new System.Drawing.Point(3, 16);
            this.gvTable.Name = "gvTable";
            this.gvTable.Size = new System.Drawing.Size(770, 337);
            this.gvTable.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.gvTable);
            this.groupBox1.Location = new System.Drawing.Point(12, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 356);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Таблица";
            // 
            // chbIsHasHeaders
            // 
            this.chbIsHasHeaders.AutoSize = true;
            this.chbIsHasHeaders.Checked = true;
            this.chbIsHasHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbIsHasHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbIsHasHeaders.Location = new System.Drawing.Point(3, 16);
            this.chbIsHasHeaders.Name = "chbIsHasHeaders";
            this.chbIsHasHeaders.Size = new System.Drawing.Size(770, 21);
            this.chbIsHasHeaders.TabIndex = 5;
            this.chbIsHasHeaders.Text = "Отсутствует";
            this.chbIsHasHeaders.UseVisualStyleBackColor = true;
            this.chbIsHasHeaders.CheckedChanged += new System.EventHandler(this.chbIsHasHeaders_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbSeparatorType);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 40);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Разделитель";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chbIsHasHeaders);
            this.groupBox3.Location = new System.Drawing.Point(12, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 40);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Заголовок";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chbIsHasDescriptions);
            this.groupBox4.Location = new System.Drawing.Point(12, 119);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(773, 40);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Описание столбцов (2 строка)";
            // 
            // chbIsHasDescriptions
            // 
            this.chbIsHasDescriptions.AutoSize = true;
            this.chbIsHasDescriptions.Checked = true;
            this.chbIsHasDescriptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbIsHasDescriptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbIsHasDescriptions.Location = new System.Drawing.Point(3, 16);
            this.chbIsHasDescriptions.Name = "chbIsHasDescriptions";
            this.chbIsHasDescriptions.Size = new System.Drawing.Size(767, 21);
            this.chbIsHasDescriptions.TabIndex = 5;
            this.chbIsHasDescriptions.Text = "Отсутствует";
            this.chbIsHasDescriptions.UseVisualStyleBackColor = true;
            this.chbIsHasDescriptions.CheckedChanged += new System.EventHandler(this.chbIsHasDescriptions_CheckedChanged);
            // 
            // OpenFileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 530);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OpenFileWindow";
            this.Text = "Просмотр содержимого файла";
            this.Load += new System.EventHandler(this.OpenFileWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTable)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbSeparatorType;
        private System.Windows.Forms.DataGridView gvTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox chbIsHasHeaders;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chbIsHasDescriptions;
    }
}

