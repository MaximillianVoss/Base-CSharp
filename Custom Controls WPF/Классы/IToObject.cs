namespace CustomControlsWPF
{
    /// <summary>
    /// Класс-обертка для элементов, хранящихся в выпадающем списке
    /// </summary>
    public class ComboBoxItemObject
    {


        #region Поля

        #endregion

        #region Свойства
        /// <summary>
        /// ID элемента
        /// </summary>
        public int Id
        {
            get; set;
        }
        /// <summary>
        /// Текстовое отображение в списке
        /// </summary>
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// Данные, привязанные непосретсвенно к элементу, свойство может быть null
        /// </summary>
        public object Data
        {
            set; get;
        }
        #endregion

        #region Методы
        public override string ToString()
        {
            return this.Title;
        }
        #endregion

        #region Конструкторы/Деструкторы
        public ComboBoxItemObject(int id, string title, object data)
        {
            this.Id = id;
            this.Title = title;
            this.Data = data;
        }
        public ComboBoxItemObject(int id, string title) : this(id, title, null) { }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion


    }
    public interface IToObject
    {
        /// <summary>
        /// Делает удобным использование LabeledComboBox - позволяет поместить значения в нужные поля.
        /// Очень важны поля id и title из них LabeledComboBox берет информацию
        /// </summary> 
        /// <code>
        /// return new
        ///      {
        ///            id = this.id,
        ///            title = String.Format("{0} {1}", this.Descriptors.code, this.Descriptors.title),
        ///            titleShort = this.Descriptors.titleShort,
        ///            description = this.Descriptors.description,
        ///            code = this.Descriptors.code
        ///      };
        /// </code>
        /// <returns></returns>
        ComboBoxItemObject ToObject();
    }
}
