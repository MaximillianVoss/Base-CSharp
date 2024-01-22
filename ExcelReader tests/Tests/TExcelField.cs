using ExcelReader.ExcelDocument;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExcelReader_tests.Tests
{
    [TestClass]
    public class TExcelField
    {
        [TestMethod]
        public void TestEmptyConstructor()
        {
            var field = new ExcelField();
            Assert.AreEqual(string.Empty, field.Title);
            Assert.AreEqual(string.Empty, field.Value);
            Assert.AreEqual(string.Empty, field.Description);
        }
        [TestMethod]
        public void TestFullConstructor()
        {
            for (int i = 0; i < 10; i++)
            {
                string title = String.Format("title {0}", i);
                string value = String.Format("value {0}", i);
                string description = String.Format("description {0}", i);
                var field = new ExcelField(title, value, description);
                Assert.AreEqual(title, field.Title);
                Assert.AreEqual(value, field.Value);
                Assert.AreEqual(description, field.Description);
            }
        }
    }
}
