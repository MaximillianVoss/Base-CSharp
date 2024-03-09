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
        private bool isShowPassword;
        private string text;
        private char passwordChar;
        #endregion

        #region Свойства
        public string Title
        {
            set => this.lblTitle.Content = value;
            get => this.lblTitle.Content.ToString();
        }
        public string Text
        {
            set
            {
                this.text = value;
                if (this.text != this.txbValueHidden.Password)
                    this.txbValueHidden.Password = this.text;
                if (this.text != this.txbValue.Text)
                    this.txbValue.Text = this.text;
            }
            get => this.text;
        }
        public char PasswordChar
        {
            get => this.passwordChar;
            set
            {
                if (value == '\0') // Проверка на пустой символ
                {
                    throw new ArgumentException("Секретный символ пароля не может быть пустым символом.");
                }
                this.passwordChar = value;
            }
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
                if (this.RegEx == null || this.RegEx == String.Empty || this.txbValueHidden.Password == null)
                {
                    return true;
                }

                var regex = new Regex(this.RegEx);
                return regex.IsMatch(this.txbValueHidden.Password);
            }
        }
        public Brush BackgroundColor
        {
            set => this.gMain.Background = value;
            get => this.gMain.Background;
        }
        public bool IsShowPassword
        {
            get => this.isShowPassword;
            set
            {
                this.isShowPassword = value;
                this.btnShowPassword.ImagePath = this.isShowPassword ?
                    "/CustomControlsWPF;component/Ресурсы/показать пароль.png" :
                    "/CustomControlsWPF;component/Ресурсы/скрыть пароль.png";

                this.txbValue.Visibility = this.isShowPassword ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
                this.txbValueHidden.Visibility = !this.isShowPassword ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;

            }
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
            this.PasswordChar = '●';
            this.IsShowPassword = false;
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void txbValue_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            this.isValidCheck();

            this.Text = this.txbValueHidden.Password;
        }

        private void txbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.isValidCheck();

            this.Text = this.txbValue.Text;
        }


        private void btnShowPassword_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsShowPassword = !this.IsShowPassword;
        }



        #endregion

    }
}
