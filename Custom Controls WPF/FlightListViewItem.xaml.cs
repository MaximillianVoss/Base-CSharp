using System;
using System.Windows.Controls;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для FlightListViewItem.xaml
    /// </summary>
    public partial class FlightListViewItem : UserControl
    {

        #region Поля
        private bool isBuyClicked;
        private int idFlight;
        private DateTime departureDate;
        #endregion

        #region Свойства
        public string Price
        {
            get => (string)lblPrice.Content;
            set => lblPrice.Content = value;
        }
        public string FromTo
        {
            get => (string)lblFromToTitle.Content;
            set => lblFromToTitle.Content = value;
        }
        public string FromToTime
        {
            get => (string)lblFromToValue.Content;
            set => lblFromToValue.Content = value;
        }
        public string Duration
        {
            get => (string)lblDurationValue.Content;
            set => lblDurationValue.Content = value;
        }
        public string Transfer
        {
            get => (string)lblTransferValue.Content;
            set => lblTransferValue.Content = value;
        }
        public bool IsBuyClicked { get => isBuyClicked; set => isBuyClicked = value; }
        public int IdFlight { get => idFlight; set => idFlight = value; }
        public DateTime DepartureDate { get => departureDate; set => departureDate = value; }
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public FlightListViewItem() : this(-1, "Не указана цена", "XXX-XXX", "XXч. XXмин.", "Без пересадок", DateTime.Now)
        {
            InitializeComponent();
        }
        public FlightListViewItem(int idFlight, string price, string fromTo, string fromToTime, string duration, DateTime departureDate, string transfer = "Без пересадок")
        {
            InitializeComponent();
            this.idFlight = idFlight;
            Price = price;
            FromTo = fromTo;
            FromToTime = fromToTime;
            DepartureDate = departureDate;
            Duration = duration;
            Transfer = transfer;
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void btnBuy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            isBuyClicked = true;
        }
        #endregion

    }
}
