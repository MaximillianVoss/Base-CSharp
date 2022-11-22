using System;
using System.Collections.Generic;
using System.Linq;
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
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));
        public event RoutedEventHandler SelectionChanged
        {
            add => this.AddHandler(SelectionChangedEvent, value);
            remove => this.RemoveHandler(SelectionChangedEvent, value);
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
        public List<String> Items
        {
            set
            {
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        this.cbItems.Items.Add(item);
                    }
                }
            }
            get => this.cbItems.Items.OfType<string>().ToList();
        }
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
        public Brush BackgroundColor
        {
            set => this.gMain.Background = value;
            get => this.gMain.Background;
        }

        public bool IsEditable
        {
            set => this.cbItems.IsEditable = value;
            get => this.cbItems.IsEditable;
        }
        public Object DataContext
        {
            set => this.cbItems.DataContext = value;
            get => this.cbItems.DataContext;
        }
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
        public string Text
        {
            set => this.cbItems.Text = value;
            get => this.cbItems.Text;
        }
        #endregion

        #region Свойства

        #endregion

        #region Методы
        public void Add(object item)
        {
            this.cbItems.Items.Add(item);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledComboBox() : this("Заголовок")
        {
            this.InitializeComponent();
        }

        public LabeledComboBox(string title, List<string> items = null, string error = null, Brush backgroundColor = null, bool isEditable = false)
        {
            this.InitializeComponent();
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
        private void cbItems_DragOver(object sender, System.Windows.DragEventArgs e)
        {

        }
        #endregion

    }
}
