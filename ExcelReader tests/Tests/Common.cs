using System.Collections.Generic;

namespace ExcelReader_tests.Tests
{
    internal static class Common
    {
        public static string GetParentFoder(string path)
        {
            path = path.Replace("\\\\", "\\");
            int lastIndex = path.LastIndexOf('\\');
            if (lastIndex != -1)
            {
                return path.Substring(0, path.Length - (path.Length - lastIndex));
            }
            else
            {
                return path;
            }
        }
        public static string GetParentFoder(string path, int levels)
        {
            for (int i = 0; i < levels; i++)
            {
                path = GetParentFoder(path);
            }

            return path;
        }
        public static List<string> GetStrings(string prefix, int count)
        {
            var list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(string.Format("{0}{1}", prefix, i));
            }
            return list;
        }
    }
}
