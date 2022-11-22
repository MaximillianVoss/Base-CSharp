using System;

namespace ExcelReader.CSVFile
{
    /// <summary>
    /// Поле объекта из таблицы
    /// </summary>
    public class CSVField
    {

        #region Поля

        #endregion

        #region Свойства
        /// <summary>
        /// Название поля
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// Значение поля
        /// </summary>
        public string Value { set; get; }
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        /// <summary>
        /// Создает поле по умолчанию
        /// </summary>
        public CSVField()
        {

        }
        /// <summary>
        /// Создает поле с указанными параметрами
        /// </summary>
        /// <param name="title">название</param>
        /// <param name="value">значение</param>
        public CSVField(String title = "", String value = "", string description = "") : this()
        {
            this.Title = title;
            this.Value = value;
            this.Description = description;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
