using System;
using System.Collections.Generic;
using FluentAssertions;
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
            // var grid = new Grid(1, 1, new string[,] {{"."}});
            var testReader = new FakeInput();
            var testOutput = new FakeConsoleOut();
            var game = new Game(testReader, testOutput);
            var userSelectedLocation = new List<string> {"1", "0,0", "R"};
            testReader.SetupSequence(userSelectedLocation);
            game.Run();
            var expected = new Location(0, 0);
            var actual = game.GetCellLocation("0,0");
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_ACellLocation_When_SelectedCellIsCoveredAndIsNotAMine_Then_RevealTheValueOfTheCell()
        {
            var testReader = new FakeInput();
            var testWriter = new FakeConsoleOut();
            var game = new Game(testReader, testWriter);
            var userSelectedLocation = new List<string> {"2", "3", "3", "0,0", "R"};
            testReader.SetupSequence(userSelectedLocation);
            game.Run();
            Assert.True(game.GetSelectedCell(new Location(0,0)).IsRevealed);
        }
        
        [Fact]
        public void Given_AGame_When_UserSelectsRandomGrid_Then_UserCanFlagACell()
        {
            var testReader = new FakeInput();
            var testWriter = new FakeConsoleOut();
            var game = new Game(testReader, testWriter);
            var userSelectedLocation = new List<string> {
                "1", "0,0", "F"};
            testReader.SetupSequence(userSelectedLocation);
            game.Run();
            //TODO assert that the game finished with a win or a loss
            
        }
        
        [Fact]
        public void Given_ACellLocation_When_SelectedACoveredMineCellIsSelected_Then_IsLossIsTrue()
        {
            var grid = new Grid(1, 1, new string[,] {{"*"}});
            grid.InitialiseCells();
            var testReader = new FakeInput();
            var testWriter = new FakeConsoleOut();
            var game = new Game(testReader, testWriter);
            var userSelectedLocation = new List<string> {"0,0"};
            testReader.SetupSequence(userSelectedLocation);
            var selectedCellLocation = game.GetCellLocation(TODO);
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
            var game = new Game(testReader, testWriter);
            var userSelectedLocation = new List<string> {"0,0"};
            testReader.SetupSequence(userSelectedLocation);
            var selectedCellLocation = game.GetCellLocation(TODO);
            var selectedCell = game.GetSelectedCell(selectedCellLocation);
            game.AddFlagToCell(selectedCell);
            Assert.True(selectedCell.IsFlagged);
        }
    }
}