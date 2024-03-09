using System;
using System.Collections.Generic;
using System.Windows;

namespace WPF_Windows_and_Controls_Test
{
    /// <summary>
    /// Логика взаимодействия для WindowTestSelection.xaml
    /// </summary>
    public partial class WindowTestSelection : Window
    {


        #region Поля
        Dictionary<String, Type> tests;
        #endregion

        #region Свойства

        #endregion

        #region Методы
        private void Init()
        {
            var testWindowsTypes =
                new List<Type>()
                {
                    typeof(WindowTestTextInputs),
                    typeof(WindowTestButtons),
                    typeof(WindowTestComboBoxes),
                    typeof(WindowTestBase)
                };
            this.tests = new Dictionary<String, Type>();
            foreach (Type item in testWindowsTypes)
                this.tests.Add(item.Name, item);
            foreach (KeyValuePair<string, Type> item in this.tests)
                _ = this.cmbTest.Items.Add(item.Key);
            this.cmbTest.SelectedIndex = this.cmbTest.Items.Count > 0 ? 0 : -1;
        }
        #endregion

        #region Конструкторы/Деструкторы
        public WindowTestSelection()
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Init();
        }


        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.cmbTest.SelectedIndex == -1)
                    throw new Exception("Выберите тестовую форму из списка.");
                Type value;
                bool keyExists = this.tests.TryGetValue(this.cmbTest.SelectedItem as String, out value);
                if (!keyExists)
                    throw new Exception("Невозможно создать форму, не добавлен тип формы в словарь.");
                Type formType = this.tests[this.cmbTest.SelectedItem as String];
                object instance = Activator.CreateInstance(formType);
                _ = (instance as Window).ShowDialog();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
        }

        #endregion

    }
}
