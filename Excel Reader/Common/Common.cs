namespace CSV_Reader.Common
{
    public static class Common
    {
        public static class Strings
        {
            public static class Errors
            {
                public static string fieldsCountNotEqual = "Количество полей в объекте и в документе не совпадает";
                public static string rowParseErroe = "Не удалось получить строку из таблицы документа Excel!";
                public static string headersCountNotMatch = "Количество столбцов в документе и таблице не совпадает!";
                public static string fieldsValuesCountNotMatch = "Количество полей не совпадает с количеством значений для полей";
            }
            public static class Warnings
            {

            }
            public static class Messages
            {

            }
            public static class Extensions
            {
                public static string xlsx = ".xlsx";
                public static string csv = ".csv";
            }


        }
    }
}
