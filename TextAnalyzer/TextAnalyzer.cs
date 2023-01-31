using System;
using System.Collections.Generic;
using System.Data;

namespace TextAnalyzer
{
    public class Analyzer
    {

        #region Поля

        #endregion

        #region Свойства

        #endregion

        #region Методы
        /// <summary>
        /// Получает список слов в тексте в виде словаря.
        /// Ключом является слово, а значением - число вхождений слова в тексте
        /// </summary>
        /// <param name="text">исходный текст</param>
        /// <returns></returns>
        public Dictionary<string, AnalyzerItem> GetWordsList(string text, List<string> delimiters)
        {
            if (String.IsNullOrEmpty(text))
                throw new Exception("Не указан текст для составления словаря!");
            if (delimiters == null)
                throw new Exception("Не указан список разделителей!");
            Dictionary<string, AnalyzerItem> result = new Dictionary<string, AnalyzerItem>();
            var words = text.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (result.ContainsKey(word))
                {
                    result[word].Occurrences++;
                }
                else
                {
                    result[word] = new AnalyzerItem(key: word.ToLower(), value: word, occurrences: 1);
                }
            }
            return result;
        }

        public DataTable ToTable(Dictionary<string, AnalyzerItem> dictionary, bool isShowKey = false)
        {
            DataTable dataTable = new DataTable();
            if (isShowKey)
            {
                dataTable.Columns.Add("Ключ");
            }
            dataTable.Columns.Add("Слово");
            dataTable.Columns.Add("Числов вхождений в тексте");
            foreach (var item in dictionary)
            {
                if (isShowKey)
                {
                    dataTable.Rows.Add(new String[] { item.Value.Key, item.Value.Value, item.Value.Occurrences.ToString() });
                }
                else
                {
                    dataTable.Rows.Add(new String[] { item.Value.Value, item.Value.Occurrences.ToString() });
                }
            }
            return dataTable;
        }

        #endregion

        #region Конструкторы/Деструкторы

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}