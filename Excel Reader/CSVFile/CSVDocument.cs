using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace ExcelReader.CSVFile
{
    /// <summary>
    /// Содержит строки из файла .csv в виде объектов
    /// </summary>
    public class CSVDocument
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
        private List<CSVField> headers { set; get; }
        /// <summary>
        /// Список записей таблицы
        /// </summary>
        private List<CSVObject> rows { set; get; }

        #endregion

        #region Методы
        private void Add(CSVObject @object)
        {
            this.rows.Add(@object);
        }
        private bool IsContainsKey(string fieldName)
        {
            return this.headers.Any(x => x.Title == fieldName);
        }
        /// <summary>
        /// Получает объект DataTable из текущей таблицы CSV
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            DataTable dataTable = new DataTable();
            foreach (var columnName in this.headers)
            {
                dataTable.Columns.Add(columnName.Title);
            }
            foreach (var value in this.rows)
            {
                dataTable.Rows.Add(value.GetFieldValues());
            }
            return dataTable;
        }
        public List<string> ToSQLScript(List<string> dbTableColumnNames, int startRowIndex = 0, int limit = 10)
        {
            List<string> queries = new List<string>();
            for (int i = startRowIndex; i < this.rows.Count; i += limit)
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
                for (int j = 0; j < limit && i + j < this.rows.Count; j++)
                {
                    string subValuesStr = String.Empty;
                    foreach (var column in dbTableColumnNames)
                    {
                        if (this.rows[i + j].IsContainsKey(column))
                        {
                            subValuesStr += String.Format("'{0}',", this.rows[i + j][column].Value);
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
        public CSVDocument(String path, bool isHasHeaders = true, char separator = ';')
        {
            this.path = path;
            this.headers = new List<CSVField>();
            this.rows = new List<CSVObject>();
            using (var reader = new StreamReader(path))
            {
                for (int i = 0; !reader.EndOfStream; i++)
                {
                    List<string> values = new List<string>();
                    values = reader.ReadLine().Split(separator).ToList();
                    if (i == 0)
                    {
                        if (isHasHeaders)
                        {
                            foreach (var value in values)
                            {
                                this.headers.Add(new CSVField(value));
                            }
                        }
                        else
                        {
                            for (int j = 0; j < values.Count; j++)
                            {
                                this.headers.Add(new CSVField(String.Format("Столбец #{0}", j), String.Empty, "Это автоматически сгенерированное название столбца"));
                            }
                            this.Add(new CSVObject(this.headers, values));
                        }
                    }
                    else
                    {
                        this.Add(new CSVObject(this.headers, values));
                    }
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
