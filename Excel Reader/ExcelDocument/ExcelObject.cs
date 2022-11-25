using CSV_Reader.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelReader.ExcelDocument
{
    public class ExcelObject
    {

        #region Поля

        #endregion

        #region Свойства
        public int Id { set; get; }
        /// <summary>
        /// Поля объекта
        /// </summary>
        public List<ExcelField> Fields { set; get; }
        /// <summary>
        /// Количество полей
        /// </summary>
        public int FieldsCount { get { if (Fields == null) { return 0; } return Fields.Count; } }
        #endregion

        #region Методы
        public void Add(ExcelField field)
        {
            this.Fields.Add(field);
        }
        public void Set(string fieldName, string value)
        {
            var field = this.Fields.FirstOrDefault(x => x.Title == fieldName);
            if (field == null)
            {
                throw new Exception(String.Format("Указанное поле '{0}' не найдено!", fieldName));
            }
            field.Value = value;
        }
        public void Set(string fieldName, ExcelField field)
        {
            this.Set(fieldName, field.Value);
        }
        public ExcelField Get(string fieldName)
        {
            return this.Fields.FirstOrDefault(x => x.Title == fieldName);
        }
        public string[] GetFieldValues()
        {
            string[] values = new string[this.Fields.Count];
            for (int i = 0; i < this.Fields.Count; i++)
            {
                values[i] = this.Fields[i].Value;
            }

            return values;
        }
        public bool IsContainsKey(string fieldName)
        {
            return this.Fields.Any(x => x.Title == fieldName);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public ExcelObject(int id = 0)
        {
            this.Id = id;
            this.Fields = new List<ExcelField>();
        }
        public ExcelObject(List<ExcelField> headers, List<string> values) : this()
        {
            if (values.Count != headers.Count)
            {
                throw new Exception(Common.Strings.Errors.fieldsValuesCountNotMatch);
            }
            for (int j = 0; j < values.Count; j++)
            {
                this.Add(new ExcelField(headers[j].Title, values[j], headers[j].Description));
            }

            this.Fields = this.Fields;
        }


        #endregion

        #region Операторы
        public ExcelField this[string fieldName]
        {
            get => this.Get(fieldName);
            set => this.Set(fieldName, value);
        }
        #endregion

        #region Обработчики событий

        #endregion

    }
}
