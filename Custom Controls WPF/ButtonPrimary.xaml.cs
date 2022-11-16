using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace CustomControlsWPF
{
    public enum Colors
    {
        primary,
        secondary,
        success,
        danger,
        warning,
        info,
        light,
        dark
    }
    /// <summary>
    /// Логика взаимодействия для ButtonPrimary.xaml
    /// </summary>
    public partial class ButtonPrimary : UserControl
    {
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabItem));

        public event RoutedEventHandler Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }

        private void ClickHadler(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }

        private readonly Dictionary<Colors, SolidColorBrush> brushes;

        public Double ButtonFontSize { set => btn.FontSize = value; get => btn.FontSize; }

        public String Text { set => btn.Content = value; get => btn.Content.ToString(); }

        public Colors Color
        {
            set
            {
                try
                {
                    btn.Background = brushes[value];
                }
                catch (Exception ex)
                {
                    btn.Background = brushes[Colors.primary];
                    btn.Content = ex.Message;
                }
            }
        }


        public ButtonPrimary()
        {
            InitializeComponent();
            brushes = new Dictionary<Colors, SolidColorBrush>() {
                { Colors.primary,GetColorBrush("#ff0d6efd")},
                { Colors.secondary,GetColorBrush("#6c757d")},
                { Colors.success,GetColorBrush("#198754")},
                { Colors.danger,GetColorBrush("#dc3545")},
                { Colors.warning,GetColorBrush("#ffc107")},
                { Colors.info,GetColorBrush("#0dcaf0")},
                { Colors.light,GetColorBrush("#f8f9fa")},
                { Colors.dark,GetColorBrush("#212529")}
            };
            if (btn != null)
            {
                btn.Click += ClickHadler;
            }
        }

        /// <summary>
        /// Создает кисть указаного цвета из 16-разрядной строки
        /// </summary>
        /// <param name="hexStr">Строка с 16-разрядным числом, например '#ffaacc'</param>
        /// <returns></returns>
        private SolidColorBrush GetColorBrush(String hexStr)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexStr.ToLower()));
        }
    }
}
