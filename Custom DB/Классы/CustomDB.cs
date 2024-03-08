using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;

namespace Custom_DB.Классы
{
    /// <summary>
    /// Специальный класс обертка для БД
    /// </summary>
    public class CustomDB<TContext> where TContext : DbContext, new()
    {
        #region Поля

        /// <summary>
        /// База данных
        /// </summary>
        private TContext db;

        /// <summary>
        /// Настройки
        /// </summary>
        private Settings settings;
        #endregion

        #region Свойства
        public Settings Settgins => this.settings;
        public TContext DB
        {
            get => this.db; private set => this.db = value;
        }
        /// <summary>
        /// Текущая строка подключения к базе данных.
        /// </summary>
        public ConnectionStringInfo CurrentConnectionString
        {
            get => this.settings.CurrentConnectionString;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Строка подключения не может быть null.");
                }

                string connectionStringName = this.GetConnectionStringName(value.Value);
                if (connectionStringName == null)
                {
                    throw new InvalidOperationException($"Строка подключения с содержимым '{value.Value}' не найдена.");
                }

                this.settings.CurrentConnectionString = value;
                this.InitDB(true); // Переинициализация БД с новой строкой подключения
            }
        }

        #endregion

        #region Методы

        #region SQL запросы
        private List<T> ReadData<T>(SqlCommand command) where T : class, new()
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                var result = new List<T>();
                System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();

                while (reader.Read())
                {
                    var item = new T();
                    foreach (System.Reflection.PropertyInfo property in properties)
                    {
                        string columnName = property.Name.Replace('_', ' ');
                        if (reader.GetOrdinal(columnName) >= 0 && !reader.IsDBNull(reader.GetOrdinal(columnName)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(reader[columnName], convertTo), null);
                        }
                    }
                    result.Add(item);
                }

                return result;
            }
        }
        /// <summary>
        /// Выполняет SQL запрос и возвращает результаты в виде списка объектов.
        /// </summary>
        /// <typeparam name="T">Тип объектов в списке.</typeparam>
        /// <param name="sqlQuery">SQL запрос для выполнения.</param>
        /// <returns>Список объектов типа T.</returns>
        /// <remarks>
        /// Этот метод открывает соединение с базой данных, используя текущую строку подключения,
        /// выполняет предоставленный SQL запрос и отображает результаты в список объектов указанного типа.
        /// Важно корректно обрабатывать входные данные для избежания SQL инъекций,
        /// особенно если запросы строятся на основе внешних данных.
        /// </remarks>
        /// <example>
        /// Пример использования:
        /// <code>
        /// var myData = customDbInstance.ExecuteSqlQuery<MyDataType>("SELECT * FROM MyTable");
        /// </code>
        /// </example>
        public List<T> ExecuteSqlQuery<T>(string sqlQuery) where T : class, new()
        {
            var entityBuilder = new EntityConnectionStringBuilder(this.CurrentConnectionString.Value);
            string sqlConnectionString = entityBuilder.ProviderConnectionString;

            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    return this.ReadData<T>(command);
                }
            }
        }

        public List<object> ExecuteSqlQuery(string sqlQuery, Type entityType)
        {
            var entityBuilder = new EntityConnectionStringBuilder(this.CurrentConnectionString.Value);
            string sqlConnectionString = entityBuilder.ProviderConnectionString;
            var resultList = new List<object>();

            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var props = entityType.GetProperties().Where(property => property.CanWrite).ToList();
                        while (reader.Read())
                        {
                            object item = Activator.CreateInstance(entityType);
                            foreach (System.Reflection.PropertyInfo property in props)
                            {
                                object val = reader[property.Name];
                                if (val != DBNull.Value)
                                {
                                    property.SetValue(item, val);
                                }
                            }
                            resultList.Add(item);
                        }
                    }
                }
            }
            return resultList;
        }


        public List<T> ExecuteSqlQuery<T>(string sqlQuery, IEnumerable<SqlParameter> parameters) where T : class, new()
        {
            var entityBuilder = new EntityConnectionStringBuilder(this.CurrentConnectionString.Value);
            string sqlConnectionString = entityBuilder.ProviderConnectionString;

            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    // Добавление параметров к команде
                    if (parameters != null)
                    {
                        foreach (SqlParameter param in parameters)
                        {
                            _ = command.Parameters.Add(param);
                        }
                    }

                    return this.ReadData<T>(command);
                }
            }
        }
        #endregion

        #region Строка подключения
        /// <summary>
        /// Обработчик события изменения строки подключения.
        /// Этот метод вызывается при обнаружении изменения строки подключения 
        /// в настройках. При этом происходит обновление контекста БД 
        /// с использованием новой строки подключения.
        /// </summary>
        /// <param name="newConnectionStringName">Новая строка подключения к БД.</param>
        private void HandleConnectionStringChange(string newConnectionStringName)
        {
            // Здесь ваш код обработки изменения строки подключения.
            // Например, вы можете вызвать InitDB или любой другой метод, 
            // который обновляет вашу базу данных с новой строкой подключения.
            this.InitDB(true);
        }
        private string GetConnectionStringByName(string name)
        {
            // Проверяем, существует ли строка подключения с таким именем
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            if (settings != null)
                return settings.ConnectionString;

            return null; // или выбросить исключение, если предпочитаете
        }
        public string GetConnectionStringName(string connectionString)
        {
            // Перебираем все строки подключения в конфигурации
            foreach (ConnectionStringSettings connectionStringSetting in ConfigurationManager.ConnectionStrings)
            {
                if (connectionStringSetting.ConnectionString == connectionString)
                {
                    return connectionStringSetting.Name; // Возвращаем имя, если нашли соответствующую строку
                }
            }

            return null; // Возвращаем null, если соответствующая строка подключения не найдена
        }
        #endregion

        #region Контекст БД
        /// <summary>
        /// Обновляет экземпляр контекста БД, создавая новый экземпляр <see cref="DBSEEntities"/>.
        /// Этот метод может быть использован для "сброса" текущего контекста 
        /// и начала работы с новым, чистым экземпляром.
        /// </summary>
        public void Update()
        {
            this.DB = new TContext();
        }


        /// <summary>
        /// Пересоздаем контекст БД в зависимости от выбранной строки подключения
        /// </summary>
        /// <param name="isForce">Обновить принудительно или нет</param>
        public void InitDB(bool isForce = false)
        {
            if (this.DB == null || isForce)
            {
                // Убедитесь, что ваш контекст данных имеет конструктор по умолчанию
                // или конструктор, принимающий строку подключения, если вы планируете использовать его.
                this.DB = new TContext();

                // Если у вашего контекста есть конструктор, принимающий строку подключения,
                // вы можете использовать следующий код:
                // Это предполагает, что у всех контекстов есть конструктор, принимающий строку подключения.

                var connectionString = this.CurrentConnectionString.Value;
                var context = (TContext)Activator.CreateInstance(typeof(TContext), connectionString);
                this.DB = context;

            }
        }

        #endregion

        #endregion

        #region Конструкторы/Деструкторы

        public CustomDB() : this(new Settings())
        {

        }

        public CustomDB(Settings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.settings.CurrentConnectionStringChanged += this.HandleConnectionStringChange;
            this.Update();
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion
    }
}
