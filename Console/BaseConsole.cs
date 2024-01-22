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
            string str = this.GetStr(message);
            int value;
            bool isNumeric = Int32.TryParse(str, out value);
            if (isNumeric)
            {
                return value;
            }
            else
            {
                return this.GetInt(message);
            }
        }

        public double GetDouble(String message = "")
        {
            string str = this.GetStr(message);
            double vlaue;
            bool isNumeric = Double.TryParse(str, out vlaue);
            if (isNumeric)
            {
                return vlaue;
            }
            else
            {
                return this.GetDouble(message);
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
            foreach (T v in values)
            {
                this.Print(v, newLine);
            }
        }

        public void Print<T>(List<T> values, bool newLine = true)
        {
            foreach (T v in values)
            {
                this.Print(v, newLine);
            }
        }

        public void Print(String value, bool newLine = true)
        {
            this.Print<string>(value, newLine);
        }
        public void Pause(String message = "Press any key to continue...")
        {
            this.Print(message);
            _ = Console.ReadLine();
        }
    }
}
