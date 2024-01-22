using System.Windows;
using Control = System.Windows.Controls.Control;
using MenuItem = System.Windows.Controls.MenuItem;

namespace CustomControlsWPF
{
    public class ControlState
    {


        #region Поля
        public bool isVisible;
        public System.Windows.Visibility visibility;
        #endregion

        #region Свойства
        public string Title
        {
            get; set;
        }
        public bool IsVisible
        {
            set
            {
                this.isVisible = value;
                this.visibility = this.isVisible ? Visibility.Visible : Visibility.Collapsed;
            }
            get => this.isVisible;
        }
        public bool IsEnabled
        {
            set; get;
        }
        public System.Windows.Visibility Visibility
        {
            set
            {
                this.IsVisible = value == System.Windows.Visibility.Visible;
                this.visibility = value;
            }
            get => this.visibility;
        }
        #endregion

        #region Методы
        public void UpdateControl(MenuItem menuItem)
        {
            menuItem.Header = this.Title;
            menuItem.IsEnabled = this.IsEnabled;
            menuItem.Visibility = this.Visibility;
        }
        public void Control(Control control)
        {
            control.Visibility = this.Visibility;
            control.IsEnabled = this.IsEnabled;
        }
        #endregion

        #region Конструкторы/Деструкторы
        public ControlState(string title, bool isEnabled, bool isVisible)
        {
            this.Title = title;
            this.IsEnabled = isEnabled;
            this.IsVisible = isVisible;
        }
        public ControlState(string title, bool isEnabled, Visibility visibility) : this(title, isEnabled, visibility == Visibility.Visible)
        {

        }
        public ControlState() : this(string.Empty, true, true)
        {

        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
}
