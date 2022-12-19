using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        List<object> items;
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("LabeledTextBoxAndComboBoxSelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent("LabeledTextBoxAndComboBoxTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        #endregion

        #region Свойства

        public event RoutedEventHandler SelectionChanged
        {
            add => this.AddHandler(SelectionChangedEvent, value);
            remove => this.RemoveHandler(SelectionChangedEvent, value);
        }

        public event RoutedEventHandler TextChanged
        {
            add => this.AddHandler(TextChangedEvent, value);
            remove => this.RemoveHandler(TextChangedEvent, value);
        }
        private void TextInputHandler(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(TextChangedEvent));
        }
        private void ClickHadler(object sender, RoutedEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(SelectionChangedEvent));
        }
        public string Title
        {
            set => this.lblTitle.Content = value;
            get => this.lblTitle.Content.ToString();
        }
        /// <summary>
        /// Текст TextBox
        /// </summary>
        public string Text
        {
            set => this.txbValue.Text = value;
            get => this.txbValue.Text;
        }
        /// <summary>
        /// Строка, которая будет показана при ошибке валидации
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
        public Brush BackgroundColor
        {
            set => this.gMain.Background = value;
            get => this.gMain.Background;
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
                    this.items.AddRange(value);
                    this.Items.Clear();
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
                    this.Items.AddRange(value);
                }
            }
            get => this.cbItems.Items.OfType<object>().ToList();
        }
        public bool IsEditable
        {
            set => this.cbItems.IsEditable = value;
            get => this.cbItems.IsEditable;
        }
        public new Object DataContext
        {
            set => this.cbItems.DataContext = value;
            get => this.cbItems.DataContext;
        }
        public int SelectedIndex
        {
            set
            {
                if (value < 0 || value > this.cbItems.Items.Count)
                {
                    throw new InvalidOperationException(String.Format("Невозможно выбрать элемент с индексом {0}", value));
                }
                this.cbItems.SelectedIndex = value;

            }
            get => this.cbItems.SelectedIndex;
        }
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

        #region Методы
        public object GetObjectFieldValue(object obj, string fieldName)
        {
            //TODO: падает при выделении другой таблицы, проверить!
            var field = obj.GetType().GetProperty(fieldName);
            if (field == null)
            {
                throw new Exception("Поле не найдено");
            }
            return field.GetValue(obj, null);
        }
        public void Select(int? id)
        {
            if (id != null)
            {
                this.SelectedIndex = this.items.FindIndex(x => Convert.ToInt32(this.GetObjectFieldValue(x, "id")) == id);
            }
        }
        public void Select(string value)
        {
            this.SelectedIndex = this.items.FindIndex(x => this.GetObjectFieldValue(x, "title").ToString() == value || x.ToString() == value);
        }
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
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledTextBoxAndComboBox() : this("Заголовок", "Значение")
        {
            this.InitializeComponent();
            this.Error = String.Empty;
        }
        public LabeledTextBoxAndComboBox(string title, string text, string error = null, Brush backgroundColor = null)
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
