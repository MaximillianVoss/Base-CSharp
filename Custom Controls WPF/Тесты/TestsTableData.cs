using CustomControlsWPF;
using System;
using System.Collections.Generic;
using Xunit;

public class TestsTableData
{
    [Fact]
    public void AddDisplayColumn_AddsCorrectColumn_WhenColumnExists()
    {
        // Arrange
        var columnNames = new List<string> { "Column1", "Column2", "Column3" };
        var tableData = new TableData("TestTitle", "Test Display Title", typeof(object), columnNames);

        // Act
        tableData.AddDisplayColumn("Column2");

        // Assert
        Assert.Contains("Column2", tableData.DisplayColumnNames);
    }

    [Fact]
    public void AddDisplayColumn_ThrowsException_WhenColumnDoesNotExist()
    {
        // Arrange
        var columnNames = new List<string> { "Column1", "Column2" };
        var tableData = new TableData("TestTitle", "Test Display Title", typeof(object), columnNames);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => tableData.AddDisplayColumn("NonExistentColumn"));
        Assert.Equal("Столбец с именем NonExistentColumn отсутствует в данных.", ex.Message);
    }

    [Fact]
    public void RemoveDisplayColumn_RemovesColumnCorrectly_WhenColumnExists()
    {
        // Arrange
        var columnNames = new List<string> { "Column1", "Column2", "Column3" };
        var tableData = new TableData("TestTitle", "Test Display Title", typeof(object), columnNames);
        tableData.AddDisplayColumn("Column2");

        // Act
        tableData.RemoveDisplayColumn("Column2");

        // Assert
        Assert.DoesNotContain("Column2", tableData.DisplayColumnNames);
    }
}
