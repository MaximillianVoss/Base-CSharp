using System.Windows;

namespace WPF_Windows_and_Controls_Test
{
    /// <summary>
    /// Логика взаимодействия для WindowTestButtons.xaml
    /// </summary>
    public partial class WindowTestButtons : Window
    {


        #region Поля

        #endregion

        #region Свойства

        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public WindowTestButtons()
        {
            this.InitializeComponent();
        }


        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void ButtonPrimary_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format("Нажато: {0}", sender));
        }
        #endregion

    }
}
