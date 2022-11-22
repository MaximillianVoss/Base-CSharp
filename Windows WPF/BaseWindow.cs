using System;
using System.Windows;

namespace BaseWindow_WPF
{
    public class BaseWindow : Window
    {
        #region Поля

        #endregion

        #region Свойства

        #endregion

        #region Методы
        public void ShowMessage(string message, string title = "Уведомление")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(Exception ex)
        {
            this.ShowError(ex.Message);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void SetCenter()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #endregion

        #region Конструкторы/Деструкторы
        public BaseWindow()
        {
            this.SetCenter();
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
