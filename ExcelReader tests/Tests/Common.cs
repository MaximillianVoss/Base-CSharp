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
    }
}
