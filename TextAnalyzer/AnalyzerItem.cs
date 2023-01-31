using System;

namespace TextAnalyzer
{
    /// <summary>
    /// Описывает слово в словаре с дополнительными характеристиками помимо ключа
    /// </summary>
    public class AnalyzerItem
    {



        #region Поля

        #endregion

        #region Свойства
        /// <summary>
        /// Ключ
        /// </summary>
        public string Key { set; get; }
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { set; get; }
        /// <summary>
        /// Число вхождений в текст
        /// </summary>
        public int Occurrences { set; get; }
        #endregion

        #region Методы
        public override string ToString()
        {
            return String.Format("Ключ:'{0}' Слово:'{1}' Число вхождений в тексте:{2}", this.Key, this.Value, this.Occurrences);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public AnalyzerItem(string key, string value, int occurrences)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Occurrences = occurrences;
        }
        public AnalyzerItem(string value) : this("", value, 0)
        {
        }
        public AnalyzerItem() : this("", "", 0)
        {
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
