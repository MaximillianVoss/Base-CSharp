using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для LabeledPasswordTextBox.xaml
    /// </summary>
    public partial class LabeledPasswordTextBox : UserControl
    {

        #region Поля
        private string regex;
        private string validationText;
        #endregion

        #region Свойства
        public string Title
        {
            set => this.lblTitle.Content = value;
            get => this.lblTitle.Content.ToString();
        }
        public string Text
        {
            set => this.txbValue.Password = value;
            get => this.txbValue.Password;
        }
        /// <summary>
        /// Строка, которая будет показана при ошибке валидации
        /// </summary>
        public string ValidationText
        {
            set
            {
                this.validationText = value;
                this.isValidCheck();
            }
            get => this.validationText;
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
        /// <summary>
        /// Хранить регулярное выражение 
        /// для проверки ввденного значения,
        /// если строка null или пустая,
        /// то проверки не будет
        /// </summary>
        public string RegEx
        {
            set
            {
                this.regex = value;
                this.isValidCheck();
            }
            get => this.regex;
        }
        /// <summary>
        /// Является ли введеное значение корректным,
        /// проверка происходит с помощью поля RegEx
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.RegEx == null || this.RegEx == String.Empty || this.txbValue.Password == null)
                {
                    return true;
                }

                Regex regex = new Regex(this.RegEx);
                return regex.IsMatch(this.txbValue.Password);
            }
        }
        public Brush BackgroundColor
        {
            set => this.gMain.Background = value;
            get => this.gMain.Background;
        }

        #endregion

        #region Методы
        private void isValidCheck()
        {
            if (!this.IsValid && this.ValidationText != null && this.Error != null)
            {
                this.Error = this.ValidationText;
            }
            else
            {
                this.Error = String.Empty;
            }
        }
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledPasswordTextBox() : this("Заголовок", "Значение")
        {
            this.InitializeComponent();
            this.Error = String.Empty;
        }
        public LabeledPasswordTextBox(string title, string text, string error = null, Brush backgroundColor = null)
        {
            this.InitializeComponent();
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
            this.BackgroundColor = backgroundColor;
            this.Error = error;
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void txbValue_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            this.isValidCheck();
        }

        #endregion

    }
}
