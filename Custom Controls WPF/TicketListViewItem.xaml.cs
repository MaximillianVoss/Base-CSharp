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

        public int IdTicket { get => idTicket; set => idTicket = value; }
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public TicketListViewItem() : this("Не указана цена", "XXX-XXX", "XXч. XXмин.", "Без пересадок")
        {
            InitializeComponent();
        }

        public TicketListViewItem(string price, string fromTo, string fromToTime, string duration, string transfer = "Без пересадок")
        {
            InitializeComponent();
            Price = price;
            FromTo = fromTo;
            FromToTime = fromToTime;
            Duration = duration;
            Transfer = transfer;
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
