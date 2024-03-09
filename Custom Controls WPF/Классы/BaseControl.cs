using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControlsWPF
{
    public enum Colors
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }
    /// <summary>
    /// Логика взаимодействия для BaseControl.xaml
    /// </summary>
    public abstract partial class BaseControl : UserControl
    {

        #region Поля
        public readonly Dictionary<Colors, SolidColorBrush> brushes;
        #endregion

        #region Свойства
        /// <summary>
        /// Подпись элемента управления.
        /// </summary>
        public abstract String Title
        {
            set; get;
        }
        /// <summary>
        /// Текст-значение внутри элмента управления.
        /// </summary>
        public abstract String Text
        {
            get; set;
        }
        /// <summary>
        /// Видимость элемента правления на форме.
        /// </summary>
        public new Visibility Visibility
        {
            get => base.Visibility;
            set => base.Visibility = value;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Создает кисть указаного цвета из 16-разрядной строки
        /// </summary>
        /// <param name="hexStr">Строка с 16-разрядным числом, например '#ffaacc'</param>
        /// <returns></returns>
        private SolidColorBrush GetColorBrush(String hexStr)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexStr.ToLower()));
        }
        #endregion

        #region Конструкторы/Деструкторы
        public BaseControl()
        {
            //InitializeComponent();
            this.brushes = new Dictionary<Colors, SolidColorBrush>() {
                { Colors.Primary,this.GetColorBrush("#ff0d6efd")},
                { Colors.Secondary,this.GetColorBrush("#6c757d")},
                { Colors.Success,this.GetColorBrush("#198754")},
                { Colors.Danger,this.GetColorBrush("#dc3545")},
                { Colors.Warning,this.GetColorBrush("#ffc107")},
                { Colors.Info,this.GetColorBrush("#0dcaf0")},
                { Colors.Light,this.GetColorBrush("#f8f9fa")},
                { Colors.Dark,this.GetColorBrush("#212529")}
            };
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
