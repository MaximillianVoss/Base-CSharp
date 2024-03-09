using CustomControlsWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Windows_and_Controls_Test
{

    public class ExampleObject : IToObject
    {
        public int SomeID
        {
            set; get;
        }
        public string SomeText
        {
            set; get;
        }
        public ExampleObject(int someID, string someText)
        {
            this.SomeID = someID;
            this.SomeText = someText;
        }

        public ComboBoxItemObject ToObject()
        {
            return new ComboBoxItemObject(SomeID, SomeText, this);
        }
    }
    /// <summary>
    /// Логика взаимодействия для WindowTestComboBoxes.xaml
    /// </summary>
    public partial class WindowTestComboBoxes : Window
    {


        #region Поля

        #endregion

        #region Свойства
        List<ExampleObject> ExampleObjects;
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        public WindowTestComboBoxes()
        {
            InitializeComponent();
            this.ExampleObjects = new List<ExampleObject>();
            for (int i = 0; i < 10; i++)
                ExampleObjects.Add(new ExampleObject(i + 1, String.Format("ExampleObject #{0}", i)));

            this.cmbTestObjectsList.Add(ExampleObjects);
        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void cmbTestObjectsList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (this.cmbTestObjectsList.SelectedIndex != -1)
            {
                var selectedItem = this.cmbTestObjectsList.Items[this.cmbTestObjectsList.SelectedIndex] as ComboBoxItemObject;
                this.cmbTestObjectsList.Error = String.Format("id:{0}; title:{1}; data:{2};",selectedItem.Id,selectedItem.Title,selectedItem.Data);
            }
        }
        #endregion




    }
}
