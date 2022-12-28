using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Custom_Controls_WF.Controls
{
    public partial class LabeledTextBox : UserControl
    {
        #region Поля
        private string regex;
        private string validationText;
        #endregion

        #region Свойства
        public string Title
        {
            set => this.lblTitle.Text = value;
            get => this.lblTitle.Text.ToString();
        }
        public string CurrentText
        {
            set => this.txbValue.Text = value;
            get => this.txbValue.Text;
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
                        this.lblError.Text = String.Empty;
                    }
                    else
                    {
                        this.lblError.Text = value;
                    }
                }
            }
            get
            {
                if (this.lblError != null)
                {
                    return this.lblError.Text.ToString();
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
                if (this.RegEx == null || this.RegEx == String.Empty || this.txbValue.Text == null)
                {
                    return true;
                }

                Regex regex = new Regex(this.RegEx);
                return regex.IsMatch(this.txbValue.Text);
            }

        }
        /// <summary>
        /// Доступен этот элемент управления для пользователя или нет
        /// </summary>
        public new bool Enabled
        {
            get
            {
                return this.txbValue.Enabled;
            }
            set
            {
                this.txbValue.Enabled = value;
            }
        }
        public Color BackgroundColor
        {
            set => this.BackColor = value;
            get => this.BackColor;
        }
        /// <summary>
        /// Событие происходит при изменении текста в textBox
        /// </summary>
        public event EventHandler CurrentTextChanged
        {
            add
            {
                this.txbValue.TextChanged += value;
            }
            remove
            {
                this.txbValue.TextChanged -= value;
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
        public LabeledTextBox()
        {
            this.InitializeComponent();
            this.isValidCheck();
            this.Error = String.Empty;
        }
        public LabeledTextBox(string title, string text, string error = null)
        {
            this.InitializeComponent();
            this.isValidCheck();
            this.Error = String.Empty;
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.CurrentText = text ?? throw new ArgumentNullException(nameof(text));
            this.Error = error;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void txbValue_TextChanged(object sender, EventArgs e)
        {
            this.isValidCheck();
        }
        #endregion
    }
}