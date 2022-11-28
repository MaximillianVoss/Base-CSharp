using ExcelReader.ExcelDocument;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ExcelReader_tests.Tests
{
    [TestClass]
    public class TExcelObject
    {
        [TestMethod]
        public void TestEmptyConstructor()
        {
            ExcelObject excelObject = new ExcelObject();
            Assert.AreEqual(0, excelObject.Id);
            Assert.IsNotNull(excelObject.Fields);
            Assert.AreEqual(0, excelObject.FieldsCount);
        }

        [TestMethod]
        public void TestFullConstructor()
        {
            int itemsCount = 100;
            List<string> titles = Common.GetStrings("title", itemsCount);
            List<string> values = Common.GetStrings("value", itemsCount);
            List<string> descriptions = Common.GetStrings("descriptions", itemsCount);
            List<ExcelField> fields = new List<ExcelField>();
            for (int i = 0; i < itemsCount; i++)
            {
                fields.Add(new ExcelField(titles[i], values[i], descriptions[i]));
            }
            ExcelObject excelObject = new ExcelObject(itemsCount, fields);
            Assert.AreEqual(itemsCount, excelObject.Fields.Count);
            Assert.AreEqual(itemsCount, excelObject.FieldsCount);
            for (int i = 0; i < itemsCount; i++)
            {
                Assert.AreEqual(titles[i], excelObject.Fields[i].Title);
                Assert.AreEqual(values[i], excelObject.Fields[i].Value);
                Assert.AreEqual(descriptions[i], excelObject.Fields[i].Description);
            }
        }
    }
}
