using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

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
        public Dictionary<string, AnalyzerItem> GetWordsList(string text, List<string> delimiters, Dictionary<string, List<string>> wordForms)
        {
            if (String.IsNullOrEmpty(text))
                throw new Exception("Не указан текст для составления словаря!");
            if (delimiters == null)
                throw new Exception("Не указан список разделителей!");
            if (wordForms == null)
                throw new Exception("Не переда словарь со склонениями слов!");
            var result = new Dictionary<string, AnalyzerItem>();
            string[] words = text.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            #region Инверсия словаря к виду склонение - базовое слово
            var wordFormsInversed = new Dictionary<string, string>();
            foreach (KeyValuePair<string, List<string>> basweWordItem in wordForms)
            {
                foreach (string declinationWord in basweWordItem.Value)
                {
                    if (!wordFormsInversed.ContainsKey(declinationWord.ToLower()))
                        wordFormsInversed.Add(declinationWord.ToLower(), basweWordItem.Key);
                }
            }

            #endregion
            foreach (string word in words)
            {
                string currentWord = word.ToLower();
                #region Для слова со склонением (не именительный падеж)
                if (wordFormsInversed.ContainsKey(word))
                {
                    currentWord = wordFormsInversed[word];
                }
                #endregion
                #region Для слова в именительном падаеже ед.ч.
                if (result.ContainsKey(currentWord))
                {
                    result[currentWord].Occurrences++;
                }
                else
                {
                    result[currentWord] = new AnalyzerItem(key: currentWord.ToLower(), value: currentWord, occurrences: 1);
                }
                #endregion
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="isShowKey"></param>
        /// <returns></returns>
        public DataTable ToTable(Dictionary<string, AnalyzerItem> dictionary, bool isShowKey = false)
        {
            var dataTable = new DataTable();
            if (isShowKey)
            {
                _ = dataTable.Columns.Add("Ключ");
            }
            _ = dataTable.Columns.Add("Слово");
            _ = dataTable.Columns.Add("Число вхождений в тексте");
            foreach (KeyValuePair<string, AnalyzerItem> item in dictionary)
            {
                if (isShowKey)
                {
                    _ = dataTable.Rows.Add(new String[] { item.Value.Key, item.Value.Value, item.Value.Occurrences.ToString() });
                }
                else
                {
                    _ = dataTable.Rows.Add(new String[] { item.Value.Value, item.Value.Occurrences.ToString() });
                }
            }
            return dataTable;
        }
        /// <summary>
        /// Выделяет клонения слов из указанного корпуса языка.
        /// Корпус русского языка можно взять здесь:
        /// http://opencorpora.org/?page=downloads
        /// </summary>
        /// <param name="path">путь до XML файла</param>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetWordsForm(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList list = doc.SelectNodes("annotation/text/paragraphs/paragraph/sentence/tokens/token");
            var wordsForms = new Dictionary<string, List<string>>();
            foreach (XmlNode node in list)
            {
                XmlNode subNode = node.SelectSingleNode("tfr/v/l");
                if (subNode != null)
                {
                    string currentStr = node.Attributes["text"].Value;
                    string baseStr = subNode.Attributes["t"].Value;
                    if (!wordsForms.ContainsKey(baseStr))
                    {
                        wordsForms[baseStr] = new List<string>();
                    }
                    if (!wordsForms[baseStr].Contains(currentStr))
                    {
                        wordsForms[baseStr].Add(currentStr);
                    }
                }
            }
            return wordsForms;
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