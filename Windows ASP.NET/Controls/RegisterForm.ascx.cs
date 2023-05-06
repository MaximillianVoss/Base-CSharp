using System;
using System.Text.RegularExpressions;

namespace Windows_ASP.NET.Controls
{
    public partial class RegisterForm : System.Web.UI.UserControl
    {
        #region Поля
        /// <summary>
        /// Регулярные выражения
        /// </summary>
        private static class RegularExpressions
        {
            /// <summary>
            /// Пароли,
            /// подробнее тут:
            /// https://stackoverflow.com/questions/19605150/regex-for-password-must-contain-at-least-eight-characters-at-least-one-number-a
            /// </summary>
            public static class Password
            {
                /// <summary>
                /// Минимум 8 символов, как минимум одна буква и одна цифра
                /// </summary>
                public static string reg1 = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
                /// <summary>
                /// Минимум 8 символов, как минимум одна буква и одна цифра и один спецсимвол
                /// </summary>
                public static string reg2 = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
                /// <summary>
                /// Минимум 8 символов, как минимум по одной букве в верхнем и нижнем регистре, одна цифра
                /// </summary>
                public static string reg3 = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
                /// <summary>
                /// Минимум 8 символов, как минимум по одной букве в верхнем и нижнем регистре, одна цифра и один спецсимвол
                /// </summary>
                public static string reg4 = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
                /// <summary>
                /// Минимум 8, максимум 10 символов, как минимум по одной букве в верхнем и нижнем регистре,одна цифра и один спецсимвол
                /// </summary>
                public static string reg5 = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$";
            }
            /// <summary>
            /// Логин
            /// </summary>
            public static class Login
            {
                /// <summary>
                /// Логин длинной до 19 симвлов
                /// </summary>
                public static string reg1 = @"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d.-]{0,19}$";
                /// <summary>
                /// Логин длинной до 20 симвлов и минимум 3
                /// </summary>
                public static string reg2 = @"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d.-]{3,20}$";
                /// <summary>
                /// Логин длиной минимум 3 символа, максимальная длина неограничена
                /// </summary>
                public static string reg3 = @"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d.-]{3,}$";
                /// <summary>
                /// Логин являющийся электронной почтой
                /// </summary>
                public static string reg4 = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            }
        }
        #endregion

        #region Свойства
        public string IdVisitor
        {
            get => this.hfFingerprint.Value;
            set => this.hfFingerprint.Value = value;
        }
        public string Title
        {
            set => this.lblTitle.Text = value;
            get => this.lblTitle.Text;
        }
        public string Login
        {
            set => this.txbLogin.Text = value;
            get => this.txbLogin.Text;
        }
        public string LoginRegEx
        {
            set; get;
        }
        public string LoginTitle
        {
            set => this.lblLogin.Text = value;
            get => this.lblLogin.Text;
        }
        public string Password
        {
            set => this.txbPassword.Text = value;
            get => this.txbPassword.Text;
        }
        public string PasswordRegEx
        {
            set; get;
        }
        public string PasswordTitle
        {
            set => this.lblPassword.Text = value;
            get => this.lblPassword.Text;
        }
        public string LoginError
        {
            set => this.lblError.Text = value;
            get => this.lblError.Text;
        }
        public EventHandler OnRegisterClick
        {
            set => this.btnRegister.Click += value;
            get => null;
        }
        #endregion

        #region Методы
        public bool IsUserExists(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void DoLoginSuccess()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadForm()
        {
            this.lblError.Visible = false;
            this.LoginRegEx = RegularExpressions.Login.reg4;
            this.PasswordRegEx = RegularExpressions.Password.reg1;
        }
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <exception cref="Exception"></exception>
        private void DoRegister(string login, string password)
        {
            if (this.CheckLogin(login, this.LoginRegEx) && this.CheckPassword(password, this.PasswordRegEx))
            {
                //TODO:код проверки пользователя на наличие в БД
                if (this.IsUserExists(login, password))
                {
                    this.DoLoginSuccess();
                }
                else
                {
                    throw new Exception("Пользователя с указанным логином и паролем не существует!");
                }
            }

        }

        /// <summary>
        /// Действие после регистрации
        /// </summary>
        private void AfterRegister()
        {
            // Перенаправление на страницу Index.aspx
            //Response.Redirect("Index.aspx");
        }
        /// <summary>
        /// 
        /// </summary>
        private void HideError()
        {
            this.lblError.Text = String.Empty;
            this.lblError.Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public void ShowError(string error)
        {
            this.lblError.Text = error;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public void ShowError(Exception exception)
        {
            this.lblError.Visible = true;
            this.ShowError(exception.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="regEx"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private bool CheckLogin(string login, string regEx)
        {
            this.HideError();
            if (String.IsNullOrEmpty(regEx))
                throw new Exception("Не указано регулярное выражение для проверки!");
            return new Regex(regEx).IsMatch(login);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="regEx"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private bool CheckPassword(string password, string regEx)
        {
            this.HideError();
            if (String.IsNullOrEmpty(regEx))
                throw new Exception("Не указано регулярное выражение для проверки!");
            return new Regex(regEx).IsMatch(password);
        }
        #endregion

        #region Конструкторы/Деструкторы

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadForm();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        protected void txbLogin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckLogin(this.txbLogin.Text, this.LoginRegEx))
                {
                    throw new Exception("Введен некорректный логин!");
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        protected void txbPassword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CheckPassword(this.txbPassword.Text, this.PasswordRegEx))
                {
                    throw new Exception("Введен некорректный пароль!");
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                this.DoRegister(this.txbLogin.Text, this.txbPassword.Text);
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        #endregion

    }
}