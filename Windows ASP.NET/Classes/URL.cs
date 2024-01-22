using System;
using System.Collections.Generic;
using System.Linq;

namespace Windows_ASP.NET.Classes
{
    public class URLParam
    {

        #region Поля

        #endregion

        #region Свойства
        private string Title
        {
            set; get;
        }
        private string Value
        {
            set; get;
        }
        #endregion

        #region Методы
        public override string ToString()
        {
            return String.Format("{0}={1}", this.Title, this.Value);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public URLParam(string title, string value)
        {
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// Создает параметр из строки
        /// </summary>
        /// <param name="str">строка вида TITLE=VALUE</param>
        public URLParam(string str)
        {
            var items = str.Split('=').ToList();
            if (items.Count != 2)
                throw new Exception("Передана некорректная строка!");
            this.Title = items[0];
            this.Value = items[1];
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
    public class URL
    {
        #region Поля
        private string urlBase;
        #endregion

        #region Свойства
        private string UrlBase
        {
            set
            {
                var subItems = value.Split('?').ToList();
                if (subItems.Count > 2)
                    throw new Exception("В строке запроса не может быть несколько знаков '?'");
                if (subItems.Count == 0)
                {
                    this.urlBase = value;
                }
                if (subItems.Count == 2)
                {
                    this.urlBase = subItems[0];
                    var paramsStr = subItems[1].Split('&').ToList();
                    foreach (string item in paramsStr)
                        this.Params.Add(new URLParam(item));
                }
            }
            get => this.UrlBase;
        }
        private List<URLParam> Params
        {
            set; get;
        }
        public string Url
        {
            get
            {
                string result = this.UrlBase + "?";
                foreach (URLParam param in this.Params)
                {
                    result += param.ToString() + "&";
                }
                result = result.Substring(0, result.Length - 1);
                return result;
            }
            set => this.UrlBase = value;
        }
        #endregion

        #region Методы
        public void AddParam(string title, string value)
        {
            this.AddParam(new URLParam(title, value));
        }
        public void AddParam(URLParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));
            this.Params.Add(param);
        }
        public override string ToString()
        {
            return this.Url;
        }
        #endregion

        #region Конструкторы/Деструкторы
        public URL(string url)
        {
            this.UrlBase = url ?? throw new ArgumentNullException(nameof(url));
        }
        public URL() : this("")
        {

        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}