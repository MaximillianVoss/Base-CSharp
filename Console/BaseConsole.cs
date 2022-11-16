using System;
using System.Collections.Generic;

namespace Base_Console
{
    public class BaseConsole
    {
        public String GetStr(String message = "")
        {
            if (message != null && message != String.Empty)
            {
                Console.WriteLine(message);
            }

            return Console.ReadLine();
        }

        public int GetInt(String message = "")
        {
            string str = GetStr(message);
            int value = 0;
            bool isNumeric = Int32.TryParse(str, out value);
            if (isNumeric)
            {
                return value;
            }
            else
            {
                return GetInt(message);
            }
        }

        public double GetDouble(String message = "")
        {
            string str = GetStr(message);
            double vlaue = 0;
            bool isNumeric = Double.TryParse(str, out vlaue);
            if (isNumeric)
            {
                return vlaue;
            }
            else
            {
                return GetDouble(message);
            }
        }

        public void Print<T>(T value, bool newLine = true)
        {
            if (newLine)
            {
                Console.WriteLine(value.ToString());
            }
            else
            {
                Console.Write(value.ToString());
            }
        }

        public void Print<T>(T[] values, bool newLine = true)
        {
            foreach (var v in values)
            {
                Print<T>(v, newLine);
            }
        }

        public void Print<T>(List<T> values, bool newLine = true)
        {
            foreach (var v in values)
            {
                Print<T>(v, newLine);
            }
        }

        public void Print(String value, bool newLine = true)
        {
            Print<String>(value, newLine);
        }
        public void Pause(String message = "Press any key to continue...")
        {
            Print(message);
            Console.ReadLine();
        }
    }
}
