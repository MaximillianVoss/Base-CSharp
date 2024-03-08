using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Custom_DB.Классы
{
    /// <summary>
    /// Представляет информацию о строке подключения к базе данных.
    /// </summary>
    public class ConnectionStringInfo
    {
        #region Поля
        private string _value;
        #endregion

        #region Свойства

        /// <summary>
        /// Получает или задает название строки подключения.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Получает или задает значение строки подключения.
        /// </summary>
        public string Value
        {
            get => this._value;
            set
            {
                if (!this.IsValidConnectionString(value))
                {
                    throw new ArgumentException("Недопустимая строка подключения Entity Framework");
                }
                this._value = value;
            }
        }

        #endregion

        #region Конструкторы/Деструкторы

        /// <summary>
        /// Инициализирует новый экземпляр класса ConnectionStringInfo с указанными названием и значением строки подключения.
        /// </summary>
        /// <param name="name">Название строки подключения.</param>
        /// <param name="value">Значение строки подключения.</param>
        public ConnectionStringInfo(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверяет, соответствует ли заданная строка подключения формату строки подключения Entity Framework.
        /// </summary>
        /// <param name="connectionString">Строка подключения для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если строка подключения соответствует ожидаемому формату; в противном случае возвращает <c>false</c>.</returns>
        /// <remarks>
        /// Данный метод проверяет строку подключения на соответствие специфическому формату, используемому в Entity Framework,
        /// включая наличие метаданных, указания провайдера и строки подключения провайдера.
        /// </remarks>
        public bool IsValidConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return false;

            string regexPattern = @"^metadata=res:\/\/\*\/.*\.csdl\|res:\/\/\*\/.*\.ssdl\|res:\/\/\*\/.*\.msl;provider=System\.Data\.SqlClient;provider connection string=""[^""]*""$";
            return Regex.IsMatch(connectionString, regexPattern);
        }


        #endregion

        #region Операторы
        #endregion

        #region Обработчики событий
        #endregion
    }
}
