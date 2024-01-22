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
        public List<ExcelField> Headers
        {
            set; get;
        }
        /// <summary>
        /// Список записей таблицы
        /// </summary>
        public List<ExcelObject> Rows
        {
            set; get;
        }
        /// <summary>
        /// Количество столбцов
        /// </summary>
        public int HeadersCount
        {
            get
            {
                if (this.Headers == null)
                {
                    return 0;
                }
                return this.Headers.Count;
            }
        }
        /// <summary>
        /// Количество строк 
        /// </summary>
        public int RowsCount
        {
            get
            {
                if (this.Rows == null)
                {
                    return 0;
                }
                return this.Rows.Count;
            }
        }
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
        /// <param name="field">поле</param>
        public void Add(ExcelField field)
        {
            this.Headers.Add(field);
            for (int i = 0; i < this.Rows.Count; i++)
            {
                this.Rows[i].Add(new ExcelField(field));
            }
        }
        /// <summary>
        /// Добавляет заголовок в документ
        /// </summary>
        /// <param name="fieldName">имя поля</param>
        public void Add(string fieldName)
        {
            this.Add(new ExcelField(title: fieldName));
        }
        /// <summary>
        /// Удаляет указанное поле из документа
        /// </summary>
        /// <param name="field">поле</param>
        public void Remove(ExcelField field)
        {
            _ = this.Headers.RemoveAll(x => x.Title == field.Title);
            for (int i = 0; i < this.Rows.Count; i++)
            {
                this.Rows[i].Remove(field);
            }
        }
        /// <summary>
        /// Удаляет указанное поле из документа
        /// </summary>
        /// <param name="fieldName">имя поля</param>
        public void Remove(string fieldName)
        {
            _ = this.Headers.RemoveAll(x => x.Title == fieldName);
            for (int i = 0; i < this.Rows.Count; i++)
            {
                this.Rows[i].Remove(new ExcelField(title: fieldName));
            }
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
            var dataTable = new DataTable();
            foreach (ExcelField columnName in this.Headers)
            {
                _ = dataTable.Columns.Add(columnName.Title);
            }
            foreach (ExcelObject value in this.Rows)
            {
                _ = dataTable.Rows.Add(value.ToArray());
            }
            return dataTable;
        }

        public List<string> ToSQLScript_OLD(List<string> dbTableColumnNames, string tableName, int startRowIndex = 0, int limit = 10)
        {
            var queries = new List<string>();
            for (int i = startRowIndex; i < this.Rows.Count; i += limit)
            {
                string query = string.Format("INSERT INTO dbo.[{0}] ", tableName);
                string columnsNames = string.Empty;
                // string columnsNamesOutput = string.Empty;
                string columnsNamesOutput = String.Format("Inserted.[{0}],", "id");
                foreach (string column in dbTableColumnNames)
                {
                    if (this.IsContainsKey(column))
                    {
                        columnsNames += String.Format("[{0}],", column);
                        columnsNamesOutput += String.Format("Inserted.[{0}],", column);
                    }
                }
                if (String.IsNullOrEmpty(columnsNames))
                {
                    return queries;
                }
                columnsNames = columnsNames.Substring(0, columnsNames.Length - 1);
                if (!String.IsNullOrEmpty(columnsNamesOutput))
                {
                    columnsNamesOutput = columnsNamesOutput.Substring(0, columnsNamesOutput.Length - 1);
                }

                columnsNames = String.Format("({0}) OUTPUT {1}", columnsNames, columnsNamesOutput);
                query = String.Format("{0} {1}", query, columnsNames);
                string valuesStr = String.Empty;
                for (int j = 0; j < limit && i + j < this.Rows.Count; j++)
                {
                    string subValuesStr = String.Empty;
                    foreach (string column in dbTableColumnNames)
                    {
                        if (this.Rows[i + j].IsContainsKey(column))
                        {
                            string cellStrValue = this.Rows[i + j][column].Value;
                            if (cellStrValue != String.Empty)
                            {
                                subValuesStr += String.Format("'{0}',", cellStrValue);
                            }
                            else
                            {
                                subValuesStr += String.Format("{0},", "NULL");
                            }
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

        public List<string> ToSQLScript(List<string> dbTableColumnNames, string tableName, int startRowIndex = 0, int limit = 10)
        {
            var queries = new List<string>();
            for (int i = startRowIndex; i < this.Rows.Count; i += limit)
            {
                string query = string.Format("INSERT INTO dbo.[{0}] ", tableName);
                string columnsNames = string.Empty;
                string columnsNamesOutput = String.Format("Inserted.[{0}],", "id");

                string outputColumns = String.Format("[{0}] {1},", "id", "int");

                foreach (string column in dbTableColumnNames)
                {
                    if (this.IsContainsKey(column))
                    {
                        columnsNames += String.Format("[{0}],", column);
                        columnsNamesOutput += String.Format("Inserted.[{0}],", column);
                        outputColumns += String.Format("[{0}] {1},", column, "varchar(2048)");
                    }
                }

                if (String.IsNullOrEmpty(columnsNames))
                {
                    return queries;
                }
                columnsNames = columnsNames.Substring(0, columnsNames.Length - 1);
                if (!String.IsNullOrEmpty(columnsNamesOutput))
                {
                    columnsNamesOutput = columnsNamesOutput.Substring(0, columnsNamesOutput.Length - 1);
                }

                string outputTableDeclaration = String.Format("DECLARE @OutputTable TABLE ({0})", outputColumns.Substring(0, outputColumns.Length - 1));

                columnsNames = String.Format("({0}) OUTPUT {1}", columnsNames, columnsNamesOutput);
                query = String.Format("{0} {1}", query, columnsNames);
                ///query = String.Format("{0} {1}", query, columnsNames);
                string valuesStr = String.Empty;
                for (int j = 0; j < limit && i + j < this.Rows.Count; j++)
                {
                    string subValuesStr = String.Empty;
                    foreach (string column in dbTableColumnNames)
                    {
                        if (this.Rows[i + j].IsContainsKey(column))
                        {
                            string cellStrValue = this.Rows[i + j][column].Value;
                            if (cellStrValue != String.Empty)
                            {
                                subValuesStr += String.Format("'{0}',", cellStrValue);
                            }
                            else
                            {
                                subValuesStr += String.Format("{0},", "NULL");
                            }
                        }
                    }
                    valuesStr += String.Format("({0}),", subValuesStr.Substring(0, subValuesStr.Length - 1));
                }
                query = String.Format("{0} INTO @OutputTable ", query);
                if (valuesStr.Length > 0)
                {
                    query = String.Format("{0} VALUES {1}", query, valuesStr.Substring(0, valuesStr.Length - 1));
                }
                query = String.Format("{0} SELECT * FROM @OutputTable;", query);
                queries.Add(outputTableDeclaration + "\n" + query);
            }
            return queries;
        }


        public List<string> ToSQLScript(List<string> dbTableColumnNames, int startRowIndex = 0, int limit = 10)
        {
            return this.ToSQLScript(dbTableColumnNames, this.Title);
        }
        public void Sort(string fieldName = "", bool isAscending = true)
        {
            if (!String.IsNullOrEmpty(fieldName))
            {
                this.Rows.Sort(
                    delegate (ExcelObject l, ExcelObject r)
                    {
                        if (l[fieldName] == null && l[fieldName] == null)
                        {
                            return 0;
                        }
                        else if (l[fieldName] == null)
                        {
                            return -1;
                        }
                        else if (r[fieldName] == null)
                        {
                            return 1;
                        }
                        else
                        {
                            if (isAscending)
                            {
                                return l[fieldName].Value.CompareTo(r[fieldName].Value);
                            }
                            else
                            {
                                return r[fieldName].Value.CompareTo(l[fieldName].Value);
                            }
                        }
                    });
            }
            else
            {
                this.Rows.Sort(
                   delegate (ExcelObject l, ExcelObject r)
                   {
                       if (l == null && l == null)
                       {
                           return 0;
                       }
                       else if (l == null)
                       {
                           return -1;
                       }
                       else if (r == null)
                       {
                           return 1;
                       }
                       if (isAscending)
                       {
                           return l.Id - r.Id;
                       }
                       else
                       {
                           return r.Id - l.Id;
                       }
                   });
            }
        }
        #endregion

        #region Конструкторы/Деструкторы
        public ExcelDocument(string path, List<ExcelField> headers, List<ExcelObject> rows)
        {
            this.path = path ?? throw new ArgumentNullException(nameof(path));
            this.Headers = headers ?? throw new ArgumentNullException(nameof(headers));
            this.Rows = rows ?? throw new ArgumentNullException(nameof(rows));
        }
        /// <summary>
        /// Создает объектную модель документа .csv
        /// </summary>
        /// <param Title="path">путь до файла</param>
        /// <param Title="separatorType">тип разделителя, по умолчанию точка с запятой</param>
        public ExcelDocument(String path) : this(path, new List<ExcelField>(), new List<ExcelObject>())
        {

        }
        public ExcelDocument(List<ExcelField> fields) : this(String.Empty, fields, new List<ExcelObject>())
        {

        }
        public ExcelDocument(List<string> fields) : this(String.Empty, new List<ExcelField>(), new List<ExcelObject>())
        {
            foreach (string fieldsName in fields)
            {
                this.Headers.Add(new ExcelField(fieldsName));
            }
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
