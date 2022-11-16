using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для LabeledDateTimePicker.xaml
    /// </summary>
    public partial class LabeledDateTimePicker : UserControl
    {



        #region Поля
        public string Title
        {
            set => lblTitle.Content = value;
            get => lblTitle.Content.ToString();
        }
        public DateTime? Date
        {
            set => dtpDate.SelectedDate = value;
            get => dtpDate.SelectedDate;
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
        public DateTime? StartDate
        {
            set => dtpDate.DisplayDateStart = value;
            get => dtpDate.DisplayDateStart;
        }
        public DateTime? EndDate
        {
            set => dtpDate.DisplayDateEnd = value;
            get => dtpDate.DisplayDateEnd;
        }
        #endregion

        #region Свойства

        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public LabeledDateTimePicker() : this("Заголовок", DateTime.Now)
        {
            InitializeComponent();
        }
        public LabeledDateTimePicker(string title, DateTime date, string error = null, Brush backgroundColor = null)
        {
            InitializeComponent();
            Title = title;
            Date = date;
            Error = error;
            BackgroundColor = backgroundColor;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
