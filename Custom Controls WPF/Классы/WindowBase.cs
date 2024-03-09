using BaseWindow_WPF.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace CustomControlsWPF
{
    /// <summary>
    /// Режимы окна или формы
    /// </summary>
    public enum EditModes
    {
        /// <summary>
        /// Создание.
        /// </summary>
        Create,

        /// <summary>
        /// Обновление.
        /// </summary>
        Update,

        /// <summary>
        /// Удаление.
        /// </summary>
        Delete,

        /// <summary>
        /// Нет режима.
        /// </summary>
        None
    }
    public class WindowBase : Window
    {

        #region Поля
        private List<CustomEventArgs> currentObjects = new List<CustomEventArgs>();
        private CustomEventArgs currentObject = null;
        #endregion

        #region Свойства
        /// <summary>
        /// Текущий режим: создание/редактирование/удаление
        /// </summary>
        public EditModes Mode
        {
            set; get;
        }
        /// <summary>
        /// Объекты с которыми в данный момент взаимодействует окно/страница, обычно передаются ему в качестве параметров
        /// </summary>
        public List<CustomEventArgs> CurrentObjects
        {
            get => this.currentObjects;
            set
            {
                if (value == null)
                    this.currentObjects = new List<CustomEventArgs>();
                else
                    this.currentObjects = value;
            }
        }
        /// <summary>
        /// Объект возвращаемый окном/страницей после закрытия,
        /// NULL - если ничего не требуется возвращать
        /// </summary>
        public CustomEventArgs Result
        {
            set; get;
        }
        /// <summary>
        /// Текущий объект, выбранный в списке параметров
        /// </summary>
        public CustomEventArgs CurrentObject
        {
            set => this.currentObject = value;
            get
            {
                if (this.CurrentObjects != null && this.CurrentObjects.Count > 0)
                    this.currentObject = this.CurrentObjects[this.CurrentObjects.Count - 1];
                return this.currentObject;
            }
        }
        /// <summary>
        /// Ширина контента окна, например, если внутри окна frame
        /// </summary>
        public int ContentWidth
        {
            set; get;
        }
        /// <summary>
        /// Высота контента окна, например, если внутри окна frame
        /// </summary>
        public int ContentHeight
        {
            set; get;
        }
        #endregion

        #region Методы

        #region Заголовок окна
        public string GetTitle(EditModes mode, Type type)
        {
            return String.Format("{0} {1}", Common.EditModesDescriptions.Descriptions[mode], "объекта");
            //string typeStr = "Объекта";
            //if (Common.EntityRussianNames.NamesNominative.ContainsKey(type))
            //    typeStr = Common.EntityRussianNames.NameGenitiveSingular[type];
            //return String.Format("{0} {1}", Common.EditModesDescriptions.Descriptions[mode], typeStr.ToLower());
        }
        public string GetTitle(EditModes mode, object obj)
        {
            return String.Format("{0} {1}", Common.EditModesDescriptions.Descriptions[mode], "объекта");
            //if (obj == null)
            //    return null;
            //if (Common.EntityRussianNames.NamesGenitive.ContainsKey(obj.GetType()))
            //    return this.GetTitle(mode, obj.GetType());
            //return this.GetTitle(mode, obj.GetType().BaseType);
        }
        private string GetTitle(object mode, object currentObject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обновит заголовок окна в соответствии с режимом и выбранным объектом
        /// </summary>
        /// <param name="mode">Режим окна</param>
        public void UpdateTitle(EditModes mode)
        {
            this.Title = GetTitle(mode, this.CurrentObject);
        }
        /// <summary>
        /// Обновит заголовок окна в соответствии с режимом и выбранным объектом
        /// </summary>
        public void UpdateTitle()
        {
            this.UpdateTitle(this.Mode);
        }
        #endregion

        #region Работа с передаваемыми параметрами
        /// <summary>
        /// Проверяет количество переданных аргументов
        /// </summary>
        /// <param name="expectedArgsCount">Количество ожидаемых аргументов</param>
        /// <returns></returns>
        public bool IsArgsCorrect(int expectedArgsCount)
        {
            return this.CurrentObjects == null ? expectedArgsCount == 0 : this.CurrentObjects.Count == expectedArgsCount;
        }
        /// <summary>
        /// Проверяет количество переданных аргументов
        /// </summary>
        /// <param name="expectedArgsCount">Количество ожидаемых аргументов</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Выдает исключение,
        /// если число переданных и ожидаемых аргументов не совпадает
        /// </exception>
        public bool IsArgsCorrectException(int expectedArgsCount)
        {
            if (!this.IsArgsCorrect(expectedArgsCount))
                throw new Exception(String.Format("Ожидалось: {0} параметров, получено: {1} параметров", expectedArgsCount, this.CurrentObjects.Count));
            return true;
        }
        /// <summary>
        /// Извлекает объект из контейнера или упакованного типа, если возможно.
        /// </summary>
        /// <typeparam name="T">Тип объекта, который необходимо извлечь.</typeparam>
        /// <param name="obj">Объект для извлечения.</param>
        /// <returns>Объект заданного типа T, если извлечение успешно, иначе null.</returns>
        public T UnpackCurrentObject<T>(object obj) where T : class
        {
            // Попытка прямого приведения, если obj уже является нужным типом T.
            if (obj is T tObj)
                return tObj;

            // Попытка извлечения значения из TreeViewItemCustom, если obj - это TreeViewItemCustom и его Value имеет тип T.
            if (obj is TreeViewItemCustom treeViewItem && treeViewItem.Value is T value)
                return value;

            // Попытка извлечения значения из CustomEventArgs, если obj - это CustomEventArgs и его Data имеет тип T.
            if (obj is CustomEventArgs customArgs && customArgs.Data is T data)
                return data;

            // Попытка извлечения значения из CustomEventArgs, если его Data является TreeViewItemCustom и содержит данные типа T.
            if (obj is CustomEventArgs customArgsTreeItem && customArgsTreeItem.Data is TreeViewItemCustom treeViewData && treeViewData.Value is T treeValue)
                return treeValue;

            // Возвращает null, если ни одна из проверок не сработала.
            return null;
        }
        /// <summary>
        /// Извлекает вложенный объект без попытки приведения к определенному типу.
        /// </summary>
        /// <param name="obj">Объект для извлечения.</param>
        /// <returns>Вложенный объект, если он присутствует, иначе null.</returns>
        public object UnpackCurrentObject(object obj)
        {
            object currentObject;

            // Проверяем, содержит ли obj объект TreeViewItemCustom и извлекаем его Value
            if (obj is TreeViewItemCustom treeViewItem)
                currentObject = treeViewItem.Value;
            // Проверяем, содержит ли obj объект CustomEventArgs и извлекаем его Data
            else if (obj is CustomEventArgs customArgs)
                currentObject = customArgs.Data;
            // Проверяем, содержит ли obj объект CustomEventArgs, и внутри него TreeViewItemCustom, и извлекаем его Value
            else if (obj is CustomEventArgs customArgstreeViewItem && customArgstreeViewItem.Data is TreeViewItemCustom treeViewData)
                currentObject = treeViewData.Value;
            // В противном случае возвращаем obj, если он не null
            else
                currentObject = obj;

            return currentObject;
        }

        /// <summary>
        /// Добавляет элемент в список текущих объектов.
        /// </summary>
        /// <param name="item">Элемент для добавления.</param>
        public void AddCurrentObject(CustomEventArgs item)
        {
            if (item != null)
            {
                this.CurrentObjects.Add(item);
            }
        }
        /// <summary>
        /// Добавляет элемент в список текущих объектов.
        /// </summary>
        /// <param name="object">Элемент для добавления.</param>
        public void AddCurrentObject(object @object)
        {
            this.AddCurrentObject(new CustomEventArgs(@object));
        }
        /// <summary>
        /// Удаляет последний добавленный элемент
        /// </summary>
        public void RemoveLastCurrentObject()
        {
            if (this.CurrentObjects != null && this.CurrentObjects.Count > 0)
                this.CurrentObjects.RemoveAt(this.CurrentObjects.Count - 1);
        }

        /// <summary>
        /// Очищает список текущих объектов.
        /// </summary>
        public void ClearCurrentObjects()
        {
            this.CurrentObjects.Clear();
        }

        /// <summary>
        /// Очищает список текущих объектов и добавляет новый элемент.
        /// </summary>
        /// <param name="item">Элемент для добавления после очистки списка.</param>
        public void AddWithClearCurrentObjects(CustomEventArgs item)
        {
            this.CurrentObjects.Clear();
            if (item != null)
            {
                this.CurrentObjects.Add(item);
            }
        }
        /// <summary>
        /// Очищает список текущих объектов и добавляет новый элемент.
        /// </summary>
        /// <param name="@object">Элемент для добавления после очистки списка.</param>
        public void AddWithClearCurrentObjects(object @object)
        {
            this.AddWithClearCurrentObjects(new CustomEventArgs(@object));
        }
        #endregion

        #region Обновление элементов управления
        /// <summary>
        /// Обновляет текст для текущей кнопки действия окна. 
        /// Нужно для редактирующих и создающих окон
        /// </summary>
        /// <param name="btnOk"></param>
        /// <exception cref="Exception">
        /// Выдает исключение, если кнопка не инициализирована
        /// </exception>
        public void UpdateOkButton(ButtonPrimary btnOk)
        {
            if (btnOk == null)
                throw new Exception("Кнопка действия не инициализирована");
            btnOk.Text = this.CurrentObject != null ?
                Common.Strings.Titles.Controls.Buttons.saveChanges :
                Common.Strings.Titles.Controls.Buttons.createItem;
        }
        /// <summary>
        /// Заполняет LabeledComboBox элементами из указанной коллекции
        /// </summary>
        /// <param name="labeledComboBox">элемент управления</param>
        /// <param name="items">коллекция объектов (поля:id,Title)</param>
        public void UpdateComboBox(LabeledComboBox labeledComboBox, List<object> items)
        {
            labeledComboBox.Items = items;
            if(labeledComboBox.Items.Count>0)
                labeledComboBox.SelectedIndex = 0;
        }
        /// <summary>
        /// Заполняет LabeledTextBoxAndComboBox элементами из указанной коллекции
        /// </summary>
        /// <param name="labeledComboBox">элемент управления</param>
        /// <param name="items">коллекция объектов (поля:id,Title)</param>
        public void UpdateComboBox(LabeledTextBoxAndComboBox labeledComboBox, List<object> items)
        {
            labeledComboBox.Items = items;
            if (labeledComboBox.Items.Count > 0)
                labeledComboBox.SelectedIndex = 0;
        }
        /// <summary>
        /// Обновляет значение LabeledCheckBox указанными значениями
        /// </summary>
        /// <param name="labeledCheckBox">элемент управления</param>
        /// <param name="isCheckedStr">сообщение, когда галочка установлена</param>
        /// <param name="isUncheckedStr">сообщение, когда галочка НЕ установлена</param>
        /// <param name="isChecked">начальное значение LabeledCheckBox</param>
        public void UpdateCheckBox(LabeledCheckBox labeledCheckBox, string isCheckedStr, string isUncheckedStr, bool isChecked)
        {
            labeledCheckBox.IsCheckedTrue = isCheckedStr;
            labeledCheckBox.IsCheckedFalse = isUncheckedStr;
            labeledCheckBox.IsChecked = isChecked;
        }
        /// <summary>
        /// Заполняет LabeledTextBox указанным значением
        /// </summary>
        /// <param name="labeledTextBox">элемент управления</param>
        /// <param name="value">значение</param>
        /// <param name="defaultValue">значение по умолчанию(если основное знаение null)</param>
        public void UpdateTextBox(LabeledTextBox labeledTextBox, object value, string defaultValue)
        {
            labeledTextBox.Text = value == null ? defaultValue : value.ToString();
        }
        #endregion

        #region Преобразование в списки
        /// <summary>
        /// Преобразует коллекцию элементов в список объектов заданного типа.
        /// </summary>
        /// <param name="items">Коллекция элементов, которую нужно преобразовать.</param>
        /// <param name="type">Тип объектов, которые будут содержаться в списке.</param>
        /// <returns>Новый список объектов заданного типа, содержащий элементы из переданной коллекции.</returns>
        public object ToList(IEnumerable items, Type type)
        {
            Type collectionWithType = typeof(List<>).MakeGenericType(type);
            return Activator.CreateInstance(collectionWithType, items);
        }
        /// <summary>
        /// Получает список объектов из указанного DbSet.
        /// Для заполнения у объекта должен быть метод ToObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<object> ToList<T>(DbSet items)
        {
            var list = new List<object>();
            foreach (object item in items)
            {
                Type type = item.GetType();
                System.Reflection.MethodInfo methodInfo = type.GetMethod("ToObject");
                list.Add(methodInfo.Invoke(item, null));
            }
            return list;
        }
        /// <summary>
        /// Получает список объектов из указанного List.
        /// Для заполнения у объекта должен быть метод ToObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<object> ToList<T>(List<T> items)
        {
            var list = new List<object>();
            foreach (T item in items)
            {
                Type type = item.GetType();
                System.Reflection.MethodInfo methodInfo = type.GetMethod("ToObject");
                list.Add(methodInfo.Invoke(item, null));
            }
            return list;
        }
        /// <summary>
        /// Преобразует список объектов, реализующих интерфейс IToObject, в список объектов.
        /// </summary>
        /// <param name="items">Список объектов, реализующих интерфейс IToObject.</param>
        /// <returns>Новый список объектов, полученных путем вызова метода ToObject() для каждого элемента в переданном списке.</returns>
        public List<object> ToList(List<IToObject> items)
        {
            return items.Select(item => item.ToObject()).ToList<object>();
        }
        #endregion

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

        private void SetContentSize(int width, int height)
        {
            this.Topmost = true; // Установка окна поверх всех окон
            this.ContentWidth = width;
            this.ContentHeight = height;
            this.MinHeight = height;
            this.MinWidth = width;
        }
        #endregion

        #region Конструкторы/Деструкторы
        public WindowBase() : this(EditModes.Create, null)
        {

        }
        public WindowBase(EditModes mode) : this(mode, null)
        {

        }
        /// <summary>
        /// Создает окно с указанными параметрами
        /// </summary>
        /// <param name="mode">Режим окна</param>
        /// <param name="args">Аргументы, переданные окну</param>
        public WindowBase(EditModes mode, List<CustomEventArgs> args)
        {
            this.SetCenter();
            this.Mode = mode;
            this.CurrentObjects = args;
            this.Title = GetTitle(mode, args);
        }

        /// <summary>
        /// Создает окно с указанными параметрами
        /// </summary>
        /// <param name="mode">Режим окна</param>
        /// <param name="args">Один аргумент, переданный окну</param>
        public WindowBase(EditModes mode, object arg)
        {
            this.SetCenter();
            this.Mode = mode;
            this.AddCurrentObject(arg);
            this.Title = GetTitle(mode, arg);
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
