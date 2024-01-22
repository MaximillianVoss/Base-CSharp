using System.Windows.Controls;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для FlightListViewItem.xaml
    /// </summary>
    public partial class TicketListViewItem : UserControl
    {

        #region Поля
        private int idTicket;
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

        public int IdTicket
        {
            get => this.idTicket; set => this.idTicket = value;
        }
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public TicketListViewItem() : this("Не указана цена", "XXX-XXX", "XXч. XXмин.", "Без пересадок")
        {
            this.InitializeComponent();
        }

        public TicketListViewItem(string price, string fromTo, string fromToTime, string duration, string transfer = "Без пересадок")
        {
            this.InitializeComponent();
            this.Price = price;
            this.FromTo = fromTo;
            this.FromToTime = fromToTime;
            this.Duration = duration;
            this.Transfer = transfer;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
