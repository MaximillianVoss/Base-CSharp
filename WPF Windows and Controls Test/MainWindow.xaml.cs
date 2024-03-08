using CustomControlsWPF.Классы;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Windows_and_Controls_Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.txbTestInt.Title = "txbTestInt";
            this.txbTestInt.RegEx = Common.Strings.RegularExpressions.regInt;
            this.txbTestInt.ValidationText = Common.Strings.Errors.incorrectIntStr;

            this.txbTestFloatPoint.Title = "txbTestFloat - pont ";
            this.txbTestFloatPoint.RegEx = Common.Strings.RegularExpressions.regFloatPoint;
            this.txbTestFloatPoint.ValidationText = Common.Strings.Errors.incorrectFloatPointStr;

            this.txbTestFloatComma.Title = "txbTestFloat - comma";
            this.txbTestFloatComma.RegEx = Common.Strings.RegularExpressions.regFloatComma;
            this.txbTestFloatComma.ValidationText = Common.Strings.Errors.incorrectFloatCommaStr;

        }
    }
}
