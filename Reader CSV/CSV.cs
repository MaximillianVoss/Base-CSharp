using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Reader
{
    enum SeparatorType
    {
        comma,
        colon
    }
    class CSVField
    {
        public String name { set; get; }
        public String value { set; get; }
        public CSVField() : this("", "")
        {

        }
        public CSVField(String name = "", String value = "")
        {
            this.name = name;
            this.value = value;
        }

    }
    class CSVObject
    {
        private List<CSVField> fields { set; get; }
        public void Add(CSVField field)
        {
            this.fields.Add(field);
        }
        public string Get(String fieldName)
        {
            string reuslt = null;
            foreach (var field in this.fields)
                if (field.name == fieldName)
                {
                    reuslt = field.value;
                    break;
                }
            return reuslt;
        }
        public string[] GetFieldValues()
        {
            string[] values = new string[this.fields.Count];
            for (int i = 0; i < this.fields.Count; i++)
                values[i] = this.fields[i].value;
            return values;
        }
        public CSVObject()
        {
            this.fields = new List<CSVField>();
        }
    }
    class CSV
    {
        private List<string> fields { set; get; }
        private List<CSVObject> values { set; get; }
        public CSV(String path, SeparatorType separatorType)
        {
            this.fields = new List<string>();
            this.values = new List<CSVObject>();
            using (var reader = new StreamReader(path))
            {
                for (int i = 0; !reader.EndOfStream; i++)
                {
                    string[] values = new string[0];
                    if (separatorType == SeparatorType.comma)
                        values = reader.ReadLine().Split(',');
                    if (separatorType == SeparatorType.colon)
                        values = reader.ReadLine().Split(';');
                    if (i == 0)
                        this.fields = values.ToList();
                    else
                    {
                        if (values.Count() == this.fields.Count)
                        {
                            CSVObject obj = new CSVObject();
                            for (int j = 0; j < this.fields.Count; j++)
                                obj.Add(new CSVField(this.fields[j], values[j]));
                            this.values.Add(obj);
                        }
                    }
                }

            }
        }

        public DataTable ToTable()
        {
            DataTable dataTable = new DataTable();
            foreach (var columnName in this.fields)
                dataTable.Columns.Add(columnName);
            foreach (var value in this.values)
                dataTable.Rows.Add(value.GetFieldValues());
            return dataTable;
        }
    }
}
