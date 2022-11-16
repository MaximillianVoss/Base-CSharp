using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Title
        {
            set => lblTitle.Content = value;
            get => lblTitle.Content.ToString();
        }
        public List<String> Items
        {
            set
            {
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        cbItems.Items.Add(item);
                    }
                }
            }
            get => cbItems.Items.OfType<string>().ToList();
        }
        public string Error
        {
            set
            {
                if (value == null || value == String.Empty)
                {
                    lblError.Content = String.Empty;
                }
                else
                {
                    lblError.Content = value;
                }
            }
            get => lblError.Content.ToString();
        }
        public Brush BackgroundColor
        {
            set => gMain.Background = value;
            get => gMain.Background;
        }

        public bool IsEditable
        {
            set => cbItems.IsEditable = value;
            get => cbItems.IsEditable;
        }
        public Object DataContext
        {
            set => cbItems.DataContext = value;
            get => cbItems.DataContext;
        }
        public int SelectedIndex
        {
            set
            {
                if (value < cbItems.Items.Count)
                {
                    cbItems.SelectedIndex = value;
                }
            }
            get => cbItems.SelectedIndex;
        }
        public string Text
        {
            set => cbItems.Text = value;
            get => cbItems.Text;
        }
        #endregion

        #region Свойства

        #endregion

        #region Методы
        public void Add(object item)
        {
            cbItems.Items.Add(item);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledComboBox() : this("Заголовок")
        {
            InitializeComponent();
        }

        public LabeledComboBox(string title, List<string> items = null, string error = null, Brush backgroundColor = null, bool isEditable = false)
        {
            InitializeComponent();
            Title = title;
            Items = items;
            Error = error;
            BackgroundColor = backgroundColor;
            IsEditable = isEditable;
            SelectedIndex = -1;
        }


        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedIndex = cbItems.SelectedIndex;
        }
        private void cbItems_DragOver(object sender, System.Windows.DragEventArgs e)
        {

        }
        #endregion

    }
}
