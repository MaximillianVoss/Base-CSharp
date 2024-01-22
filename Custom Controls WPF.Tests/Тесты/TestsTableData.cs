using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomControlsWPF.Tests
{
    [TestClass]
    public class TestsTableData
    {
        [TestMethod]
        public void AddDisplayColumn_AddsCorrectColumn_WhenColumnExists()
        {
            // Arrange
            var columnNames = new List<string> { "Column1", "Column2", "Column3" };
            var tableData = new TableData("TestTitle", "Test Display Title", typeof(object), columnNames);

            // Act
            tableData.AddDisplayColumn("Column2");

            // Assert
            CollectionAssert.Contains(tableData.DisplayColumnNames, "Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddDisplayColumn_ThrowsException_WhenColumnDoesNotExist()
        {
            // Arrange
            var columnNames = new List<string> { "Column1", "Column2" };
            var tableData = new TableData("TestTitle", "Test Display Title", typeof(object), columnNames);

            // Act
            tableData.AddDisplayColumn("NonExistentColumn");

            // Assert is handled by the ExpectedException
        }

        [TestMethod]
        public void RemoveDisplayColumn_RemovesColumnCorrectly_WhenColumnExists()
        {
            // Arrange
            var columnNames = new List<string> { "Column1", "Column2", "Column3" };
            var tableData = new TableData("TestTitle", "Test Display Title", typeof(object), columnNames);
            tableData.AddDisplayColumn("Column2");

            // Act
            tableData.RemoveDisplayColumn("Column2");

            // Assert
            CollectionAssert.DoesNotContain(tableData.DisplayColumnNames, "Column2");
        }
    }
}
