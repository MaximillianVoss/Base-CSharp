using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace BaseWindow_WPF
{
    public class BaseWindow : Window
    {

        #region Поля

        #endregion

        #region Свойства

        #endregion

        #region Методы


        #region Загрузка файлов
        public string GetFolderPath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
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
            List<string> filesList = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog
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
            OpenFileDialog openFileDialog = new OpenFileDialog
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
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
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
            System.Windows.MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowError(Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowMessage(string message, string title = "Уведомление")
        {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void ShowWarning(string message, string title = "Предупреждение")
        {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

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
