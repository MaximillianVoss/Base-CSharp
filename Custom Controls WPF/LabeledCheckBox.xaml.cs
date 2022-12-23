using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для LabeledCheckBox.xaml
    /// </summary>
    public partial class LabeledCheckBox : UserControl
    {

        #region Поля

        #endregion

        #region Свойства
        public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent("LabeledCheckBoxChecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        public event RoutedEventHandler Checked
        {
            add => this.AddHandler(CheckedEvent, value);
            remove => this.RemoveHandler(CheckedEvent, value);
        }
        private void CheckedHandler(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(CheckedEvent));
        }
        public string Title
        {
            set => this.lblTitle.Content = value;
            get => this.lblTitle.Content.ToString();
        }
        public bool? IsChecked
        {
            set => this.chbValue.IsChecked = value;
            get => this.chbValue.IsChecked;
        }
        public string IsCheckedTrue
        {
            set; get;
        }
        public string IsCheckedFalse
        {
            set; get;
        }
        public string Error
        {
            set
            {
                if (this.lblError != null)
                {
                    if (value == null || value == String.Empty)
                    {
                        this.lblError.Content = String.Empty;
                    }
                    else
                    {
                        this.lblError.Content = value;
                    }
                }
            }
            get
            {
                if (this.lblError != null)
                {
                    return this.lblError.Content.ToString();
                }

                return null;
            }
        }
        public Brush BackgroundColor
        {
            set => this.gMain.Background = value;
            get => this.gMain.Background;
        }
        #endregion

        #region Методы
        private void UpdateCheckBox()
        {
            if (this.chbValue != null)
            {
                if (this.chbValue.IsChecked == true)
                {
                    this.chbValue.Content = this.IsCheckedTrue;
                }
                else
                {
                    this.chbValue.Content = this.IsCheckedFalse;
                }
            }
        }

        public void Update(bool? value, string isCheckedTrue, string isCheckedFalse)
        {
            this.IsCheckedTrue = isCheckedTrue;
            this.IsCheckedFalse = isCheckedFalse;
            this.chbValue.IsChecked = (value == null || value == false);
            this.UpdateCheckBox();
        }
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledCheckBox() : this("Заголовок", "Галочка поставлена", "Галочка снята")
        {
            InitializeComponent();
        }
        public LabeledCheckBox(
            string title,
            string isCheckedTrue,
            string isCheckedFalse,
            bool? isChecked = false,
            string error = null,
            Brush backgroundColor = null
            )
        {
            this.InitializeComponent();
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.IsCheckedTrue = isCheckedTrue;
            this.IsCheckedFalse = isCheckedFalse;
            this.chbValue.IsChecked = isChecked;
            this.BackgroundColor = backgroundColor;
            this.Error = error;
            this.chbValue.Checked += this.CheckedHandler;

        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void chbValue_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateCheckBox();
        }

        private void chbValue_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateCheckBox();
        }
        #endregion

    }
}
