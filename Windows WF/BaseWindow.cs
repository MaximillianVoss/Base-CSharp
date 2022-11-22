using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BaseWindow
{
    /// <summary>
    /// Базовый класс для форм
    /// </summary>
    public partial class BaseWindow : Form
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
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
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
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    filesList.Add(openFileDialog.FileNames[i]);
                }
            }

            return filesList;
        }
        public string GetLoadFilePath(
            string filter = "Все файлы (*.*)|*.*",
            int filterIndex = 1,
            string defaultExtension = "txt",
            bool checkFileExists = false,
            bool checkPathExists = true,
            string Title = "Сохранение файла"
            )
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog
            {
                Title = Title,
                CheckFileExists = checkFileExists,
                CheckPathExists = checkPathExists,
                DefaultExt = defaultExtension,
                Filter = filter,
                FilterIndex = filterIndex,
                RestoreDirectory = true
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
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
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }

            return String.Empty;
        }
        #endregion

        #region Уведомления
        public void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ShowMessage(string message, string title = "Уведомление")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void ShowWarning(string message, string title = "Предупреждение")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion

        #endregion

        #region Конструкторы/Деструкторы
        public BaseWindow()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}

