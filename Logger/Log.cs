using System.Collections.Generic;

namespace Logger
{

    #region Логгер сообщений
    /// <summary>
    /// Лог для отладочных сообщений или событий программы
    /// </summary>
    public class Log
    {

        #region Поля
        /// <summary>
        /// Сообщения
        /// </summary>
        private List<LogMessage> messages;
        #endregion

        #region Свойства
        /// <summary>
        /// Сообщения
        /// </summary>
        public List<LogMessage> Messages { get => this.messages; }
        #endregion

        #region Методы
        /// <summary>
        /// Добавляет сообщение в лог
        /// </summary>
        /// <param name="message">сообщение</param>
        public void Add(string message)
        {
            this.messages.Add(new LogMessage(message));
        }
        /// <summary>
        /// Добавляет сообщение в логДобавляет сообщение в лог
        /// </summary>
        /// <param name="message">сообщение</param>
        public void Add(LogMessage message)
        {
            this.messages.Add(message);
        }
        /// <summary>
        /// Добавляет сообщения в лог
        /// </summary>
        /// <param name="messages">сообщения</param>
        public void Add(List<string> messages)
        {
            foreach (string message in messages)
            {
                this.Add(message);
            }
        }
        #endregion

        #region Конструкторы/Деструкторы
        /// <summary>
        /// Создает лог с пустыми сообщениями
        /// </summary>
        public Log()
        {
            this.messages = new List<LogMessage>();
        }
        /// <summary>
        /// Создает лог с указанными сообщениями
        /// </summary>
        /// <param name="messages">сообщения</param>
        public Log(List<LogMessage> messages) : this()
        {
            this.messages = messages;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
    #endregion
}
