using System;

namespace Windows_ASP.NET.Forms
{
    public partial class LoginWindow : System.Web.UI.Page
    {
        private void TestClick(object sender, EventArgs e)
        {
            _ = this.LoginForm.IdVisitor;
        }
        protected void LoginForm_Load(object sender, EventArgs e)
        {
            //this.LoginForm.Login = "dasd";
            //this.LoginForm.OnRegisterClick += this.TestClick;
        }
    }
}