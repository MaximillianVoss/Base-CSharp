using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace BaseWindow_WPF
{
    /// <summary>
    /// Возможные режимы окна или формы
    /// </summary>
    public enum EditModes
    {
        Create,
        Update,
        Delete,
        None
    }
    public class WindowBase : Window
    {

        #region Поля

        #endregion

        #region Свойства
        /// <summary>
        /// Текущий режим: создание/редактирование/удаление
        /// </summary>
        public EditModes Mode
        {
            set; get;
        }
        #endregion

        #region Методы


        #region Загрузка файлов
        public string GetFolderPath()
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Выберите папку"
            };
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                return folderBrowserDialog.SelectedPath;
            }

            return String.Empty;
        }
        public List<string> GetFilesPath(string filter = "Все файлы (*.*)|*.*")
        {
            var filesList = new List<string>();
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                Multiselect = true,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    filesList.Add(openFileDialog.FileNames[i]);
                }
            }

            return filesList;
        }
        public string[] GetLoadFilePath(
            string filter = "Все файлы (*.*)|*.*",
            bool isMulti = false,
            int filterIndex = 1,
            string defaultExtension = "txt",
            bool checkFileExists = false,
            bool checkPathExists = true,
            string Title = "Сохранение файла"
            )
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = Title,
                CheckFileExists = checkFileExists,
                CheckPathExists = checkPathExists,
                DefaultExt = defaultExtension,
                Filter = filter,
                Multiselect = isMulti,
                FilterIndex = filterIndex,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog.Multiselect)
                {
                    return openFileDialog.FileNames;
                }
                else
                {
                    return new string[] { openFileDialog.FileName };
                }
            }
            return null;
        }

        #endregion

        #region Сохранение файлов
        public string GetSaveFilePath()
        {
            var saveFileDialog1 = new SaveFileDialog
            {
                Title = "Сохранения файла",
                CheckFileExists = false,
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }

            return String.Empty;
        }
        #endregion

        #region Уведомления
        public void ShowError(string message)
        {
            _ = System.Windows.MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowError(Exception ex, bool isShowInner = true)
        {
            string message = isShowInner && ex.InnerException != null
                ? ex.Message + "\n\n" + ex.InnerException.Message
                : ex.Message;

            _ = System.Windows.MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowMessage(string message, string title = "Уведомление")
        {
            _ = System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void ShowWarning(string message, string title = "Предупреждение")
        {
            _ = System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        public void SetCenter()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #endregion

        #region Конструкторы/Деструкторы
        public WindowBase()
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
