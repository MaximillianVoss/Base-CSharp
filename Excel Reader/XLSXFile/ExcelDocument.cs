using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExcelReader.XLSXFile
{
    public class ExcelDocument
    {



        #region Поля
        private int currentRowIndex;
        private int readCells;
        #endregion

        #region Свойства
        /// <summary>
        /// Название файла без расширения
        /// </summary>
        public string Title => Path.GetFileNameWithoutExtension(this.FilePath);
        /// <summary>
        /// Путь до файла
        /// </summary>
        public string FilePath { set; get; }
        private List<ExcelColumn> Columns { set; get; }

        /// <summary>
        /// Возвращает заголовки столбцов
        /// </summary>
        private List<String> Headers
        {
            get
            {
                List<String> names = new List<String>();
                foreach (var column in this.Columns)
                {
                    names.Add(column.Title);
                }

                return names;
            }
        }
        /// <summary>
        /// Возвращает миннимальную длину из всех столбцов
        /// </summary>
        public int RowsCount
        {
            get
            {
                int min = Int32.MaxValue;
                foreach (var column in this.Columns)
                {
                    min = Math.Min(column.Rows.Count, min);
                }
                return min;
            }
        }
        public int CurrentRowIndex { get => this.currentRowIndex; set => this.currentRowIndex = value; }
        public int ReadCells => this.readCells;
        #endregion

        #region Методы
        public void Add(ExcelColumn column)
        {
            this.Columns.Add(column);
        }

        public ExcelColumn Get(string title)
        {
            return this.Columns.FirstOrDefault(x => x.Title == title);
        }

        public List<String> GetNextRow()
        {
            return this.GetRow(this.CurrentRowIndex++);
        }
        public List<String> GetNextRow(List<String> columnsNames)
        {
            return this.GetRow(this.CurrentRowIndex++, columnsNames);
        }
        public bool IsNextRowAvailiable()
        {
            return this.CurrentRowIndex < this.RowsCount && this.CurrentRowIndex >= 0;
        }
        public List<String> GetRow(int index)
        {
            if (index < this.RowsCount && index >= 0)
            {
                List<String> strings = new List<String>();
                foreach (var column in this.Columns)
                {
                    strings.Add(column.Rows[index]);
                }
                return strings;
            }
            return null;
        }
        public List<String> GetRow(int index, List<String> columnsNames)
        {
            if (index < this.RowsCount && index >= 0 && columnsNames != null)
            {
                List<String> strings = new List<String>();
                foreach (String column in columnsNames)
                {
                    if (this.Get(column) != null)
                    {
                        strings.Add(column);
                    }
                }
                return strings;
            }
            return null;
        }
        /// <summary>
        /// Удаляет столбцы с указанным названием
        /// </summary>
        /// <param name="title">имя столбца</param>
        /// <param name="ignoreCase">игрнорировать регистр</param>
        public void Remove(String title, bool ignoreCase = true)
        {
            for (int i = 0; i < this.Columns.Count; i++)
            {
                bool isDelete = false;
                if (ignoreCase)
                {
                    isDelete = this.Columns[i] != null && this.Columns[i].Title.ToLower() == title.ToLower();
                }
                else
                {
                    isDelete = this.Columns[i] != null && this.Columns[i].Title == title;
                }
                if (isDelete)
                {
                    this.Columns.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Генерирует скрипт для вставки значений в БД
        /// </summary>
        /// <returns></returns>
        public List<String> ToSQLScript(String[] dbTableColumnNames)
        {
            if (dbTableColumnNames == null)
            {
                return null;
            }
            return this.ToSQLScript(dbTableColumnNames.ToList());
        }
        /// <summary>
        /// Генерирует скрипт для вставки значений в БД
        /// </summary>
        /// <returns></returns>
        public List<String> ToSQLScript(List<String> dbTableColumnNames = null)
        {
            this.readCells = 0;
            if (dbTableColumnNames == null)
            {
                return null;
            }
            else
            {
                //Общие столбцы
                ExcelDocument documentCopy = new ExcelDocument(this, dbTableColumnNames.ToHashSet().Intersect(this.Headers.ToHashSet()).ToList())
                {
                    CurrentRowIndex = 0
                };
                List<String> queries = new List<string>();
                while (documentCopy.IsNextRowAvailiable())
                {
                    string query = string.Format("INSERT INTO dbo.[{0}] ", documentCopy.Title);
                    string columnsNames = string.Empty;
                    foreach (var column in documentCopy.Columns)
                    {
                        columnsNames += String.Format("[{0}],", column.Title);
                    }
                    columnsNames = String.Format("({0})", columnsNames.Substring(0, columnsNames.Length - 1));
                    query = String.Format("{0} {1}", query, columnsNames);
                    String valuesStr = String.Empty;
                    int valuesCount = 0;
                    int valuesLimit = 10;
                    for (List<String> row = documentCopy.GetNextRow(); row != null && valuesCount < valuesLimit; row = documentCopy.GetNextRow(), valuesCount++)
                    {
                        string subValuesStr = "";
                        foreach (var item in row)
                        {
                            subValuesStr += String.Format("'{0}',", item);
                        }
                        valuesStr += String.Format("({0}),", subValuesStr.Substring(0, subValuesStr.Length - 1));
                        this.readCells++;
                    }
                    if (valuesStr.Length > 0)
                    {
                        query = String.Format("{0} VALUES {1}", query, valuesStr.Substring(0, valuesStr.Length - 1));
                    }
                    queries.Add(query);
                }
                return queries;
            }
        }
        #endregion

        #region Конструкторы/Деструкторы
        public ExcelDocument()
        {
            this.Columns = new List<ExcelColumn>();
            this.CurrentRowIndex = 0;
            this.readCells = 0;
        }
        public ExcelDocument(string path) : this()
        {
            this.FilePath = path;
        }
        /// <summary>
        /// Создает копию книги с указанными столбцами
        /// </summary>
        /// <param name="excelDocument"></param>
        /// <param name="columnsNames"></param>
        public ExcelDocument(ExcelDocument excelDocument, List<String> columnsNames) : this(excelDocument.FilePath)
        {

            if (columnsNames != null)
            {
                foreach (String column in columnsNames)
                {
                    this.Add(excelDocument.Get(column));
                }
            }
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
