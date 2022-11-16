using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для LabeledTextBox.xaml
    /// </summary>
    public partial class LabeledTextBox : UserControl
    {

        #region Поля
        private string regex;
        private string validationText;
        #endregion

        #region Свойства
        public string Title
        {
            set => lblTitle.Content = value;
            get => lblTitle.Content.ToString();
        }
        public string Text
        {
            set => txbValue.Text = value;
            get => txbValue.Text;
        }
        /// <summary>
        /// Строка, которая будет показана при ошибке валидации
        /// </summary>
        public string ValidationText
        {
            set
            {
                validationText = value;
                isValidCheck();
            }
            get => validationText;
        }
        public string Error
        {
            set
            {
                if (lblError != null)
                {
                    if (value == null || value == String.Empty)
                    {
                        lblError.Content = String.Empty;
                    }
                    else
                    {
                        lblError.Content = value;
                    }
                }
            }
            get
            {
                if (lblError != null)
                {
                    return lblError.Content.ToString();
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
                regex = value;
                isValidCheck();
            }
            get => regex;
        }
        /// <summary>
        /// Является ли введеное значение корректным,
        /// проверка происходит с помощью поля RegEx
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (RegEx == null || RegEx == String.Empty || txbValue.Text == null)
                {
                    return true;
                }

                Regex regex = new Regex(RegEx);
                return regex.IsMatch(txbValue.Text);
            }
        }
        public Brush BackgroundColor
        {
            set => gMain.Background = value;
            get => gMain.Background;
        }
        #endregion

        #region Методы
        private void isValidCheck()
        {
            if (!IsValid && ValidationText != null && Error != null)
            {
                Error = ValidationText;
            }
            else
            {
                Error = String.Empty;
            }
        }
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledTextBox() : this("Заголовок", "Значение")
        {
            InitializeComponent();
            Error = String.Empty;
        }
        public LabeledTextBox(string title, string text, string error = null, Brush backgroundColor = null)
        {
            InitializeComponent();
            isValidCheck();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Text = text ?? throw new ArgumentNullException(nameof(text));
            BackgroundColor = backgroundColor;
            Error = error;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void txbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            isValidCheck();
        }
        #endregion

    }
}
