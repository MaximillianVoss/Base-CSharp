using ExcelReader.ExcelDocument;
using ExcelReader.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExcelReader_tests.Tests
{
    [TestClass]
    public class TXlsx
    {
        private ExcelParser parser = new ExcelParser();

        private string TestsFilesFolder => String.Format("{0}\\{1}", Common.GetParentFoder(AppDomain.CurrentDomain.BaseDirectory, 2), "Test files\\");

        [TestMethod]
        public void ReadEmpty()
        {
            ExcelDocument document = this.parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test empty.xlsx"));
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Headers);
            Assert.IsNotNull(document.Rows);
            //Excel по умолчанию считает рабочий диапазон 1 даже в пустом файле!
            Assert.AreEqual(1, document.HeadersCount);
            Assert.AreEqual(0, document.RowsCount);
        }
        [TestMethod]
        public void ReadEmptyWithHeaders()
        {
            ExcelDocument document = this.parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test empty only headers.xlsx"));
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Headers);
            Assert.IsNotNull(document.Rows);
            int testColumnsCount = 10;
            Assert.AreEqual(testColumnsCount, document.HeadersCount);
            Assert.AreEqual(0, document.RowsCount);
            for (int i = 0; i < testColumnsCount; i++)
            {
                Assert.AreEqual(String.Format("Column {0}", i + 1), document.Headers[i].Title);
                Assert.AreEqual(String.Empty, document.Headers[i].Value);
                Assert.AreEqual(String.Empty, document.Headers[i].Description);
            }
        }
        [TestMethod]
        public void ReadEmptyWithHeadersAndDescription()
        {
            ExcelDocument document = this.parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test empty only headers and description.xlsx"), ';', "Лист1", true, true);
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Headers);
            Assert.IsNotNull(document.Rows);
            int testColumnsCount = 10;
            Assert.AreEqual(testColumnsCount, document.HeadersCount);
            Assert.AreEqual(0, document.RowsCount);
            for (int i = 0; i < testColumnsCount; i++)
            {
                Assert.AreEqual(String.Format("Column {0}", i + 1), document.Headers[i].Title);
                Assert.AreEqual(String.Empty, document.Headers[i].Value);
                Assert.AreEqual(String.Format("Description {0}", i + 1), document.Headers[i].Description);
            }
        }
        [TestMethod]
        public void ReadSipmle()
        {
            ExcelDocument document = this.parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test simple table.xlsx"), ';', "Лист1", true, true);
            document.Sort();
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Headers);
            Assert.IsNotNull(document.Rows);
            int testColumnsCount = 10;
            Assert.AreEqual(testColumnsCount, document.HeadersCount);
            Assert.AreEqual(100, document.RowsCount);
            for (int i = 0; i < testColumnsCount; i++)
            {
                Assert.AreEqual(String.Format("Column {0}", i + 1), document.Headers[i].Title);
                Assert.AreEqual(String.Empty, document.Headers[i].Value);
                Assert.AreEqual(String.Format("Description {0}", i + 1), document.Headers[i].Description);
            }
            for (int i = 0; i < document.RowsCount; i++)
            {
                var row = document.Rows[i];
                for (int j = 0; j < document.HeadersCount; j++)
                {
                    Assert.AreEqual(String.Format("value {0}", i + 1 + j), row[document.Headers[j].Title].Value);
                }
            }
        }
        [TestMethod]
        public void ReadLarge()
        {
            ExcelDocument document = this.parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test large.xlsx"), ';', "Лист1", true, true);
            document.Sort();
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Headers);
            Assert.IsNotNull(document.Rows);
            int testColumnsCount = 35;
            Assert.AreEqual(testColumnsCount, document.HeadersCount);
            Assert.AreEqual(200, document.RowsCount);
            //for (int i = 0; i < testColumnsCount; i++)
            //{
            //    Assert.AreEqual(String.Format("Column {0}", i + 1), document.Headers[i].Title);
            //    Assert.AreEqual(String.Empty, document.Headers[i].Value);
            //    Assert.AreEqual(String.Format("Description {0}", i + 1), document.Headers[i].Description);
            //}
            //for (int i = 0; i < document.RowsCount; i++)
            //{
            //    var row = document.Rows[i];
            //    for (int j = 0; j < document.HeadersCount; j++)
            //    {
            //        Assert.AreEqual(String.Format("value {0}", i + 1 + j), row[document.Headers[j].Title].Value);
            //    }
            //}
        }
    }
}
