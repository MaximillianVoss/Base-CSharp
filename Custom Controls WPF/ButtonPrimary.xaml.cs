using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace CustomControlsWPF
{
    public enum Colors
    {
        primary,
        secondary,
        success,
        danger,
        warning,
        info,
        light,
        dark
    }
    /// <summary>
    /// Логика взаимодействия для ButtonPrimary.xaml
    /// </summary>
    public partial class ButtonPrimary : UserControl
    {
        #region Поля
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("ButtonPrimaryClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        private readonly Dictionary<Colors, SolidColorBrush> brushes;
        #endregion

        #region Свойства
        public event RoutedEventHandler Click
        {
            add => this.AddHandler(ClickEvent, value);
            remove => this.RemoveHandler(ClickEvent, value);
        }
        public Double ButtonFontSize
        {
            set => this.btn.FontSize = value; get => this.btn.FontSize;
        }
        public String Text
        {
            set => this.btn.Content = value; get => this.btn.Content.ToString();
        }
        public Colors Color
        {
            set
            {
                try
                {
                    this.btn.Background = this.brushes[value];
                }
                catch (Exception ex)
                {
                    this.btn.Background = this.brushes[Colors.primary];
                    this.btn.Content = ex.Message;
                }
            }
        }

        #endregion

        #region Методы
        private void ClickHadler(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ClickEvent));
        }

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
        public ButtonPrimary()
        {
           
            this.brushes = new Dictionary<Colors, SolidColorBrush>() {
                { Colors.primary,this.GetColorBrush("#ff0d6efd")},
                { Colors.secondary,this.GetColorBrush("#6c757d")},
                { Colors.success,this.GetColorBrush("#198754")},
                { Colors.danger,this.GetColorBrush("#dc3545")},
                { Colors.warning,this.GetColorBrush("#ffc107")},
                { Colors.info,this.GetColorBrush("#0dcaf0")},
                { Colors.light,this.GetColorBrush("#f8f9fa")},
                { Colors.dark,this.GetColorBrush("#212529")}
            };
            this.InitializeComponent();
            if (this.btn != null)
            {
                this.btn.Click += this.ClickHadler;
            }
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion
    }
}
