using CSV_Reader.Common;
using ExcelReader.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using ED = ExcelReader.ExcelDocument.ExcelDocument;

namespace ExcelReader.Forms
{
    public partial class OpenFileWindow : BaseWindow.BaseWindow
    {

        #region Поля
        private string currentPath;
        private Dictionary<string, char> separators;
        #endregion

        #region Свойства

        #endregion

        #region Методы
        private void UpdateHeadersCheckBox()
        {
            if (this.chbIsHasHeaders.Checked)
            {
                this.chbIsHasHeaders.Text = "В таблице есть строка с заголовком";
            }
            else
            {
                this.chbIsHasHeaders.Text = "В таблице нет заголовка";
            }
        }
        private void UpdateDescriptionCheckBox()
        {
            if (this.chbIsHasDescriptions.Checked)
            {
                this.chbIsHasDescriptions.Text = "В таблице есть строка с описанием";
            }
            else
            {
                this.chbIsHasDescriptions.Text = "В таблице нет строки с описанием";
            }
        }
        private void UpdateComboBox()
        {
            if (this.cbSeparatorType != null)
            {
                foreach (var item in this.separators)
                {
                    this.cbSeparatorType.Items.Add(item.Key);
                }
                this.cbSeparatorType.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Открывает файл .csv или .xlsx
        /// </summary>
        /// <param name="path"></param>
        private void OpenFile(string path)
        {
            if (path == null)
            {
                throw new Exception("Не указан путь до файла!");
            }
            else
            {
                this.currentPath = path;
                string extension = Path.GetExtension(path);
                this.cbSeparatorType.Enabled = (extension == Common.Strings.Extensions.csv);
                ExcelParser excelParser = new ExcelParser();
                ED document = excelParser.Parse(
                    path,
                    this.separators[this.cbSeparatorType.SelectedItem.ToString()],
                    "Лист1",
                    this.chbIsHasHeaders.Checked,
                    this.chbIsHasDescriptions.Checked
                    );
                this.gvTable.DataSource = document.GetTable();
            }
        }


        #endregion

        #region Конструкторы/Деструкторы
        public OpenFileWindow()
        {
            this.InitializeComponent();
            this.currentPath = null;
            this.separators = new Dictionary<string, char>() {
            { "Точка с запятой",';'},
            { "Запятая",','}
        };
            this.UpdateComboBox();
            this.UpdateHeadersCheckBox();
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.OpenFile(this.GetLoadFilePath("Книга Excel|*.xlsx|CSV файл|*.csv"));
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        private void cbSeparatorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.currentPath))
            {
                this.OpenFile(this.currentPath);
            }
        }
        private void chbIsHasHeaders_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateHeadersCheckBox();
            if (!String.IsNullOrEmpty(this.currentPath))
            {
                this.OpenFile(this.currentPath);
            }
        }
        private void chbIsHasDescriptions_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateDescriptionCheckBox();
            if (!String.IsNullOrEmpty(this.currentPath))
            {
                this.OpenFile(this.currentPath);
            }
        }
        private void OpenFileWindow_Load(object sender, EventArgs e)
        {
            this.UpdateHeadersCheckBox();
            this.UpdateDescriptionCheckBox();
        }
        #endregion

    }
}
