using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CustomControlsWPF.Классы
{
    public class TreeViewItemCustom : TreeViewItem
    {

        #region Поля

        #endregion

        #region Свойства
        /// <summary>
        /// Тип хранимого значения
        /// </summary>
        public Type ValueType
        {
            get; set;
        }
        /// <summary>
        /// Хранимое значение
        /// </summary>
        public object Value
        {
            set; get;
        }
        #endregion

        #region Методы
        public void Add(TreeViewItem child)
        {
            _ = this.Items.Add(child);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public TreeViewItemCustom(string header, object value = null, TreeViewItem child = null)
        {
            this.Header = header;
            this.Value = value;
            this.ValueType = value.GetType();
            if (child != null)
            {
                _ = this.Items.Add(child);
            }
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
