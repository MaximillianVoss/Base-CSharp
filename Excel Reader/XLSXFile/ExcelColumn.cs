using System;
using System.Collections.Generic;

namespace ExcelReader.XLSXFile
{
    public class ExcelColumn
    {



        #region Поля

        #endregion

        #region Свойства
        public string Title { set; get; }
        public string Description { set; get; }
        public List<string> Rows { set; get; }
        #endregion

        #region Методы
        public void Add(string item)
        {
            this.Rows.Add(item);
        }
        public string Get(int index)
        {
            if (index < 0 || index >= this.Rows.Count)
            {
                throw new Exception(String.Format("Некорректный индекс строки:{0} в столбце:{1}", index, this.Title));
            }

            return this.Rows[index];
        }
        #endregion

        #region Конструкторы/Деструкторы
        public ExcelColumn()
        {
            this.Rows = new List<string>();
        }

        public ExcelColumn(string title, string description) : this()
        {
            this.Title = title;
            this.Description = description;
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
