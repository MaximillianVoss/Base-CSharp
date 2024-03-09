using CustomControlsWPF;
using System.Windows;

namespace WPF_Windows_and_Controls_Test
{
    /// <summary>
    /// Логика взаимодействия для WindowTestTextInputs.xaml
    /// </summary>
    public partial class WindowTestTextInputs : Window
    {
        public WindowTestTextInputs()
        {
            this.InitializeComponent();
            this.txbTestInt.Title = "txbTestInt";
            this.txbTestInt.Text = "1";
            this.txbTestInt.RegEx = Common.Strings.RegularExpressions.regInt;
            this.txbTestInt.ValidationText = Common.Strings.Errors.incorrectInt;

            this.txbTestFloatPoint.Title = "txbTestFloat - pont ";
            this.txbTestFloatPoint.Text = "13.5";
            this.txbTestFloatPoint.RegEx = Common.Strings.RegularExpressions.regFloatPoint;
            this.txbTestFloatPoint.ValidationText = Common.Strings.Errors.incorrectFloatPoint;

            this.txbTestFloatComma.Title = "txbTestFloat - comma";
            this.txbTestFloatComma.Text = "13,5";
            this.txbTestFloatComma.RegEx = Common.Strings.RegularExpressions.regFloatComma;
            this.txbTestFloatComma.ValidationText = Common.Strings.Errors.incorrectFloatComma;


            this.txbTestPassword.Text = "Password-Какой-то-пароль";

            this.txbTestEmail.Title = "Email";
            this.txbTestEmail.RegEx = Common.Strings.RegularExpressions.regEmail;
            this.txbTestEmail.Text = "example@gmail.com";
            this.txbTestEmail.ValidationText = Common.Strings.Errors.incorrectEmail;

        }
    }
}
