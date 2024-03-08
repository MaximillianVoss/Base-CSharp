using DevExpress.Data.Entity;
using System;
using System.IO;
using System.Text.Json;

namespace Custom_DB.Классы
{
    public class Settings
    {
        public const string DEFAULT_FILE_PATH = "settings.json";

        #region Поля
        private ConnectionStringInfo _currentConnectionString;
        private string _userNameWDClient;
        private string _passwordWDClient;
        private string _serverWDClient;
        private string _basePathWDClient;
        #endregion

        #region Свойства
        public ConnectionStringInfo CurrentConnectionString
        {
            get => this._currentConnectionString;
            set
            {
                if (this._currentConnectionString != value)
                {
                    this._currentConnectionString = value;
                    this.OnCurrentConnectionStringChanged(value.Name);
                }
            }
        }

        public string UserNameWDClient
        {
            get => this._userNameWDClient; set => this._userNameWDClient = value;
        }
        public string PasswordWDClient
        {
            get => this._passwordWDClient; set => this._passwordWDClient = value;
        }
        public string ServerWDClient
        {
            get => this._serverWDClient; set => this._serverWDClient = value;
        }
        public string BasePathWDClient
        {
            get => this._basePathWDClient; set => this._basePathWDClient = value;
        }

        public event Action<string> CurrentConnectionStringChanged;
        #endregion

        #region Методы

        public void SaveToFile(string filePath)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(this, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);
                    Settings settings = JsonSerializer.Deserialize<Settings>(jsonString);
                    if (settings != null)
                    {
                        this._currentConnectionString = settings.CurrentConnectionString;
                        this._userNameWDClient = settings.UserNameWDClient;
                        this._passwordWDClient = settings.PasswordWDClient;
                        this._serverWDClient = settings.ServerWDClient;
                        this._basePathWDClient = settings.BasePathWDClient;

                        this.OnCurrentConnectionStringChanged(this._currentConnectionString?.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        #endregion

        #region Конструкторы/Деструкторы
        public Settings()
        {
            // Загрузка настроек по умолчанию или какая-либо другая логика
        }

        public Settings(string filePath)
        {
            if (!File.Exists(filePath))
            {
                this.SaveToFile(filePath);
            }
            else
            {
                this.LoadFromFile(filePath);
            }
        }
        #endregion

        #region Операторы
        #endregion

        #region Обработчики событий
        private void OnCurrentConnectionStringChanged(string newConnectionStringName)
        {
            CurrentConnectionStringChanged?.Invoke(newConnectionStringName);
        }
        #endregion
    }
}
