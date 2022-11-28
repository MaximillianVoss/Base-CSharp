﻿using ExcelReader.ExcelDocument;
using ExcelReader.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExcelReader_tests.Tests
{
    [TestClass]
    public class TCsv
    {
        private ExcelParser parser = new ExcelParser();
        public ExcelParser Parser { get => this.parser; set => this.parser = value; }
        private string TestsFilesFolder => String.Format("{0}\\{1}", Common.GetParentFoder(AppDomain.CurrentDomain.BaseDirectory, 2), "Test files\\");
        [TestMethod]
        public void ReadEmpty()
        {
            ExcelDocument document = this.Parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test empty.csv"));
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
            ExcelDocument document = this.Parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test empty only headers.csv"));
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
            ExcelDocument document = this.Parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test empty only headers and description.csv"), ';', "Лист1", true, true);
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
        public void ReadSmall()
        {
            ExcelDocument document = this.Parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test simple table.csv"), ';', "Лист1", true, true);
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
            ExcelDocument document = this.Parser.Parse(String.Format("{0}{1}", this.TestsFilesFolder, "Test large.csv"), ';', "Лист1", true, true);
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Headers);
            Assert.IsNotNull(document.Rows);
            int testColumnsCount = 35;
            Assert.AreEqual(testColumnsCount, document.HeadersCount);
            Assert.AreEqual(20000, document.RowsCount);
        }
    }
}
