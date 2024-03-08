using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CustomControlsWPF
{
    /// <summary>
    /// Хранит данные из таблиц типа DbSet.
    /// </summary>
    public class TableData
    {
        #region Поля

        #endregion

        #region Свойства
        /// <summary>
        /// Получает или задает заголовок таблицы.
        /// </summary>
        public String Title
        {
            set; get;
        }
        /// <summary>
        /// Получает или задает отображаемый заголовок таблицы.
        /// </summary>
        public String TitleDisplay
        {
            set; get;
        }

        /// <summary>
        /// Получает или задает тип элементов, которые хранятся в коллекции.
        /// </summary>
        public Type ItemsType
        {
            set; get;
        }
        /// <summary>
        /// Получает или задает имена колонок таблицы.
        /// </summary>
        public List<string> ColumnNames
        {
            get; set;
        }
        /// <summary>
        /// Получает или задает отображаемые колонки таблицы.
        /// </summary>
        public List<string> DisplayColumnNames
        {
            get; set;
        }
        /// <summary>
        /// Получает или задает коллекцию всех элементов.
        /// </summary>
        public ObservableCollection<object> ItemsAll { get; set; } = new ObservableCollection<object>();
        #endregion

        #region Методы
        // Получение элементов указанной страницы из коллекции ItemsAll.
        // Нумерация страниц начинается с 1.
        /// <summary>
        /// Получает элементы указанной страницы из коллекции ItemsAll.
        /// </summary>
        /// <param name="pageNumber">Номер страницы (начиная с 1).</param>
        /// <param name="itemsPerPage">Количество элементов на странице.</param>
        /// <returns>Коллекция элементов страницы.</returns>
        public IEnumerable GetPage(int pageNumber, int itemsPerPage)
        {
            return this.ItemsAll
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
        }

        /// <summary>
        /// Получает количество строк в коллекции ItemsAll.
        /// </summary>
        /// <returns>Количество строк.</returns>
        public int GetRowsCount()
        {
            return this.ItemsAll.Count;
        }

        /// <summary>
        /// Проверяет, существует ли указанная страница.
        /// </summary>
        /// <param name="pageNumber">Номер страницы (начиная с 1).</param>
        /// <param name="itemsPerPage">Количество элементов на странице.</param>
        /// <returns>True, если страница существует, иначе False.</returns>
        public bool HasPage(int pageNumber, int itemsPerPage)
        {
            return this.ItemsAll.Skip((pageNumber - 1) * itemsPerPage).Any();
        }

        // Метод фильтрации элементов по определенному условию. Вы можете его изменить под ваши нужды.
        /// <summary>
        /// Применяет фильтр к элементам коллекции на основе заданного условия.
        /// </summary>
        /// <param name="filterCriteria">Условие фильтрации.</param>
        public void ApplyFilter(Predicate<object> filterCriteria)
        {
            var filtered = new ObservableCollection<object>();

            foreach (object item in this.ItemsAll)
            {
                if (filterCriteria(item))
                {
                    filtered.Add(item);
                }
            }

            this.ItemsAll = filtered;
        }

        /// <summary>
        /// Сбрасывает фильтр и возвращает все элементы в коллекцию.
        /// </summary>
        public void ResetFilter()
        {
            // В данной реализации фильтрация напрямую меняет ItemsAll.
            // Если вам нужно восстановить первоначальное состояние, вам придется хранить копию исходной коллекции.
        }

        /// <summary>
        /// Добавляет коллекцию элементов к существующей коллекции ItemsAll.
        /// </summary>
        /// <param name="items">Коллекция элементов для добавления.</param>
        public void AddRange(IEnumerable items)
        {
            if (this.ItemsAll == null)
                this.ItemsAll = new ObservableCollection<object>();
            foreach (object item in items)
            {
                this.ItemsAll.Add(item);
            }
            // Если нужно сразу сбросить фильтр после добавления элементов:
            this.ResetFilter();
        }

        /// <summary>
        /// Заменяет элемент в коллекции ItemsAll на основе идентификатора id.
        /// </summary>
        /// <param name="newItem">Новый элемент, который следует вставить вместо существующего.</param>
        /// <exception cref="ArgumentException">Вызывается, если <paramref name="newItem"/> не содержит свойства id или если элемент с таким id не найден в коллекции.</exception>
        public void ReplaceItemById(object newItem)
        {
            // Проверяем, имеет ли объект свойство id
            System.Reflection.PropertyInfo idProperty = newItem.GetType().GetProperty("id") ?? throw new ArgumentException("Объект не содержит свойства id.");

            // Получаем значение id для нового элемента
            object newId = idProperty.GetValue(newItem) ?? throw new ArgumentException("Значение id не может быть null.");

            object itemToReplace = this.ItemsAll.FirstOrDefault(item =>
                item.GetType().GetProperty("id")?.GetValue(item) == newId) ?? throw new ArgumentException($"Элемент с id = {newId} не найден.");

            int index = this.ItemsAll.IndexOf(itemToReplace);
            this.ItemsAll[index] = newItem;
        }

        /// <summary>
        /// Очищает коллекцию ItemsAll.
        /// </summary>
        public void Clear()
        {
            this.ItemsAll.Clear();
        }

        /// <summary>
        /// Добавляет отображаемую колонку к коллекции DisplayColumnNames.
        /// </summary>
        /// <param name="columnName">Имя колонки для добавления.</param>
        public void AddDisplayColumn(string columnName)
        {
            if (!this.ColumnNames.Contains(columnName))
            {
                throw new ArgumentException($"Столбец с именем {columnName} отсутствует в данных.");
            }
            if (this.ColumnNames.Contains(columnName) && !this.DisplayColumnNames.Contains(columnName))
            {
                this.DisplayColumnNames.Add(columnName);
            }
        }

        /// <summary>
        /// Добавляет список отображаемых колонок к коллекции DisplayColumnNames.
        /// </summary>
        /// <param name="displayColumnNames">Список имен колонок для добавления.</param>
        public void AddDisplayColumns(List<string> displayColumnNames)
        {
            foreach (string item in displayColumnNames)
                this.AddDisplayColumn(item);
            //this.DisplayColumnNames = displayColumnNames.Where(c => ColumnNames.Contains(c)).ToList();
        }

        /// <summary>
        /// Устанавливает список отображаемых колонок в коллекцию DisplayColumnNames.
        /// </summary>
        /// <param name="displayColumnNames">Список имен колонок для установки.</param>
        public void SetDisplayColumns(List<string> displayColumnNames)
        {
            this.DisplayColumnNames.Clear();
            this.AddDisplayColumns(displayColumnNames);
        }

        /// <summary>
        /// Удаляет отображаемую колонку из коллекции DisplayColumnNames.
        /// </summary>
        /// <param name="columnName">Имя колонки для удаления.</param>
        public void RemoveDisplayColumn(string columnName)
        {
            if (this.DisplayColumnNames.Contains(columnName))
            {
                _ = this.DisplayColumnNames.Remove(columnName);
            }
        }
        #endregion

        #region Конструкторы/Деструкторы
        // Конструктор
        /// <summary>
        /// Инициализирует новый экземпляр класса TableData с заданными параметрами.
        /// </summary>
        /// <param name="title">Заголовок таблицы.</param>
        /// <param name="titleDisplay">Отображаемый заголовок таблицы.</param>
        /// <param name="itemsType">Тип элементов, которые хранятся в коллекции.</param>
        /// <param name="columnNames">Имена колонок таблицы.</param>
        /// <param name="items">Коллекция элементов для инициализации. Может быть null.</param>
        public TableData(String title, String titleDisplay, Type itemsType, List<string> columnNames, ObservableCollection<object> items)
        {
            this.Title = title;
            this.TitleDisplay = titleDisplay;
            this.ItemsType = itemsType;
            this.ColumnNames = columnNames ?? throw new ArgumentNullException(nameof(columnNames));
            this.DisplayColumnNames = columnNames.Where(c => this.ColumnNames.Contains(c)).ToList();
            this.ItemsAll = items;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса TableData с заданными параметрами.
        /// </summary>
        /// <param name="title">Заголовок таблицы.</param>
        /// <param name="titleDisplay">Отображаемый заголовок таблицы.</param>
        /// <param name="itemsType">Тип элементов, которые хранятся в коллекции.</param>
        /// <param name="columnNames">Имена колонок таблицы.</param>
        public TableData(String title, String titleDisplay, Type itemsType, List<string> columnNames) :
            this(title, titleDisplay, itemsType, columnNames, null)
        {

        }

        /// <summary>
        /// Инициализирует новый экземпляр класса TableData на основе существующего экземпляра.
        /// </summary>
        /// <param name="other">Существующий экземпляр класса TableData для копирования.</param>
        public TableData(TableData other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            this.Title = other.Title;
            this.TitleDisplay = other.TitleDisplay;
            this.ItemsType = other.ItemsType;

            // Копирование списка имен колонок
            this.ColumnNames = new List<string>(other.ColumnNames);

            // Копирование элементов. Здесь мы предполагаем, что items - это простой объект и нам не нужна глубокая копия.
            // Если объекты в списке items сложные, то возможно вам потребуется выполнить глубокое копирование.
            if (other.ItemsAll != null)
            {
                this.ItemsAll = new ObservableCollection<object>(other.ItemsAll);
            }
            else
            {
                this.ItemsAll = null;
            }
        }

        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
