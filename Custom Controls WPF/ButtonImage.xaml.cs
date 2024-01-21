using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для ButtonImage.xaml
    /// </summary>
    public partial class ButtonImage : UserControl
    {
        #region Поля
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("ButtonImageClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        private readonly Dictionary<Colors, SolidColorBrush> brushes;
        #endregion

        #region Свойства
        public event RoutedEventHandler Click
        {
            add => this.AddHandler(ClickEvent, value);
            remove => this.RemoveHandler(ClickEvent, value);
        }
        public Double ButtonFontSize { set => this.btn.FontSize = value; get => this.btn.FontSize; }
        //public String Text { set => this.btn.Content = value; get => this.btn.Content.ToString(); }

        public String ImagePath
        {
            set => this.imgContent.Source = new BitmapImage(new Uri(value, UriKind.RelativeOrAbsolute));
            get
            {
                if (this.imgContent.Source is BitmapImage bitmapImage)
                {
                    return bitmapImage.UriSource?.ToString();
                }
                return null;
            }
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
        #endregion

        #region Конструкторы/Деструкторы
        public ButtonImage()
        {
            this.InitializeComponent();
            this.brushes = null;
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
