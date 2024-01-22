using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControlsWPF
{
    /// <summary>
    /// Логика взаимодействия для PagedDataGrid.xaml
    /// </summary>
    public partial class PagedDataGrid : UserControl
    {


        #region Поля
        public TableData tableData;
        private object selectedItem;
        private int? selectedItemId;
        #endregion

        #region Свойства
        public TableData TableData
        {
            get => this.tableData;
            set
            {
                this.tableData = value;

                // Устанавливаем общее количество элементов
                this.pcPages.ItemsCount = value.GetRowsCount();

                // Рассчитываем общее количество страниц
                this.pcPages.PagesCount = (int)Math.Ceiling((double)value.GetRowsCount() / this.pcPages.PageSize);

                if (this.pcPages.PagesCount > 0)
                    this.pcPages.CurrentPage = 1;

                // Обновляем DataGrid с учетом текущей страницы и размера страницы
                this.UpdateDataGrid(value, this.pcPages.CurrentPage, this.pcPages.PageSize);
            }
        }
        public object SelectedItem
        {
            get => this.selectedItem;
            set
            {
                this.selectedItem = value;

                if (this.selectedItem != null)
                {
                    System.Reflection.PropertyInfo idProperty = this.selectedItem.GetType()
    .GetProperties()
    .FirstOrDefault(p => p.Name.StartsWith("Id", StringComparison.OrdinalIgnoreCase) || p.Name.StartsWith("id"));

                    if (idProperty != null && idProperty.PropertyType == typeof(int))
                    {
                        this.selectedItemId = (int)idProperty.GetValue(this.selectedItem);
                    }
                    else
                    {
                        this.selectedItemId = null;
                    }
                }
                else
                {
                    this.selectedItemId = null;
                }
            }
        }
        public int? SelectedItemId => this.selectedItemId;
        public string Title
        {
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.lblTitle.Content = string.Empty;
                    this.lblTitle.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.lblTitle.Content = value;
                }
            }
            get => this.lblTitle.Content.ToString();
        }
        public int CurrentPage
        {
            set => this.pcPages.CurrentPage = value;
            get => this.pcPages.CurrentPage;
        }
        public int PageSize
        {
            set => this.pcPages.PageSize = value;
            get => this.pcPages.PageSize;
        }
        public int PagesCount
        {
            set => this.pcPages.PagesCount = value;
            get => this.pcPages.PagesCount;
        }
        #endregion

        #region События

        public delegate void CellClickChangedEventHandler(object sender, EventArgs e);

        public event CellClickChangedEventHandler LeftClickSelectedCellChanged;
        public event CellClickChangedEventHandler RightClickSelectedCellChanged;
        public event EventHandler AddMenuItemClicked;
        public event EventHandler EditMenuItemClicked;
        public event EventHandler DeleteMenuItemClicked;
        public event EventHandler RefreshMenuItemClicked;

        #endregion

        #region Методы

        #region Методы для управления столбцами

        public void AddDisplayColumn(string columnName)
        {
            //if (!this.TableData.ColumnNames.Contains(columnName))
            //{
            //    throw new ArgumentException($"Столбец с именем {columnName} отсутствует в данных.");
            //}

            if (!this.dgData.Columns.Any(c => (c.Header as string).Replace(" ", "_").Equals(columnName)))
            {
                this.TableData.ColumnNames.Add(columnName);
                this.Update();
            }
        }

        public void RemoveDisplayColumn(string columnName)
        {
            if (this.dgData.Columns.Any(c => (c.Header as string).Replace(" ", "_").Equals(columnName)))
            {
                _ = this.TableData.ColumnNames.Remove(columnName);
                this.Update();
            }
        }

        public void SetDisplayColumns(List<string> columnNames)
        {
            this.tableData.DisplayColumnNames.Clear();
            foreach (string item in columnNames)
                this.AddDisplayColumn(item);
        }
        #endregion

        /// <summary>
        /// Заполняет элемент управления DataGrid записями из указанной таблицы
        /// </summary>
        /// <param name="tableData">имя таблицы</param>
        private void UpdateDataGrid(TableData tableData, int pageNumber, int itemsPerPage)
        {
            #region Удаление предыдущих колонок
            this.dgData.AutoGenerateColumns = false;
            this.dgData.Columns.Clear();
            #endregion

            #region Определение диапазона элементов для отображения
            int startIndex = (pageNumber - 1) * itemsPerPage;
            var itemsToShow = tableData.ItemsAll.Skip(startIndex).Take(itemsPerPage).ToList();
            #endregion

            #region Задаем источник данных
            this.dgData.ItemsSource = null;
            this.dgData.DataContext = tableData;
            this.dgData.ItemsSource = itemsToShow;
            #endregion

            #region Обновляем количество страниц
            int rowCount = tableData.GetRowsCount();
            int pageSize = this.pcPages.PageSize;
            this.pcPages.PagesCount = (rowCount + pageSize - 1) / pageSize; // Учтите округление вверх
            #endregion

            #region Добавляем колонки
            if (tableData.DisplayColumnNames == null || !tableData.DisplayColumnNames.Any())
            {
                // Если DisplayColumnNames не заданы, добавляем все колонки из ColumnNames
                if (tableData.ColumnNames != null && tableData.ItemsAll.Any()) // Добавил проверку на наличие элементов в ItemsAll
                {
                    object firstItem = tableData.ItemsAll.FirstOrDefault();
                    if (firstItem != null)
                    {
                        foreach (string columnName in tableData.ColumnNames)
                        {
                            if (firstItem.GetType().GetProperty(columnName) != null)
                            {
                                var column = new DataGridTextColumn
                                {
                                    Header = columnName.Replace('_', ' '),
                                    Binding = new System.Windows.Data.Binding(columnName)
                                };
                                this.dgData.Columns.Add(column);
                            }
                        }
                    }
                }
            }
            else
            {
                // В противном случае добавляем только указанные в DisplayColumnNames
                if (tableData.ItemsAll.Any()) // Добавил проверку на наличие элементов в ItemsAll
                {
                    object firstItem = tableData.ItemsAll.FirstOrDefault();
                    if (firstItem != null)
                    {
                        var properties = firstItem.GetType().GetProperties().ToDictionary(p => p.Name.Replace('_', ' ').ToLower(), p => p);

                        foreach (string columnName in tableData.DisplayColumnNames)
                        {
                            string normalizedColumnName = columnName.Replace('_', ' ').ToLower();

                            if (properties.TryGetValue(normalizedColumnName, out System.Reflection.PropertyInfo property))
                            {
                                var column = new DataGridTextColumn
                                {
                                    Header = columnName.Replace('_', ' '),
                                    Binding = new System.Windows.Data.Binding(property.Name)
                                };
                                this.dgData.Columns.Add(column);
                            }
                            else
                            {
                                throw new ArgumentException($"Столбец {columnName} отсутствует в источнике данных.");
                            }
                        }
                    }
                }
            }
            #endregion



        }


        /// <summary>
        /// Заменяет элемент в коллекции <see cref="ItemsAll"/> на основе идентификатора id.
        /// </summary>
        /// <param name="newItem">Новый элемент, который следует вставить вместо существующего.</param>
        /// <exception cref="ArgumentException">Вызывается, если <paramref name="newItem"/> не содержит свойства id или если элемент с таким id не найден в коллекции.</exception>
        public void ReplaceItemById(object newItem)
        {
            this.TableData.ReplaceItemById(newItem);
        }
        /// <summary>
        /// Обновляет таблицу в соотвествии с текущим внутренним набором данных
        /// </summary>
        public void Update()
        {
            this.UpdateDataGrid(this.TableData, this.pcPages.CurrentPage, this.pcPages.PageSize);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public PagedDataGrid()
        {
            this.InitializeComponent();
            this.dgData.SelectionChanged += this.DgData_SelectionChanged;
            this.dgData.PreviewMouseRightButtonDown += this.DgData_PreviewMouseRightButtonDown;

            // Подписываемся на события изменения страницы и размера страницы
            this.pcPages.PageChanged += this.pcPages_PageChanged;
            this.pcPages.PageSizeChanged += this.pcPages_PageSizeChanged;
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        private void DgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.dgData.SelectedItem != null)
            {
                this.SelectedItem = this.dgData.SelectedItem;
                LeftClickSelectedCellChanged?.Invoke(this, EventArgs.Empty); // вызываем событие при изменении выбранной ячейки левым кликом
            }
        }

        private void DgData_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            object clickedItem = (sender as DataGrid).SelectedItem;
            if (clickedItem != null)
            {
                this.SelectedItem = clickedItem;
                RightClickSelectedCellChanged?.Invoke(this, EventArgs.Empty); // вызываем событие при изменении выбранной ячейки правым кликом
            }
        }

        private void pcPages_PageChanged(object sender, PageChangedEventArgs e)
        {
            // Обновляем DataGrid при изменении текущей страницы
            this.UpdateDataGrid(this.TableData, e.NewPage, this.PageSize);
        }

        private void pcPages_PageSizeChanged(object sender, PageSizeChangedEventArgs e)
        {
            int newPagesCount = (this.TableData.GetRowsCount() + e.NewPageSize - 1) / e.NewPageSize;

            if (this.CurrentPage > newPagesCount)
            {
                this.CurrentPage = newPagesCount; // Установить на последнюю доступную страницу
            }

            // Обновляем DataGrid при изменении размера страницы
            this.UpdateDataGrid(this.TableData, this.CurrentPage, e.NewPageSize);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddMenuItemClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditMenuItemClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteMenuItemClicked?.Invoke(this, EventArgs.Empty);
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshMenuItemClicked?.Invoke(this, EventArgs.Empty);
        }
        #endregion

    }
}
