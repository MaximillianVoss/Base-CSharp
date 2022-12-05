using System;
using Windows_ASP.NET.Controls;

namespace Windows_ASP.NET.Forms
{
    public partial class LoginWindow : System.Web.UI.Page
    {


        #region Поля

        #endregion

        #region Свойства

        #endregion

        #region Методы
        private void ShowMessage(Exception ex)
        {
            this.ShowMessage(ex.Message);
        }
        private void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Авторизует пользователя
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        /// <returns></returns>
        private bool Login(string login, string password)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Действие при удачной авторизации
        /// </summary>
        private void LoginSuccess()
        {

        }
        /// <summary>
        /// Действие при неудачной авторизации
        /// </summary>
        private void LoginFailure()
        {

        }
        #endregion

        #region Конструкторы/Деструкторы

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        protected void Page_Load(object sender, EventArgs e)
        {
            // this.hfMsgBoxContainer. = new MessageBox();
            this.Controls.Add(new MessageBox());
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.Login(this.txbLogin.Text, this.txbPassword.Text);
                this.LoginSuccess();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
                this.LoginFailure();
            }
        }
        #endregion


    }
}