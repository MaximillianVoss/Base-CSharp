using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControlsWPF.Классы
{
    public class CustomEventArgs : EventArgs
    {

        #region Поля
        private object data;
        private bool isList;
        private Type dataType;
        #endregion

        #region Свойства
        public bool IsList => this.isList;
        public Type DataType => this.Data.GetType();
        public object Data
        {
            get => this.data;
            set
            {
                this.data = value;
                if (this.data != null)
                {
                    this.dataType = value.GetType();
                    this.isList = this.data is IList;
                }
                else
                {
                    this.isList = false;
                    this.dataType = typeof(void);
                }
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию, который инициализирует экземпляр CustomEventArgs со значениями по умолчанию.
        /// </summary>
        public CustomEventArgs()
        {
            // Инициализируем значения по умолчанию.
            this.isList = false; // or true, зависит от того, что считается значением по умолчанию
            this.dataType = typeof(void); // или другой тип данных по умолчанию
            this.Data = null; // или другое значение по умолчанию
        }

        /// <summary>
        /// Конструктор, который инициализирует экземпляр CustomEventArgs с использованием предоставленных данных.
        /// </summary>
        /// <param name="data">Данные для инициализации экземпляра.</param>
        public CustomEventArgs(object data)
        {
            this.isList = data is IList;
            this.dataType = data?.GetType() ?? typeof(void);
            this.Data = data;
        }

        #endregion

    }
}
