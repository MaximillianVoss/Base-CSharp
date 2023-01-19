using System;

namespace ExcelReader.ExcelDocument
{
    /// <summary>
    /// Поле объекта из таблицы
    /// </summary>
    public class ExcelField
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
        /// Создает поле с указанными параметрами
        /// </summary>
        /// <param name="title">название</param>
        /// <param name="value">значение</param>
        public ExcelField(String title = "", String value = "", string description = "")
        {
            this.Title = title;
            this.Value = value;
            this.Description = description;
        }
        public ExcelField(ExcelField excelField) : this(title: excelField.Title, value: excelField.Value, description: excelField.Description)
        {

        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
