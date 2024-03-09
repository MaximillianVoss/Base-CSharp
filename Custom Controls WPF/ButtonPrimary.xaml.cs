using System;
using System.Windows;
using System.Windows.Controls;

namespace CustomControlsWPF
{

    /// <summary>
    /// Логика взаимодействия для ButtonPrimary.xaml
    /// </summary>
    public partial class ButtonPrimary : BaseControl
    {
        #region Поля
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("ButtonPrimaryClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
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
        public override String Title
        {
            set => this.Text = value;
            get => this.Text;
        }
        public override String Text
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
                    this.btn.Background = this.brushes[Colors.Primary];
                    this.btn.Content = ex.Message;
                }
            }
        }

        #endregion

        #region Методы
        private void ClickHandler(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(ClickEvent));
        }
        #endregion

        #region Конструкторы/Деструкторы
        public ButtonPrimary() : base()
        {
            this.InitializeComponent();
            if (this.btn != null)
            {
                this.btn.Click += this.ClickHandler;
            }
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion
    }
}
