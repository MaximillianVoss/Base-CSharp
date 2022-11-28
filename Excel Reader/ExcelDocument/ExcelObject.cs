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
        public int FieldsCount { get { if (this.Fields == null) { return 0; } return this.Fields.Count; } }
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
        /// <summary>
        /// Проверяет, содержит ли объект указанное поле
        /// </summary>
        /// <param name="fieldName">имя поля</param>
        /// <returns></returns>
        public bool IsContainsKey(string fieldName)
        {
            return this.Fields.Any(x => x.Title == fieldName);
        }
        /// <summary>
        /// Получает словарь, 
        /// где ключом является название поля,
        /// а значением - значение поля
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> ToDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var field in this.Fields)
            {
                result.Add(field.Title, field.Value);
            }

            return result;
        }
        /// <summary>
        /// Получает список значений полей
        /// </summary>
        /// <returns></returns>
        public List<string> ToList()
        {
            List<string> result = new List<string>();
            foreach (var field in this.Fields)
            {
                result.Add(field.Value);
            }

            return result;
        }
        /// <summary>
        /// Получаем массив значений полей
        /// </summary>
        /// <returns></returns>
        public string[] ToArray()
        {
            return this.ToList().ToArray();
        }
        #endregion

        #region Конструкторы/Деструкторы
        public ExcelObject(int id = 0)
        {
            this.Id = id;
            this.Fields = new List<ExcelField>();
        }
        public ExcelObject(int id, List<ExcelField> fields) : this(id)
        {
            this.Fields = fields;
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
