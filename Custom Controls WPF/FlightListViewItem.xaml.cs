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
            get => (string)this.lblPrice.Content;
            set => this.lblPrice.Content = value;
        }
        public string FromTo
        {
            get => (string)this.lblFromToTitle.Content;
            set => this.lblFromToTitle.Content = value;
        }
        public string FromToTime
        {
            get => (string)this.lblFromToValue.Content;
            set => this.lblFromToValue.Content = value;
        }
        public string Duration
        {
            get => (string)this.lblDurationValue.Content;
            set => this.lblDurationValue.Content = value;
        }
        public string Transfer
        {
            get => (string)this.lblTransferValue.Content;
            set => this.lblTransferValue.Content = value;
        }
        public bool IsBuyClicked
        {
            get => this.isBuyClicked; set => this.isBuyClicked = value;
        }
        public int IdFlight
        {
            get => this.idFlight; set => this.idFlight = value;
        }
        public DateTime DepartureDate
        {
            get => this.departureDate; set => this.departureDate = value;
        }
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public FlightListViewItem() : this(-1, "Не указана цена", "XXX-XXX", "XXч. XXмин.", "Без пересадок", DateTime.Now)
        {
            this.InitializeComponent();
        }
        public FlightListViewItem(int idFlight, string price, string fromTo, string fromToTime, string duration, DateTime departureDate, string transfer = "Без пересадок")
        {
            this.InitializeComponent();
            this.idFlight = idFlight;
            this.Price = price;
            this.FromTo = fromTo;
            this.FromToTime = fromToTime;
            this.DepartureDate = departureDate;
            this.Duration = duration;
            this.Transfer = transfer;
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void btnBuy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.isBuyClicked = true;
        }
        #endregion

    }
}
