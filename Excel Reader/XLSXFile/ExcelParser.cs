using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ExcelReader.XLSXFile
{
    public class ExcelParser
    {


        #region Поля
        private Application xlApp = null;
        private Workbooks workbooks = null;
        private Workbook workbook = null;
        private Hashtable sheets;
        private int readCells;
        private int totalCells;
        #endregion

        #region Свойства
        public int ReadCells => this.readCells;
        public int TotalCells => this.totalCells;
        #endregion

        #region Методы
        public void OpenExcel(string path)
        {
            this.xlApp = new Application();
            this.workbooks = this.xlApp.Workbooks;
            this.workbook = this.workbooks.Open(path);
            this.sheets = new Hashtable();
            int count = 1;
            // Storing worksheet names in Hashtable.
            foreach (Worksheet sheet in this.workbook.Sheets)
            {
                this.sheets[count] = sheet.Name;
                count++;
            }
        }
        public void CloseExcel(string path)
        {
            this.workbook.Close(false, path, null); // Close the connection to workbook
            Marshal.FinalReleaseComObject(this.workbook); // Release unmanaged object references.
            this.workbook = null;

            this.workbooks.Close();
            Marshal.FinalReleaseComObject(this.workbooks);
            this.workbooks = null;

            this.xlApp.Quit();
            Marshal.FinalReleaseComObject(this.xlApp);
            this.xlApp = null;
        }
        public string GetCellData(string path, string sheetName, string colName, int rowNumber)
        {
            this.OpenExcel(path);

            string value = string.Empty;
            int sheetValue = 0;
            int colNumber = 0;

            if (this.sheets.ContainsValue(sheetName))
            {
                foreach (DictionaryEntry sheet in this.sheets)
                {
                    if (sheet.Value.Equals(sheetName))
                    {
                        sheetValue = (int)sheet.Key;
                    }
                }
                Worksheet worksheet = null;
                worksheet = this.workbook.Worksheets[sheetValue] as Worksheet;
                Range range = worksheet.UsedRange;

                for (int i = 1; i <= range.Columns.Count; i++)
                {
                    string colNameValue = Convert.ToString((range.Cells[1, i] as Range).Value2);

                    if (colNameValue.ToLower() == colName.ToLower())
                    {
                        colNumber = i;
                        break;
                    }
                }

                value = Convert.ToString((range.Cells[rowNumber, colNumber] as Range).Value2);
                Marshal.FinalReleaseComObject(worksheet);
                worksheet = null;
            }
            this.CloseExcel(path);
            return value;
        }
        public string GetCellData(Range range, int rowIndex, int columnIndex)
        {
            Range cell = range.Cells[rowIndex, columnIndex] as Range;
            if (cell != null && cell.Value2 != null)
            {
                return cell.Value2.ToString();
            }
            return String.Empty;
        }
        public ExcelDocument Parse(string path, string sheetName = "Лист1")
        {
            ExcelDocument excelDocument = new ExcelDocument(path);
            this.OpenExcel(path);
            if (this.sheets.ContainsValue(sheetName))
            {
                int sheetValue = 0;
                foreach (DictionaryEntry sheet in this.sheets)
                {
                    if (sheet.Value.Equals(sheetName))
                    {
                        sheetValue = (int)sheet.Key;
                    }
                }
                Worksheet worksheet = this.workbook.Worksheets[sheetValue] as Worksheet;
                Range range = worksheet.UsedRange;
                if (range.Columns.Count < 1)
                {
                    throw new Exception("В таблице с данными должно быть как минимум один столбец!");
                }
                if (range.Rows.Count < 2)
                {
                    throw new Exception("В таблице с данными должно быть как минимум две строки с название столбцов и описанием!");
                }

                #region Подсчет прогресса выполнения
                this.totalCells = range.Columns.Count * range.Rows.Count;
                this.readCells = 0;
                #endregion

                Parallel.For(1, range.Columns.Count + 1, i =>
                {
                    ExcelColumn excelColumn = new ExcelColumn(this.GetCellData(range, 1, i), this.GetCellData(range, 2, i));
                    //Parallel.For(3, range.Rows.Count + 1, j =>
                    //{
                    //    excelColumn.Add(Convert.ToString((range.Cells[j, i] as Range).Value2));
                    //    this.readCells++;
                    //});
                    for (int j = 3; j < range.Rows.Count + 1; j++)
                    {
                        excelColumn.Add(Convert.ToString((range.Cells[j, i] as Range).Value2));
                        this.readCells++;
                    }

                    excelDocument.Add(excelColumn);
                });
                Marshal.FinalReleaseComObject(worksheet);
            }
            this.CloseExcel(path);
            return excelDocument;
        }
        #endregion

        #region Конструкторы/Деструкторы

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
