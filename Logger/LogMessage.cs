using System;

namespace Logger
{
    #region Сообщение
    /// <summary>
    /// Тип сообщения
    /// </summary>
    public enum MessageType
    {
        Message,
        Error,
        Warning
    }
    /// <summary>
    /// Сообщение лога
    /// </summary>
    public class LogMessage
    {
        #region Свойства
        /// <summary>
        /// Тип  сообщения
        /// </summary>
        public MessageType type { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Подробное описание
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Создает сообщение с указанными свойствами
        /// </summary>
        /// <param name="text">текст сообщения</param>
        /// <param name="description">подробное описание</param>
        /// <param name="createDate">время сообщения</param>
        public LogMessage(string text = "", string description = "", MessageType type = MessageType.Message, DateTime? createDate = null)
        {
            if (createDate == null)
            {
                this.CreateDate = DateTime.Now;
            }
            else
            {
                this.CreateDate = (DateTime)createDate;
            }
            this.Text = text;
            this.Description = description;
            this.type = type;
        }
        #endregion

        #region Методы
        public override string ToString()
        {
            return String.Format("{0}:'{1}'", this.CreateDate, this.Text);
        }
        #endregion
    }
    #endregion
}
