using CSV_Reader.Common;
using ExcelReader.ExcelDocument;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ED = ExcelReader.ExcelDocument.ExcelDocument;
namespace ExcelReader.Parser
{
    //TODO: сделать парсинг CVS/XLSX с переключением по расширению
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
            #region Сохраняем имена листов в хэш таблицу
            foreach (Worksheet sheet in this.workbook.Sheets)
            {
                this.sheets[count] = sheet.Name;
                count++;
            }
            #endregion
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
        public ED ParseCsv(string path, char separator = ';', bool isHasHeaders = true, bool isHasFieldsDescription = false)
        {
            ED document = new ED(path);
            var reader = new StreamReader(path);
            for (int i = 0; !reader.EndOfStream; i++)
            {
                List<string> values = reader.ReadLine().Split(separator).ToList();
                if (values == null)
                {
                    throw new Exception(Common.Strings.Errors.rowParseErroe);
                }
                #region Вставка заголовков
                if (i == 0)
                {
                    if (isHasHeaders)
                    {
                        foreach (var value in values)
                        {
                            document.Add(new ExcelField(value));
                        }
                    }
                    else
                    {
                        for (int j = 0; j < values.Count; j++)
                        {
                            document.Add(new ExcelField(String.Format("Столбец #{0}", j), String.Empty, "Это автоматически сгенерированное название столбца"));
                        }
                        document.Add(new ExcelObject(document.Headers, values));
                    }
                }
                #endregion
                #region Вставка описаний столбцов
                else if (i == 1)
                {
                    if (isHasFieldsDescription)
                    {
                        if (values.Count == document.HeadersCount)
                        {
                            for (int j = 0; j < document.HeadersCount; j++)
                            {
                                document.Headers[j].Description = values[j];
                            }
                        }
                    }
                    else
                    {
                        document.Add(new ExcelObject(document.Headers, values));
                    }
                }
                #endregion
                #region Вставка обычной строки
                else
                {
                    document.Add(new ExcelObject(document.Headers, values));
                }
                #endregion
            }
            return document;
        }
        public ED ParseXlsx(string path, string sheetName = "Лист1", bool isHasHeaders = true, bool isHasFieldsDescription = false)
        {
            ED document = new ED(path);
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
                #region Запись строк в документ
                #region Получение заголовков таблицы
                if (range.Rows.Count > 0)
                {

                    for (int j = 1; j <= range.Columns.Count; j++)
                    {
                        if (isHasHeaders)
                        {
                            document.Add(new ExcelField(this.GetCellData(range, 1, j)));
                        }
                        else
                        {
                            document.Add(new ExcelField(String.Format("Столбец {0}", j)));
                        }
                    }
                }
                if (range.Rows.Count > 1)
                {
                }
                #endregion
                Parallel.For(2, range.Rows.Count + 1, i =>
            {

                #region Получение описаний столбцов
                if (i == 2 && isHasFieldsDescription)
                {
                    if (range.Columns.Count != document.HeadersCount)
                    {
                        throw new Exception(Common.Strings.Errors.headersCountNotMatch);
                    }
                    for (int j = 1; j <= range.Columns.Count; j++)
                    {
                        document.Headers[j - 1].Description = this.GetCellData(range, i, j);
                    }
                }
                #endregion
                #region Вставка обычной строки
                else
                {
                    if (range.Columns.Count != document.HeadersCount)
                    {
                        throw new Exception(Common.Strings.Errors.headersCountNotMatch);
                    }
                    ExcelObject @object = new ExcelObject();
                    for (int j = 1; j <= range.Columns.Count; j++)
                    {
                        @object.Add(new ExcelField(document.Headers[j - 1].Title, this.GetCellData(range, i, j), document.Headers[j - 1].Description));
                    }
                    document.Add(@object);
                }
                #endregion
            });
                #endregion
                Marshal.FinalReleaseComObject(worksheet);
            }
            this.CloseExcel(path);
            return document;
        }
        public ED Parse(string path, char separator = ';', string sheetName = "Лист1", bool isHasHeaders = true, bool isHasFieldsDescription = false)
        {
            ED document = null;
            string extension = Path.GetExtension(path);
            if (extension == Common.Strings.Extensions.csv)
            {
                document = ParseCsv(path, separator, isHasHeaders, isHasFieldsDescription);
            }
            if (extension == Common.Strings.Extensions.xlsx)
            {
                document = ParseXlsx(path, sheetName, isHasHeaders, isHasFieldsDescription);
            }
            return document;
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
