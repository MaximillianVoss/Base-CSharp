using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;

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
        private ObservableCollection<LogMessage> messages;
        /// <summary>
        /// Мьютек для контроля коллекции
        /// </summary>
        Mutex mutex = new Mutex();
        /// <summary>
        /// Запоминает, изменялась коллекция или нет
        /// </summary>
        bool isChanged;
        #endregion

        #region Свойства
        /// <summary>
        /// Сообщения
        /// </summary>
        public ObservableCollection<LogMessage> Messages { get => this.messages; }
        /// <summary>
        /// Таблица с сообщениями
        /// </summary>
        public DataTable Table
        {
            get
            {
                return this.ToTable();
            }
        }
        /// <summary>
        /// Получает текущее число сообщений
        /// </summary>
        public int MessagesCount
        {
            get
            {
                return this.messages == null ? 0 : this.messages.Count;
            }
        }
        /// <summary>
        /// Указывает добавлялись ли новые сообщения
        /// </summary>
        public bool IsChanged
        {
            get
            {

                return this.isChanged;
            }
        }
        #endregion

        #region Методы
        /// <summary>
        /// Добавляет сообщение в лог
        /// </summary>
        /// <param name="message">сообщение</param>
        public void Add(string message)
        {
            if (message == null)
            {
                throw new Exception("Передано пустое сообщение!");
            }
            this.Add(new LogMessage(message, "", MessageType.Message, DateTime.Now));
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Add(string format, params object[] args)
        {
            Add(String.Format(format, args));
        }
        /// <summary>
        /// Добавляет сообщение в логДобавляет сообщение в лог
        /// </summary>
        /// <param name="message">сообщение</param>
        public void Add(LogMessage message)
        {
            if (message == null)
            {
                throw new Exception("Передано пустое сообщение!");
            }
            this.mutex.WaitOne();
            this.messages.Add(message);
            this.mutex.ReleaseMutex();
        }
        /// <summary>
        /// Очищает лог
        /// </summary>
        public void Clear()
        {
            this.mutex.WaitOne();
            messages.Clear();
            this.mutex.ReleaseMutex();
        }
        /// <summary>
        /// Получает лог в качестве таблицы
        /// Столбцы: дата сообщения, текст, описание, тип
        /// </summary>
        /// <returns></returns>
        public DataTable ToTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Дата сообщения");
            dataTable.Columns.Add("Текст");
            dataTable.Columns.Add("Описание");
            dataTable.Columns.Add("Тип");
            for (int i = 0; i < messages.Count; i++)
            {
                dataTable.Rows.Add(new object[] { messages[i].CreateDate, messages[i].Text, messages[i].Description, messages[i].type });
            }
            return dataTable;
        }
        #endregion

        #region Конструкторы/Деструкторы
        /// <summary>
        /// Создает лог с пустыми сообщениями
        /// </summary>
        public Log()
        {
            this.messages = new ObservableCollection<LogMessage>();
            this.messages.CollectionChanged += messages_CollectionChanged;
        }
        /// <summary>
        /// Создает лог с указанными сообщениями
        /// </summary>
        /// <param name="messages">сообщения</param>
        public Log(ObservableCollection<LogMessage> messages) : this()
        {
            this.messages = messages;
        }
        /// <summary>
        /// Создает лог с указанными сообщениями
        /// </summary>
        /// <param name="messages">сообщения</param>
        public Log(List<LogMessage> messages) : this()
        {
            foreach (LogMessage message in messages)
            {
                this.messages.Add(message);
            }
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        void messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //list changed - an item was added.
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //this.isChanged = true;
            }
            else
            {
                //this.messages.
            }
        }
        #endregion

    }
    #endregion
}
