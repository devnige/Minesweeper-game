using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MinesweeperGame;
using Xunit;
using FluentAssertions;

namespace MinesweeperGameTests
{
    public class GridTests
    {
        [Fact]
        public void GivenRowsAndColsAndStringArrayInput_WhenGridObjectIsInstantiated_ThenObjectPropertiesAreCorrect()
        {
            const int Rows = 3;
            const int Cols = 3;
            Grid grid = new Grid(Rows, Cols,
                new string[,]
                {
                    {".", ".", "."},
                    {".", ".", "."},
                    {".", ".", "."}
                });
            grid.InitialiseCells();
            var actual = grid.Cells;
            Cell[,] expected = new Cell[3, 3]
            {
                {
                    new(new Location(0, 0), 0, CellType.NotAMine),
                    new(new Location(0, 1), 0, CellType.NotAMine),
                    new(new Location(0, 2), 0, CellType.NotAMine)
                },
                {
                    new(new Location(1, 0), 0, CellType.NotAMine),
                    new(new Location(1, 1), 0, CellType.NotAMine),
                    new(new Location(1, 2), 0, CellType.NotAMine)
                },
                {
                    new(new Location(2, 0), 0, CellType.NotAMine),
                    new(new Location(2, 1), 0, CellType.NotAMine),
                    new(new Location(2, 2), 0, CellType.NotAMine)
                }
            };

            var isEqual = actual.Cast<Cell>().SequenceEqual(expected.Cast<Cell>());
            Assert.True(isEqual);
            Assert.True(grid.NumberOfMines == 0);
        }

        [Fact]
        public void
            GivenRowsAndColsAndStringArrayInputContainingMines_WhenGridObjectIsInstantiated_ThenObjectPropertiesAreCorrect()
        {
            const int Rows = 1;
            const int Cols = 1;
            Grid grid = new Grid(Rows, Cols,
                new string[,]
                {
                    {"*"}
                });
            grid.InitialiseCells();
            var actual = grid.Cells;
            Cell[,] expected = new Cell[1, 1]
            {
                {
                    new(new Location(0, 0), 9, CellType.Mine),
                }
            };

            var isEqual = actual.Cast<Cell>().SequenceEqual(expected.Cast<Cell>());
            Assert.True(isEqual);
        }
        
        [Fact]
        public void GivenRowsAndColsAndStringArrayInputWithMines_WhenGridObjectIsInstantiated_ThenObjectPropertiesAreCorrect()
        {
            const int Rows = 2;
            const int Cols = 3;
            Grid grid = new Grid(Rows, Cols,
                new string[,]
                {
                    {".", "*", "."},
                    {".", ".", "."},
                });
            grid.InitialiseCells();
            var actual = grid.Cells;
            Cell[,] expected = new Cell[2, 3]
            {
                {
                    new(new Location(0, 0), 0, CellType.NotAMine),
                    new(new Location(0, 1), 9, CellType.Mine),
                    new(new Location(0, 2), 0, CellType.NotAMine)
                },
                {
                    new(new Location(1, 0), 0, CellType.NotAMine),
                    new(new Location(1, 1), 0, CellType.NotAMine),
                    new(new Location(1, 2), 0, CellType.NotAMine)
                }
            };

            var isEqual = actual.Cast<Cell>().SequenceEqual(expected.Cast<Cell>());
            Assert.True(isEqual);
        }

        [Fact]
        public void Given_ASelectedCell_WhenRevealCellIsCalled_ThenTheCellIsReturned()
        {
            // player choses location (0,0)
            Grid grid = new Grid(2, 3,
                new string[,]
                {
                    {".", "*", "."},
                    {".", ".", "."}
                });
            grid.InitialiseCells();
            var actual = grid.RevealCell(new Location(0,0));
            var expected = new Cell(new Location(0,0), 0, CellType.NotAMine); // new cell is instantiated with isRevealed = false;
            expected.IsRevealed = true;
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_ASelectedCell_WhenRevealCellIsCalledOnACellThatHasNotBeenRevealed_ThenTheRevealedCellCounterIsIncrementedBy1()
        {
            // player choses location (0,0)
            Grid grid = new Grid(2, 3,
                new string[,]
                {
                    {".", "*", "."},
                    {".", ".", "."}
                });
            grid.InitialiseCells();
            grid.RevealCell(new Location(0,0));
            var actual = grid.NumberOfRevealedCells;
            var expected = 1;
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_ASelectedCell_WhenRevealCellIsCalledOnACellThatHasAlreadyBeenRevealed_ThenTheRevealedCellCounterDoesNotIncrement()
        {
            // player choses location (0,0)
            Grid grid = new Grid(2, 3,
                new string[,]
                {
                    {".", "*", "."},
                    {".", ".", "."}
                });
            grid.InitialiseCells();
            grid.RevealCell(new Location(0,0)); 
            grid.RevealCell(new Location(0,0));
            var actual = grid.NumberOfRevealedCells;
            var expected = 1;
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_ASelectedCell_WhenIncrementCellValueIsCalled_ThenTheCellValueIncrementsBy1()
        {
            // player chooses location (1,1)
            Grid grid = new Grid(3, 3,
                new string[,]
                {
                    {".", "*", "."},
                    {".", ".", "."},
                    {".", "*", "."}
                });
            grid.InitialiseCells();
            grid.IncrementCellValue(new Location(1, 1));
            var actual = grid.Cells[1,1].NeighbouringMines;
            var expected = 1;
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void
            Given_IncrementNeighbourMinesIfTouchingMines_WhenTouchingAMine_ThenTheNeighbouringMineValueIncrementsBy1()
        {
            Grid grid = new Grid(2, 2,
                new string[,]
                {
                    {".", "*"},
                    {".", "."}
                });
            var mines = grid.InitialiseCells();
            grid.IncrementNeighbourCellsIfTouchingAMine(mines);
            var actual00 = grid.Cells[0, 0].NeighbouringMines;
            var expected = 1;
            Assert.Equal(expected, actual00);
        }
        
        [Fact]
        public void
            Given_IncrementNeighbourMinesIfTouchingMines_WhenTouchingTwoMines_ThenTheNeighbouringMineValueIncrementsBy2()
        {
            Grid grid = new Grid(2, 2,
                new string[,]
                {
                    {".", "*"},
                    {".", "*"}
                });
            var mines = grid.InitialiseCells();
            grid.IncrementNeighbourCellsIfTouchingAMine(mines);
            var actual00 = grid.Cells[0, 0].NeighbouringMines;
            var expected = 2;
            Assert.Equal(expected, actual00);
        }
        
        [Fact]
        public void Given_GetCountOfMines_WhenCalled_ThenTheNumberOfMinesShouldIncrementedByTheCorrectAmount()
        {
            // player choses location (0,0)
            Grid grid = new Grid(2, 3,
                new string[,]
                {
                    {".", "*", "."},
                    {".", "*", "."}
                });
            grid.InitialiseCells();
            var actual = grid.NumberOfMines;
            var expected = 2;
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_AddValidNeighbours_WhenCalled_ReturnsAListOfNeighboursInTheGrid
            ()
        {
            Grid grid = new Grid(2, 3,
                new string[,]
                {
                    {".", "*", "."},
                    {".", "*", "."}
                });
            var mines = grid.InitialiseCells();
            grid.IncrementNeighbourCellsIfTouchingAMine(mines);
            var actual = grid.AddValidNeighboursToList(new Cell(new Location(0,1
            ), 9, CellType.Mine));
            var expected = new List<Cell>{new Cell(new Location(0,0), 2, CellType.NotAMine),
                new Cell(new Location(0,2), 2, CellType.NotAMine),
                new Cell(new Location(1,0), 2, CellType.NotAMine),
                new Cell(new Location(1,2), 2, CellType.NotAMine),
                new Cell(new Location(1,1), 9, CellType.Mine)
            };
            expected.Should().BeEquivalentTo(actual);
        }
    }
}