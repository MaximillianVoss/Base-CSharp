using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelReader.CSVFile
{
    public class CSVObject
    {

        #region Поля

        #endregion

        #region Свойства
        private List<CSVField> fields { set; get; }

        #endregion

        #region Методы
        public void Add(CSVField field)
        {
            this.fields.Add(field);
        }
        public void Set(string fieldName, string value)
        {
            var field = this.fields.FirstOrDefault(x => x.Title == fieldName);
            if (field == null)
            {
                throw new Exception(String.Format("Указанное поле '{0}' не найдено!", fieldName));
            }
            field.Value = value;
        }
        public void Set(string fieldName, CSVField field)
        {
            this.Set(fieldName, field.Value);
        }
        public CSVField Get(string fieldName)
        {
            return this.fields.FirstOrDefault(x => x.Title == fieldName);
        }
        public string[] GetFieldValues()
        {
            string[] values = new string[this.fields.Count];
            for (int i = 0; i < this.fields.Count; i++)
            {
                values[i] = this.fields[i].Value;
            }

            return values;
        }
        public bool IsContainsKey(string fieldName)
        {
            return this.fields.Any(x => x.Title == fieldName);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public CSVObject()
        {
            this.fields = new List<CSVField>();
        }
        public CSVObject(List<CSVField> headers, List<string> values) : this()
        {
            if (values.Count == headers.Count)
            {
                for (int j = 0; j < values.Count; j++)
                {
                    this.Add(new CSVField(headers[j].Title, values[j], headers[j].Description));
                }
            }
            else
            {
                throw new Exception("Количество полей не совпадает с количество значений для полей");
            }

            this.fields = this.fields;
        }


        #endregion

        #region Операторы
        public CSVField this[string fieldName]
        {
            get => this.Get(fieldName);
            set => this.Set(fieldName, value);
        }
        #endregion

        #region Обработчики событий

        #endregion

    }
}
