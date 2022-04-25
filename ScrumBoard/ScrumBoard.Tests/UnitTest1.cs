using Xunit;
using System;

namespace ScrumBoard.Tests;

public class UnitTestForBoard
{
    [Fact]
    public void CreateBoard()
    {
        var board = new Board("Test Name");
        Assert.Equal("Test Name", board.Title);
    }
    [Fact]
    public void GetColumns_NoColumns_EmptyList()
    {
        var board = new Board("Test Name");

        Assert.Empty(board.GetAllColums());
    }

    [Fact]
    public void GetColumn_NoColumns_ThrowsException()
    {
        var board = new Board("Test");

        Assert.Throws<ArgumentOutOfRangeException>(() => board.GetColumn(0));
    }

    [Fact]
    public void AddNewColumn_OnEmptyBoard_ColumnsCreatedSuccessfully()
    {
        var board = new Board("Test");

        board.AddNewColumn("1");

        Assert.NotEmpty(board.GetAllColums());
        Assert.Equal("1", board.GetColumn(0).Title);

        board.AddNewColumn("2");

        Assert.Equal("1", board.GetColumn(0).Title);
        Assert.Equal("2", board.GetColumn(1).Title);

        board.AddNewColumn("3");

        Assert.Equal("1", board.GetColumn(0).Title);
        Assert.Equal("2", board.GetColumn(1).Title);
        Assert.Equal("3", board.GetColumn(2).Title);
    }

    [Fact]
    public void GetColumn_WrongIndex_ThrowsException()
    {
        var board = new Board("Test");

        board.AddNewColumn("1");
        board.AddNewColumn("2");
        board.AddNewColumn("3");

        Assert.NotNull(board.GetColumn(0));
        Assert.NotNull(board.GetColumn(1));
        Assert.NotNull(board.GetColumn(2));

        Assert.Throws<ArgumentOutOfRangeException>(() => board.GetColumn(5));
    }

    [Fact]
    public void AddNewColumn_ExceedColumnsLimit_ThrowsException()
    {
        var board = new Board("Test");

        board.AddNewColumn("1");
        board.AddNewColumn("2");
        board.AddNewColumn("3");
        board.AddNewColumn("4");
        board.AddNewColumn("5");
        board.AddNewColumn("6");
        board.AddNewColumn("7");
        board.AddNewColumn("8");
        board.AddNewColumn("9");
        board.AddNewColumn("10");

        Assert.Throws<Exception>(() => board.AddNewColumn("11"));
    }

    [Fact]
    public void DeleteColumn_NameR2_ColumnsDeletedSuccessfully()
    {
        var board = new Board("Test");

        board.AddNewColumn("1");
        board.AddNewColumn("2");
        board.AddNewColumn("3");
        board.DeleteColumn(0);
        Assert.Null(board.GetColumn(0));
    }
    [Fact]
    public void DeleteColumn_NotEmptyInBoard_ThrowsException()
    {
        var board = new Board("Test");

        board.AddNewColumn("1");
        board.AddNewColumn("2");
        board.AddNewColumn("3");

        Assert.Throws<Exception>(() => board.DeleteColumn(4));
    }
    [Fact]
    public void RenameColumn_Name1_NameR2()
    {
        var board = new Board("Test");

        board.AddNewColumn("1");

        board.GetColumn(0).Title = "R2";

        Assert.Equal("R2", board.GetColumn(0).Title);
    }

    [Fact]
    public void AddNewCard_NoColumns_ThrowsException()
    {
        var board = new Board("Test");

        Assert.Throws<Exception>(() => board.AddNewCard("New Card"));
    }

    [Fact]
    public void AddNewCard_HasOneColumn_CardCreated()
    {
        var board = new Board("Test");

        board.AddNewColumn("To do");

        board.AddNewCard("task 1");

        Assert.Equal("task 1", board.GetColumn(0).GetTask(0).Title);
    }
    [Fact]
    public void FindColumnByTitle_NotEmpty_ColumnsFindSuccessfully()
    {
        var board = new Board("Test");

        board.AddNewColumn("1");
        board.AddNewColumn("2");
        board.AddNewColumn("3");
        Assert.NotNull(board.FindColumnByTitle("1"));

    }
    [Fact]
    public void FindColumnByTitle_Empty_ThrowsException()
    {
        var board = new Board("Test");

        board.AddNewColumn("1");
        board.AddNewColumn("2");
        board.AddNewColumn("3");

        Assert.Throws<Exception>(() => board.FindColumnByTitle("5"));
    }
    [Fact]
    public void GetAllTasksFromColum_HasOneColumn_GetAllTaskSuccessfully()
    {
        var board = new Board("Test");

        board.AddNewColumn("To do");

        board.AddNewCard("task 1");
        board.AddNewCard("task 2");
        board.AddNewCard("task 3");

        Assert.NotNull(board.GetColumn(0).GetAllTasksFromColum());
        Assert.Throws<Exception>(() => board.GetColumn(1).GetAllTasksFromColum());
    }

    [Fact]

    public void RemoveTask_NotEmpty_RemoveTaskSuccessfully()
    {
        var board = new Board("Test");

        board.AddNewColumn("To do");

        board.AddNewCard("task 1");
        board.AddNewCard("task 2");
        board.AddNewCard("task 3");

        Assert.NotNull(board.GetColumn(0).FindTaskByTitle("task 2"));
        board.GetColumn(0).RemoveTask("task 2");
        Assert.Null(board.GetColumn(0).FindTaskByTitle("task 2"));
    }
}
