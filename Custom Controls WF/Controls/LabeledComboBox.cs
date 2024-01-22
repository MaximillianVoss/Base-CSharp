using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Custom_Controls_WF.Controls
{
    public partial class LabeledComboBox : UserControl
    {
        #region Поля

        #endregion

        #region Свойства
        public event EventHandler SelectedIndexChanged
        {
            add => this.cbItems.SelectedIndexChanged += value;
            remove => this.cbItems.SelectedIndexChanged -= value;
        }
        public string Title
        {
            set => this.lblTitle.Text = value;
            get => this.lblTitle.Text.ToString();
        }
        public List<String> Items
        {
            set
            {
                if (value != null)
                {
                    foreach (string item in value)
                    {
                        _ = this.cbItems.Items.Add(item);
                    }
                }
            }
            get => this.cbItems.Items.OfType<string>().ToList();
        }
        public string Error
        {
            set
            {
                if (value == null || value == String.Empty)
                {
                    this.lblError.Text = String.Empty;
                }
                else
                {
                    this.lblError.Text = value;
                }
            }
            get => this.lblError.Text.ToString();
        }
        public Color BackgroundColor
        {
            set => this.BackColor = value;
            get => this.BackColor;
        }
        public bool IsEnabled
        {
            set => this.cbItems.Enabled = value;
            get => this.cbItems.Enabled;
        }
        public Object DataContext
        {
            set => this.cbItems.SelectedItem = value;
            get => this.cbItems.SelectedItem;
        }
        public int SelectedIndex
        {
            set
            {
                if (value < this.cbItems.Items.Count)
                {
                    this.cbItems.SelectedIndex = value;
                }
            }
            get => this.cbItems.SelectedIndex;
        }
        public string SelectedItem
        {
            get
            {
                if (this.cbItems.SelectedIndex >= 0)
                {
                    return this.cbItems.SelectedItem.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public new string Text
        {
            set => this.cbItems.Text = value;
            get => this.cbItems.Text;
        }
        #endregion

        #region Методы
        public void Add(object item)
        {
            _ = this.cbItems.Items.Add(item);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public LabeledComboBox() //: this("Заголовок")
        {
            this.InitializeComponent();
        }

        public LabeledComboBox(string title, List<string> items = null, string error = null, bool isEditable = true)
        {
            this.InitializeComponent();
            this.Title = title;
            this.Items = items;
            this.Error = error;
            this.IsEnabled = isEditable;
            this.SelectedIndex = -1;
        }


        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void cbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedIndex = this.cbItems.SelectedIndex;
        }

        #endregion


    }
}
