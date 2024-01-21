using System;
using System.Windows;
using System.Windows.Controls;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для BaseControl.xaml
    /// </summary>
    public abstract partial class BaseControl : UserControl
    {


        #region Поля

        #endregion

        #region Свойства
        public abstract String Title
        {
            set; get;
        }
        public String Text
        {
            get; set;
        }
        public new Visibility Visibility
        {
            get => base.Visibility;
            set => base.Visibility = value;
        }
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public BaseControl()
        {
            //InitializeComponent();
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion



    }
}
