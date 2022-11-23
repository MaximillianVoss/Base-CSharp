using ExcelReader.CSVFile;
using ExcelReader.XLSXFile;
using System;
using System.Collections.Generic;
using System.IO;

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
        private void UpdateCheckBox()
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
                if (Path.GetExtension(path) == ".xlsx")
                {
                    this.cbSeparatorType.Enabled = false;
                    this.OpenXlsx(path);
                }
                if (Path.GetExtension(path) == ".csv")
                {
                    //this.ShowMessage("Выберите разделитель");
                    this.cbSeparatorType.Enabled = true;
                    this.OpenCSV(path);
                }
            }
        }

        private void OpenXlsx(string path)
        {
            if (path != null)
            {
                ExcelDocument xlsx = new ExcelDocument(path);
                this.gvTable.DataSource = xlsx.GetTable();
            }
        }

        private void OpenCSV(string path)
        {
            if (path != null)
            {
                CSVDocument csv = new CSVDocument(path, this.chbIsHasHeaders.Checked, this.separators[this.cbSeparatorType.SelectedItem.ToString()]);
                this.gvTable.DataSource = csv.GetTable();
            }
        }


        #endregion

        #region Конструкторы/Деструкторы
        public OpenFileWindow()
        {
            this.InitializeComponent();
            this.currentPath = null;
            this.separators = new Dictionary<string, char>() {
            { "Запятая",','},
            { "Точка с запятой",';'}
        };
            this.UpdateComboBox();
            this.UpdateCheckBox();
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog
            //{
            //    Filter = "csv files (*.csv)|*.csv|Книга Excel|*.xlsx",
            //    RestoreDirectory = true
            //};
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    if (this.separators.ContainsKey(this.cbSeparatorType.SelectedItem.ToString()))
            //    {
            //        CSVDocument csv = new CSVDocument(openFileDialog.FileName, this.chbIsHasHeaders.Checked, this.separators[this.cbSeparatorType.SelectedItem.ToString()]);
            //        this.gvTable.DataSource = csv.GetTable();
            //    }
            //}
            try
            {
                this.OpenFile(this.GetLoadFilePath("CSV файл|*.csv|Книга Excel|*.xlsx"));
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        private void cbSeparatorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OpenCSV(this.currentPath);
        }
        private void chbIsHasHeaders_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateCheckBox();
            this.OpenFile(this.currentPath);
        }

        #endregion

    }
}
