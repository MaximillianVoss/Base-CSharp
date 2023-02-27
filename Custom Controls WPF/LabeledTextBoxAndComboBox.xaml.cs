using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для LabeledTextBoxAndComboBox.xaml
    /// </summary>
    public partial class LabeledTextBoxAndComboBox : UserControl
    {

        #region Поля
        private string regex;
        private string validationText;
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("LabeledTextBoxAndComboBoxSelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent("LabeledTextBoxAndComboBoxTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        List<object> items;
        #endregion

        #region Свойства

        #region События
        /// <summary>
        /// Событие, срабатывающее при изменении выделенного элемента
        /// </summary>
        public event RoutedEventHandler SelectionChanged
        {
            add => this.AddHandler(SelectionChangedEvent, value);
            remove => this.RemoveHandler(SelectionChangedEvent, value);
        }
        /// <summary>
        /// Событие, срабатывающее при изменении текста в текстовом поле
        /// </summary>
        public event RoutedEventHandler TextChanged
        {
            add => this.AddHandler(TextChangedEvent, value);
            remove => this.RemoveHandler(TextChangedEvent, value);
        }
        /// <summary>
        /// Обработчик события для введеного текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextInputHandler(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(TextChangedEvent));
        }
        /// <summary>
        /// Обработчик события для выделение в выпадающем списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickHadler(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(SelectionChangedEvent));
        }
        #endregion

        #region Основные свойства
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title
        {
            set => this.lblTitle.Content = value;
            get => this.lblTitle.Content.ToString();
        }
        /// <summary>
        /// Текст внутри текстового поля
        /// </summary>
        public string Text
        {
            set { this.txbValue.Text = value; this.isValidCheck(); }
            get => this.txbValue.Text;
        }
        /// <summary>
        /// Текст ошибки при валидации
        /// </summary>
        public string Error
        {
            set
            {
                if (this.lblError != null)
                {
                    if (value == null || value == String.Empty)
                    {
                        this.lblError.Content = String.Empty;
                    }
                    else
                    {
                        this.lblError.Content = value;
                    }
                }
            }
            get
            {
                if (this.lblError != null)
                {
                    return this.lblError.Content.ToString();
                }

                return null;
            }
        }
        /// <summary>
        /// Можно ли редактировать выпадающий список
        /// </summary>
        public bool IsEditable
        {
            set => this.cbItems.IsEditable = value;
            get => this.cbItems.IsEditable;
        }
        /// <summary>
        /// Доступен ли элемент упраления для взаимодействия с пользователем
        /// </summary>
        public new bool IsEnabled
        {
            set
            {
                this.cbItems.IsEnabled = value;
                this.txbValue.IsEnabled = value;
            }
            get
            {
                return this.cbItems.IsEnabled && this.txbValue.IsEnabled;
            }
        }
        /// <summary>
        /// Коллекция элементов
        /// </summary>
        public List<object> Items
        {
            set
            {
                if (value != null)
                {
                    if (this.items == null)
                    {
                        this.items = new List<object>();
                    }
                    this.cbItems.Items.Clear();
                    this.items.Clear();
                    this.items.AddRange(value);
                    try
                    {
                        foreach (var item in this.items)
                        {
                            this.cbItems.Items.Add(this.GetObjectFieldValue(item, "title"));
                        }
                    }
                    catch
                    {
                        foreach (var item in this.items)
                        {
                            this.cbItems.Items.Add(item.ToString());
                        }
                    }
                }
            }
            get => this.items;
        }
        /// <summary>
        /// Заливка фона
        /// </summary>
        public System.Windows.Media.Brush BackgroundColor
        {
            set => this.gMain.Background = value;
            get => this.gMain.Background;
        }
        /// <summary>
        /// Данные для выпадающиего списка
        /// </summary>
        public new Object DataContext
        {
            set => this.cbItems.DataContext = value;
            get => this.cbItems.DataContext;
        }
        #endregion

        #region Валидация
        /// <summary>
        /// Является ли введеное значение корректным,
        /// проверка происходит с помощью поля RegEx
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.RegEx == null || this.RegEx == String.Empty || this.txbValue.Text == null)
                {
                    return true;
                }

                Regex regex = new Regex(this.RegEx);
                return regex.IsMatch(this.txbValue.Text);
            }
        }
        /// <summary>
        /// Строка, которая будет показана 
        /// при ошибке валидации
        /// </summary>
        public string ValidationText
        {
            set
            {
                this.validationText = value;
                this.isValidCheck();
            }
            get => this.validationText;
        }
        /// <summary>
        /// Хранить регулярное выражение 
        /// для проверки ввденного значения,
        /// если строка null или пустая,
        /// то проверки не будет
        /// </summary>
        public string RegEx
        {
            set
            {
                this.regex = value;
                this.isValidCheck();
            }
            get => this.regex;
        }
        #endregion




        #region Выделение в выпадающем списке
        /// <summary>
        /// ID выделенного элемента в списке
        /// </summary>
        public int? SelectedId
        {
            set
            {
                this.Select(value);
            }
            get
            {
                if (this.cbItems.SelectedIndex >= 0)
                {
                    string idStr = this.GetObjectFieldValue(this.items[this.SelectedIndex], "id").ToString();
                    return int.Parse(idStr);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Индекс выделенного элемента в списке
        /// </summary>
        public int SelectedIndex
        {
            set
            {
                if (value < this.cbItems.Items.Count)
                {
                    this.cbItems.SelectedIndex = value;
                }
            }
            get => this.cbItems.SelectedIndex;
        }
        /// <summary>
        /// Строка выделенного элемента в списке
        /// </summary>
        public string SelectedItem
        {
            get
            {
                if (this.cbItems.SelectedIndex >= 0)
                {
                    return this.cbItems.SelectedItem.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #endregion

        #region Методы
        /// <summary>
        /// Проверка правильности заполнения полей
        /// </summary>
        private void isValidCheck()
        {
            if (!this.IsValid && this.ValidationText != null && this.Error != null)
            {
                this.Error = this.ValidationText;
            }
            else
            {
                this.Error = String.Empty;
            }
        }
        /// <summary>
        /// Получает указанное свойство из объекта
        /// </summary>
        /// <param name="obj">объект</param>
        /// <param name="fieldName">имя поля</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private object GetObjectFieldValue(object obj, string fieldName)
        {
            //TODO: падает при выделении другой таблицы, проверить!
            var field = obj.GetType().GetProperty(fieldName);
            if (field == null)
            {
                throw new Exception("Поле не найдено");
            }
            return field.GetValue(obj, null);
        }
        /// <summary>
        /// Добавляет указанный элемент в выпадающий список
        /// </summary>
        /// <param name="item">объект для добавления</param>
        public void Add(object item)
        {
            this.items.Add(item);
            this.cbItems.Items.Add(item);
        }
        /// <summary>
        /// Выбирает элемент с указанным ID в выпадающем списке
        /// </summary>
        /// <param name="id">id элеметна</param>
        /// <param name="idFieldName">поле, содержащее идентификатор</param>
        public void Select(int? id, string idFieldName = "id")
        {
            if (id != null)
            {
                this.SelectedIndex = this.items.FindIndex(x => Convert.ToInt32(this.GetObjectFieldValue(x, idFieldName)) == id);
            }
        }
        /// <summary>
        /// Выбирает элемент по строковому значению
        /// </summary>
        /// <param name="value">строка для выбора в списке</param>
        public void Select(string value)
        {
            this.SelectedIndex = this.items.FindIndex(x => this.GetObjectFieldValue(x, "title").ToString() == value || x.ToString() == value);
        }
        /// <summary>
        /// Обновляет элемент управления в соответствии с указанными данными
        /// </summary>
        /// <param name="items"></param>
        /// <param name="text"></param>
        /// <param name="validationRegEx"></param>
        /// <param name="validationError"></param>
        /// <param name="currentItemId"></param>
        public void Update(List<object> items, string text, string validationRegEx = "", string validationError = "", int? currentItemId = 1)
        {
            this.RegEx = validationRegEx;
            this.validationText = validationError;
            this.Text = text;
            this.Items.Clear();
            this.Items = items;
            this.Select(currentItemId);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledTextBoxAndComboBox() : this("Заголовок", "Значение")
        {
            this.InitializeComponent();
            this.Error = String.Empty;
        }
        public LabeledTextBoxAndComboBox(string title, string text, string error = null, System.Windows.Media.Brush backgroundColor = null)
        {
            this.InitializeComponent();
            this.isValidCheck();
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
            this.BackgroundColor = backgroundColor;
            this.Error = error;
            this.txbValue.TextChanged += this.TextInputHandler;
            this.items = new List<object>();
            if (this.cbItems != null)
            {
                this.cbItems.SelectionChanged += this.ClickHadler;
            }
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void txbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.isValidCheck();
        }
        #endregion

    }
}
