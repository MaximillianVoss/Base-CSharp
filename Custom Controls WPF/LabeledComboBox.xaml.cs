using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для LabeledComboBox.xaml
    /// </summary>
    public partial class LabeledComboBox : UserControl
    {

        #region Поля
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("LabeledComboBoxSelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        private List<object> items;
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
        /// Обработчик щелчка мыши
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
        /// Текст внутри выпадающего списка
        /// </summary>
        public string Text
        {
            set => this.cbItems.Text = value;
            get => this.cbItems.Text;
        }
        /// <summary>
        /// Ошибка (для валидации и проверок).
        /// Если текст ошибки не null, 
        /// то её текст будет показан под выпадаюим списком
        /// </summary>
        public string Error
        {
            set
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
            get => this.lblError.Content.ToString();
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
            set => this.cbItems.IsEnabled = value;
            get => this.cbItems.IsEnabled;
        }
        /// <summary>
        /// Элементы выпадающего списка
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
            //get => this.cbItems.Items.OfType<object>().ToList();
        }
        /// <summary>
        /// Заливка фона
        /// </summary>
        public Brush BackgroundColor
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

        #region Выделение в выпадающем списке
        /// <summary>
        /// ID выделенного элемента в списке
        /// </summary>
        public int? SelectedId
        {
            set => this.Select(value);
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
        /// Очищает элементы выпадающего списка
        /// </summary>
        public void Clear()
        {
            this.Items?.Clear();
            if (this.cbItems != null && this.cbItems.Items != null)
            {
                this.cbItems.Items.Clear();
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
            var field = obj.GetType().GetProperty(fieldName);
            return field == null ? throw new Exception("Поле не найдено") : field.GetValue(obj, null);
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
            if (this.items.Count > 0)
            {
                if (this.items[0].GetType() == typeof(string))
                {
                    this.SelectedIndex = this.items.FindIndex(x => x.ToString() == value);
                }
                if (this.items[0].GetType() == typeof(object))
                {
                    this.SelectedIndex = this.items.FindIndex(x => this.GetObjectFieldValue(x, "title").ToString() == value);
                }
            }

        }
        /// <summary>
        /// Выбирает первый элемент в выпадающем списке
        /// </summary>
        public void SelectFirst()
        {
            if (this.items != null && this.items.Count > 0)
            {
                this.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Обновляет элемент управления в соответствии с указанными данными
        /// </summary>
        /// <param name="items"></param>
        /// <param name="currentItemId"></param>
        public void Update(List<object> items, int? currentItemId = 1)
        {
            this.Items = items;
            this.Select(currentItemId);
            if (this.SelectedIndex == -1)
                this.SelectedIndex = 0;
        }
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledComboBox() : this("Заголовок")
        {
            this.InitializeComponent();
        }
        public LabeledComboBox(string title, List<object> items = null, string error = null, Brush backgroundColor = null, bool isEditable = false)
        {
            this.InitializeComponent();
            this.items = new List<object>();
            this.Title = title;
            this.Items = items;
            this.Error = error;
            this.BackgroundColor = backgroundColor;
            this.IsEditable = isEditable;
            this.SelectedIndex = -1;
            if (this.cbItems != null)
            {
                this.cbItems.SelectionChanged += this.ClickHadler;
            }
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedIndex = this.cbItems.SelectedIndex;
        }
        #endregion

    }
}
