using CSV_Reader.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExcelReader.ExcelDocument
{
    /// <summary>
    /// Содержит строки из файла .csv в виде объектов
    /// </summary>
    public class ExcelDocument
    {

        #region Поля
        private string path;
        #endregion

        #region Свойства
        /// <summary>
        /// Название документа без расширения
        /// </summary>
        public string Title => System.IO.Path.GetFileNameWithoutExtension(this.path);
        /// <summary>
        /// Путь до файла
        /// </summary>
        public string Path => this.path;
        /// <summary>
        /// Список колонок
        /// </summary>
        public List<ExcelField> Headers { set; get; }
        /// <summary>
        /// Список записей таблицы
        /// </summary>
        public List<ExcelObject> Rows { set; get; }
        /// <summary>
        /// Количество столбцов
        /// </summary>
        public int HeadersCount { get { if (this.Headers == null) { return 0; } return this.Headers.Count; } }
        /// <summary>
        /// Количество строк 
        /// </summary>
        public int RowsCount { get { if (this.Rows == null) { return 0; } return this.Rows.Count; } }
        #endregion

        #region Методы
        /// <summary>
        /// Добавляет объект как строку в документ. 
        /// Число полей должно совпадать.
        /// </summary>
        /// <param name="object"></param>
        /// <exception cref="Exception"></exception>
        public void Add(ExcelObject @object)
        {
            if (this.HeadersCount != @object.FieldsCount)
            {
                throw new Exception(Common.Strings.Errors.fieldsCountNotEqual);
            }
            this.Rows.Add(@object);
        }
        /// <summary>
        /// Добавляет заголовок в документ
        /// </summary>
        /// <param name="field"></param>
        public void Add(ExcelField field)
        {
            this.Headers.Add(field);
        }
        /// <summary>
        /// Проверяет находится ли указанное поле(столбец) в документе
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private bool IsContainsKey(string fieldName)
        {
            return this.Headers.Any(x => x.Title == fieldName);
        }
        /// <summary>
        /// Получает объект DataTable из текущей таблицы CSV
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            DataTable dataTable = new DataTable();
            foreach (var columnName in this.Headers)
            {
                dataTable.Columns.Add(columnName.Title);
            }
            foreach (var value in this.Rows)
            {
                dataTable.Rows.Add(value.GetFieldValues());
            }
            return dataTable;
        }
        public List<string> ToSQLScript(List<string> dbTableColumnNames, int startRowIndex = 0, int limit = 10)
        {
            List<string> queries = new List<string>();
            for (int i = startRowIndex; i < this.Rows.Count; i += limit)
            {
                string query = string.Format("INSERT INTO dbo.[{0}] ", this.Title);
                string columnsNames = string.Empty;
                foreach (var column in dbTableColumnNames)
                {
                    if (this.IsContainsKey(column))
                    {
                        columnsNames += String.Format("[{0}],", column);
                    }
                }
                if (columnsNames == string.Empty)
                {
                    return queries;
                }
                columnsNames = String.Format("({0})", columnsNames.Substring(0, columnsNames.Length - 1));
                query = String.Format("{0} {1}", query, columnsNames);
                string valuesStr = String.Empty;
                for (int j = 0; j < limit && i + j < this.Rows.Count; j++)
                {
                    string subValuesStr = String.Empty;
                    foreach (var column in dbTableColumnNames)
                    {
                        if (this.Rows[i + j].IsContainsKey(column))
                        {
                            subValuesStr += String.Format("'{0}',", this.Rows[i + j][column].Value);
                        }
                    }
                    valuesStr += String.Format("({0}),", subValuesStr.Substring(0, subValuesStr.Length - 1));
                }
                if (valuesStr.Length > 0)
                {
                    query = String.Format("{0} VALUES {1}", query, valuesStr.Substring(0, valuesStr.Length - 1));
                }
                queries.Add(query);
            }
            return queries;
        }
        #endregion

        #region Конструкторы/Деструкторы
        /// <summary>
        /// Создает объектную модель документа .csv
        /// </summary>
        /// <param Title="path">путь до файла</param>
        /// <param Title="separatorType">тип разделителя, по умолчанию точка с запятой</param>
        public ExcelDocument(String path)
        {
            this.path = path;
            this.Headers = new List<ExcelField>();
            this.Rows = new List<ExcelObject>();
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
