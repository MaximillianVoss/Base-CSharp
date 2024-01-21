using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для LabeledProgressBar.xaml
    /// </summary>
    public partial class LabeledProgressBar : UserControl
    {


        #region Поля

        #endregion

        #region Свойства
        /// <summary>
        /// Подпись
        /// </summary>
        public string Title
        {
            set => this.gbMain.Header = value;
            get => this.gbMain.Header.ToString();
        }
        /// <summary>
        /// Подпись, дублирует Title для совместимости 
        /// при замене стандартных элементов управления
        /// </summary>
        public string Header
        {
            set => this.Title = value;
            get => this.Title.ToString();
        }
        /// <summary>
        /// Максимум
        /// </summary>
        public double Maximum
        {
            set => this.pbMain.Maximum = value;
            get => this.pbMain.Maximum;
        }
        /// <summary>
        /// Минимум
        /// </summary>
        public double Minimum
        {
            set => this.pbMain.Minimum = value;
            get => this.pbMain.Minimum;
        }
        /// <summary>
        /// Текущее значение
        /// </summary>
        public double Value
        {
            set => this.pbMain.Value = value;
            get => this.pbMain.Value;
        }
        /// <summary>
        /// Цвет прогресс бара при заполнении
        /// </summary>
        public Brush ForegroundColor
        {

            set => this.pbMain.Foreground = value;
            get => this.pbMain.Foreground;
        }
        /// <summary>
        /// Видимость элемента управления
        /// </summary>
        //public new System.Windows.Visibility Visibility
        //{
        //    set => this.gbMain.Visibility = value;
        //    get => this.gbMain.Visibility;
        //}
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public LabeledProgressBar()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion



    }
}
