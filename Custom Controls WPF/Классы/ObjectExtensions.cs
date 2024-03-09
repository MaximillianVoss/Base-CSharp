using System;

namespace CustomControlsWPF
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Получает указанное поле из объекта
        /// </summary>
        /// <param name="obj">Целевой объект.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение поля.</returns>
        public static object GetFieldValue(this object obj, string fieldName)
        {
            if (obj == null)
            {
                throw new Exception("Не передан объект для получения поля!");
            }

            if (String.IsNullOrEmpty(fieldName))
            {
                throw new Exception("Не передано имя поля!");
            }

            System.Reflection.PropertyInfo field = obj.GetType().GetProperty(fieldName);
            return field == null
                ? throw new Exception(Common.Strings.Errors.fieldIsNotFoundInObject)
                : field.GetValue(obj, null);
        }
        /// <summary>
        /// Проверяет, является ли тип объекта или его базовый тип указанным типом.
        /// </summary>
        /// <param name="obj">Объект для проверки.</param>
        /// <param name="type">Тип для сравнения.</param>
        /// <returns>True, если тип объекта или его базовый тип совпадает с указанным типом, иначе false.</returns>
        public static bool IsTypeOrBaseEqual(this object obj, Type type)
        {
            return obj.GetType() == type || obj.GetType().BaseType == type;
        }
        /// <summary>
        /// Проверяет, является ли тип объекта или его базовый тип указанным типом, и бросает исключение, если нет.
        /// </summary>
        /// <param name="obj">Объект для проверки.</param>
        /// <param name="expectedType">Тип для сравнения.</param>
        /// <exception cref="ArgumentException">Генерируется, если тип объекта не совпадает с ожидаемым.</exception>
        public static bool ValidateTypeOrBaseType(this object obj, Type expectedType)
        {
            Type actualType = obj.GetType();
            if (actualType != expectedType && actualType.BaseType != expectedType)
            {
                throw new ArgumentException($"Тип объекта '{actualType.FullName}' не соответствует ожидаемому типу '{expectedType.FullName}'.");
            }
            return true;
        }
        /// <summary>
        /// Проверяет, является ли тип объекта или его базовый тип указанным типом
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип для сравнения.</typeparam>
        /// <param name="obj">Объект для проверки.</param>
        public static bool ValidateTypeOrBaseType<T>(this object obj)
        {
            if (obj == null)
                return false;
            Type expectedType = typeof(T);
            Type actualType = obj.GetType();
            return !(actualType != expectedType && actualType.BaseType != expectedType);
        }
        /// <summary>
        /// Проверяет, является ли тип объекта или его базовый тип указанным типом, и бросает исключение, если нет.
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип для сравнения.</typeparam>
        /// <param name="obj">Объект для проверки.</param>
        /// <exception cref="ArgumentException">Генерируется, если тип объекта не совпадает с ожидаемым.</exception>
        public static bool ValidateTypeOrBaseTypeEx<T>(this object obj)
        {
            Type expectedType = typeof(T);
            Type actualType = obj.GetType();
            if (actualType != expectedType && actualType.BaseType != expectedType)
            {
                throw new ArgumentException($"Тип объекта '{actualType.FullName}' не соответствует ожидаемому типу '{expectedType.FullName}'.");
            }
            return true;
        }
    }
}
