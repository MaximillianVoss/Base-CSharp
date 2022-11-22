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
            set => this.lblTitle.Content = value;
            get => this.lblTitle.Content.ToString();
        }
        public DateTime? Date
        {
            set => this.dtpDate.SelectedDate = value;
            get => this.dtpDate.SelectedDate;
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
        public DateTime? StartDate
        {
            set => this.dtpDate.DisplayDateStart = value;
            get => this.dtpDate.DisplayDateStart;
        }
        public DateTime? EndDate
        {
            set => this.dtpDate.DisplayDateEnd = value;
            get => this.dtpDate.DisplayDateEnd;
        }
        #endregion

        #region Свойства

        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public LabeledDateTimePicker() : this("Заголовок", DateTime.Now)
        {
            this.InitializeComponent();
        }
        public LabeledDateTimePicker(string title, DateTime date, string error = null, Brush backgroundColor = null)
        {
            this.InitializeComponent();
            this.Title = title;
            this.Date = date;
            this.Error = error;
            this.BackgroundColor = backgroundColor;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
