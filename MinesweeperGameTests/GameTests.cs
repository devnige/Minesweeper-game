using System;
using System.Collections.Generic;
using MinesweeperGame;
using MinesweeperGame.Output;
using Xunit;

namespace MinesweeperGameTests
{
    public class GameTests
    {
        //The cell selected is covered and a mine is in the cell. Reveal the bomb. Game Over :(
        [Fact]
        public void Given_RowCell_TurnsIntoAValidLocation()
        {
            var grid = new Grid(1, 1, new string[,] {{"."}});
            var testReader = new FakeInput();
            var testOutput = new FakeConsoleOut();
            var game = new Game(grid, testReader, testOutput);
            var userSelectedLocation = new List<string> {"0,0"};
            testReader.SetupSequence(userSelectedLocation);
            var expected = new Location(0, 0);
            var actual = game.GetCellLocation();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_ACellLocation_When_SelectedCellIsCoveredAndIsNotAMine_Then_RevealTheValueOfTheCell()
        {
            var grid = new Grid(1, 1, new string[,] {{"."}});
            grid.InitialiseCells();
            var testReader = new FakeInput();
            var testWriter = new FakeConsoleOut();
            var game = new Game(grid, testReader, testWriter);
            var userSelectedLocation = new List<string> {"0,0"};
            testReader.SetupSequence(userSelectedLocation);
            var location = new Location(0, 0);
            var selectedCell = game.RevealSelectedCell(location);
            Assert.True(selectedCell.CellType == CellType.NotAMine);
            Assert.True(selectedCell.IsRevealed);
        }
        
        [Fact]
        public void Given_ACellLocation_When_Selected_Then_CellIsRevealedIsTrue()
        {
            var grid = new Grid(3, 3, new string[,]
            {
                {"." ,".", "."},
                {".", ".", "."},
                {".", ".", "."}
            });
            grid.InitialiseCells();
            var testReader = new FakeInput();
            var testWriter = new FakeConsoleOut();
            var game = new Game(grid, testReader, testWriter);
            var userSelectedLocation = new List<string> {"0,1"};
            testReader.SetupSequence(userSelectedLocation);
            var selectedCellLocation = game.GetCellLocation();
            var selectedCell = game.GetSelectedCell(selectedCellLocation);
            game.RevealSelectedCell(selectedCellLocation);
            Assert.True(selectedCell.IsRevealed);
        }
        
        [Fact]
        public void Given_ACellLocation_When_SelectedACoveredMineCellIsSelected_Then_IsLossIsTrue()
        {
            var grid = new Grid(1, 1, new string[,] {{"*"}});
            grid.InitialiseCells();
            var testReader = new FakeInput();
            var testWriter = new FakeConsoleOut();
            var game = new Game(grid, testReader, testWriter);
            var userSelectedLocation = new List<string> {"0,0"};
            testReader.SetupSequence(userSelectedLocation);
            var selectedCellLocation = game.GetCellLocation();
            var selectedCell = game.GetSelectedCell(selectedCellLocation);
            game.RevealSelectedCell(selectedCellLocation);
            var actual = game.IsLoss(selectedCell);
            Assert.True(selectedCell.CellType == CellType.Mine);
            Assert.True(selectedCell.IsRevealed);
            Assert.True(actual);
        }

        [Fact] public void Given_ACellLocation_When_ACoveredMineCellIsFlagged_Then_IsFlaggedIsTrue()
        {
            var grid = new Grid(1, 1, new string[,] {{"*"}});
            grid.InitialiseCells();
            var testReader = new FakeInput();
            var testWriter = new FakeConsoleOut();
            var game = new Game(grid, testReader, testWriter);
            var userSelectedLocation = new List<string> {"0,0"};
            testReader.SetupSequence(userSelectedLocation);
            var selectedCellLocation = game.GetCellLocation();
            var selectedCell = game.GetSelectedCell(selectedCellLocation);
            game.AddFlagToCell(selectedCell);
            Assert.True(selectedCell.IsFlagged);
        }
    }
}