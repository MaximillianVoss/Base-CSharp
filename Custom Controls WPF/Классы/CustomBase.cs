using BaseWindow_WPF.Classes;
using CustomControlsWPF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace CustomControlsWPF.Классы
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

    public static class ObjectExtensions
    {
        /// <summary>
        /// Получает указанное поле из объекта
        /// </summary>
        /// <param name="obj">Целевой объект.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение поля.</returns>
        public static object GetFieldValue(this object obj, string fieldName)
        {
            if (obj == null)
            {
                throw new Exception("Не передан объект для получения поля!");
            }

            if (String.IsNullOrEmpty(fieldName))
            {
                throw new Exception("Не передано имя поля!");
            }

            System.Reflection.PropertyInfo field = obj.GetType().GetProperty(fieldName);
            return field == null
                ? throw new Exception(Common.Strings.Errors.fieldIsNotFoundInObject)
                : field.GetValue(obj, null);
        }
        /// <summary>
        /// Проверяет, является ли тип объекта или его базовый тип указанным типом.
        /// </summary>
        /// <param name="obj">Объект для проверки.</param>
        /// <param name="type">Тип для сравнения.</param>
        /// <returns>True, если тип объекта или его базовый тип совпадает с указанным типом, иначе false.</returns>
        public static bool IsTypeOrBaseEqual(this object obj, Type type)
        {
            return obj.GetType() == type || obj.GetType().BaseType == type;
        }
        /// <summary>
        /// Проверяет, является ли тип объекта или его базовый тип указанным типом, и бросает исключение, если нет.
        /// </summary>
        /// <param name="obj">Объект для проверки.</param>
        /// <param name="expectedType">Тип для сравнения.</param>
        /// <exception cref="ArgumentException">Генерируется, если тип объекта не совпадает с ожидаемым.</exception>
        public static bool ValidateTypeOrBaseType(this object obj, Type expectedType)
        {
            Type actualType = obj.GetType();
            if (actualType != expectedType && actualType.BaseType != expectedType)
            {
                throw new ArgumentException($"Тип объекта '{actualType.FullName}' не соответствует ожидаемому типу '{expectedType.FullName}'.");
            }
            return true;
        }
        /// <summary>
        /// Проверяет, является ли тип объекта или его базовый тип указанным типом
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип для сравнения.</typeparam>
        /// <param name="obj">Объект для проверки.</param>
        public static bool ValidateTypeOrBaseType<T>(this object obj)
        {
            if (obj == null)
                return false;
            Type expectedType = typeof(T);
            Type actualType = obj.GetType();
            return !(actualType != expectedType && actualType.BaseType != expectedType);
        }
        /// <summary>
        /// Проверяет, является ли тип объекта или его базовый тип указанным типом, и бросает исключение, если нет.
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип для сравнения.</typeparam>
        /// <param name="obj">Объект для проверки.</param>
        /// <exception cref="ArgumentException">Генерируется, если тип объекта не совпадает с ожидаемым.</exception>
        public static bool ValidateTypeOrBaseTypeEx<T>(this object obj)
        {
            Type expectedType = typeof(T);
            Type actualType = obj.GetType();
            if (actualType != expectedType && actualType.BaseType != expectedType)
            {
                throw new ArgumentException($"Тип объекта '{actualType.FullName}' не соответствует ожидаемому типу '{expectedType.FullName}'.");
            }
            return true;
        }
    }

    /// <summary>
    /// Обеспечивает взаимодействие с БД и облачным хранилищем, 
    /// также позволяет вносить информацию в элементы управления должным образом
    /// </summary>
    public class CustomBase
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
        /// webDAV-клиент
        /// </summary>
        //public WDClient WDClient
        //{
        //    set; get;
        //}
        /// <summary>
        /// База данных
        /// </summary>
        //public CustomDB CustomDb
        //{
        //    set; get;
        //}
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

        #endregion

        #region Конструкторы/Деструкторы

        /// <summary>
        /// Конструктор по умолчанию, создающий CustomBase с настройками по умолчанию.
        /// </summary>
        public CustomBase()
        {
            // Initialize with default settings or state.
            // Assuming that CustomDB and WDClient can be initialized with default constructors as well.
            //this.CustomDb = new CustomDB();
            //this.WDClient = new WDClient();
            this.CurrentObjects = new List<CustomEventArgs>();
        }

        /// <summary>
        /// Конструктор, принимающий путь к файлу настроек и создающий CustomBase на основе этих настроек.
        /// </summary>
        /// <param name="settingsFilePath">Путь к файлу настроек.</param>
        //public CustomBase(string settingsFilePath) : this(new Settings(settingsFilePath))
        //{

        //}

        /// <summary>
        /// Конструктор, принимающий настройки и инициализирующий CustomBase на основе этих настроек.
        /// </summary>
        /// <param name="settings">Настройки для инициализации CustomBase.</param>
        //public CustomBase(Settings settings)
        //{
        //    this.CustomDb = new CustomDB(settings);
        //    this.WDClient = new WDClient(settings);
        //    this.CurrentObjects = new List<CustomEventArgs>();
        //}

        #endregion


        #region Методы
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
            object currentObject = null;

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

        public bool IsArgsCorrect(int expectedArgsCount)
        {
            return this.CurrentObjects == null ? expectedArgsCount == 0 : this.CurrentObjects.Count == expectedArgsCount;
        }
        public bool IsArgsCorrectException(int expectedArgsCount)
        {
            if (!this.IsArgsCorrect(expectedArgsCount))
                throw new Exception(String.Format("Ожидалось: {0} параметров, получено: {1} параметров", expectedArgsCount, this.CurrentObjects.Count));
            return true;
        }
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
        public string GetTitle(CustomBase commonBase)
        {
            return this.GetTitle(commonBase.Mode, commonBase.CurrentObject);
        }

        #region Работа с объектами
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
        public void UpdateOkButton(ButtonPrimary btnOk)
        {
            if (btnOk == null)
                throw new Exception("Кнопка действия не инициализированна");
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
        }
        /// <summary>
        /// Заполняет LabeledTextBoxAndComboBox элементами из указанной коллекции
        /// </summary>
        /// <param name="labeledComboBox">элемент управления</param>
        /// <param name="items">коллекция объектов (поля:id,Title)</param>
        public void UpdateComboBox(LabeledTextBoxAndComboBox labeledComboBox, List<object> items)
        {
            labeledComboBox.Items = items;
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
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="type"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<object> ToList(List<IToObject> items)
        {
            return items.Select(item => item.ToObject()).ToList();
        }
        #endregion

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
