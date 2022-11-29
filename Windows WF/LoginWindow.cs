using System;

namespace BaseWindow
{
    /// <summary>
    /// Форма авторизации
    /// </summary>
    public partial class LoginWindow : BaseWindow
    {
        #region Поля

        #endregion

        #region Свойства
        public string Title
        {
            get => this.lblTitle.Text; set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.lblTitle.Text = value;
                }
                else
                {
                    this.lblTitle.Text = String.Empty;
                }
            }
        }
        public string TitleWindow
        {
            get => this.Text;
            set => this.Text = value;
        }
        public string TitlePrimary
        {
            get => this.gbPrimary.Text;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.gbPrimary.Text = value;
                }
                else
                {
                    this.gbPrimary.Text = String.Empty;
                }
            }
        }
        public string TitleSecondary
        {
            get => this.gbSecondary.Text; set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.gbSecondary.Text = value;
                }
                else
                {
                    this.gbSecondary.Text = String.Empty;
                }
            }
        }
        public string TitleBtnPrimary
        {
            get => this.btnPrimary.Text;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.btnPrimary.Text = value;
                }
                else
                {
                    this.btnPrimary.Text = String.Empty;
                }
            }
        }
        public string TitleBtnSecondary
        {
            get => this.btnSecondary.Text;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.btnSecondary.Text = value;
                }
                else
                {
                    this.btnSecondary.Text = String.Empty;
                }
            }
        }
        public char PasswordCharPrimary
        {
            get => this.txbPrimary.PasswordChar;
            set => this.txbPrimary.PasswordChar = value;
        }
        public char PasswordCharSecondary
        {
            get => this.txbSecondary.PasswordChar;
            set => this.txbSecondary.PasswordChar = value;
        }
        #endregion

        #region Методы
        public virtual void PrimaryAction()
        {

        }
        public virtual void SecondaryAction()
        {

        }
        #endregion

        #region Конструкторы/Деструкторы
        public LoginWindow()
        {
            this.InitializeComponent();
        }

        public LoginWindow(
            string title = "Вход в систему",
            string titleWindow = "Вход в систему",
            string titlePrimary = "Логин",
            string titleSecondary = "Пароль",
            string titleBtnPrimary = "Вход",
            string titleBtnSecondary = "Регистрация",
            char passwordCharPrimary = '\0',
            char passwordCharSecondary = '\0'
            ) : this()
        {
            this.Title = title;
            this.TitleWindow = titleWindow;
            this.TitlePrimary = titlePrimary;
            this.TitleSecondary = titleSecondary;
            this.TitleBtnPrimary = titleBtnPrimary;
            this.TitleBtnSecondary = titleBtnSecondary;
            this.PasswordCharPrimary = passwordCharPrimary;
            this.PasswordCharSecondary = passwordCharSecondary;
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void btnPriamry_Click(object sender, System.EventArgs e)
        {
            this.PrimaryAction();
        }
        private void btnSecondary_Click(object sender, System.EventArgs e)
        {
            this.SecondaryAction();
        }

        #endregion

    }
}
