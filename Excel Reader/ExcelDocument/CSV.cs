using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace ExcelReader.CSVFile
{
    /// <summary>
    /// Тип разделителя
    /// </summary>
    public enum SeparatorType
    {
        /// <summary>
        /// запятая
        /// </summary>
        comma,
        /// <summary>
        /// точка с запятой
        /// </summary>
        colon
    }
    /// <summary>
    /// Содержит строки из файла .csv в виде объектов
    /// </summary>
    public class CSV
    {

        #region Поля

        #endregion

        #region Свойства
        /// <summary>
        /// Список полей
        /// </summary>
        private List<string> fields { set; get; }
        /// <summary>
        /// Список записей таблицы
        /// </summary>
        private List<CSVObject> items { set; get; }
        #endregion

        #region Методы
        /// <summary>
        /// Получает объект DataTable из текущей таблицы CSV
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            DataTable dataTable = new DataTable();
            foreach (var columnName in this.fields)
            {
                dataTable.Columns.Add(columnName);
            }
            foreach (var value in this.items)
            {
                dataTable.Rows.Add(value.GetFieldValues());
            }
            return dataTable;
        }
        #endregion

        #region Конструкторы/Деструкторы
        /// <summary>
        /// Создает объектную модель документа .csv
        /// </summary>
        /// <param title="path">путь до файла</param>
        /// <param title="separatorType">тип разделителя, по умолчанию точка с запятой</param>
        public CSV(String path, SeparatorType separatorType = SeparatorType.colon)
        {
            this.fields = new List<string>();
            this.items = new List<CSVObject>();
            using (var reader = new StreamReader(path))
            {
                for (int i = 0; !reader.EndOfStream; i++)
                {
                    string[] values = new string[0];
                    if (separatorType == SeparatorType.comma)
                    {
                        values = reader.ReadLine().Split(',');
                    }
                    if (separatorType == SeparatorType.colon)
                    {
                        values = reader.ReadLine().Split(';');
                    }
                    if (i == 0)
                    {
                        this.fields = values.ToList();
                    }
                    else
                    {
                        if (values.Count() == this.fields.Count)
                        {
                            CSVObject obj = new CSVObject();
                            for (int j = 0; j < this.fields.Count; j++)
                            {
                                obj.Add(new CSVField(this.fields[j], values[j]));
                            }
                            this.items.Add(obj);
                        }
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
