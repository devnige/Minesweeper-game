using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperGame
{
    public class Grid
    {
        public Grid(int rows, int cols, string[,] input) // current minesweeper game creates this already.
        {
            Rows = rows;
            Cols = cols;
            Cells = new Cell[Rows, Cols];
            InitialStringArray = input;
            NumberOfCellsInGrid = rows * cols;
        }

        private string[,] InitialStringArray { get;}

        public int Rows { get; set; }
        public int Cols { get; set; }
        public int NumberOfCellsInGrid { get; }
        public int NumberOfMines { get; set; }
        public int NumberOfRevealedCells { get; set; }
        
        public readonly Cell[,] Cells;

        public List<Cell> InitialiseCells()
        {
            var mines = new List<Cell>();
            for (var x = 0; x < Rows; x++)
            for (var y = 0; y < Cols; y++)
            {
                if (InitialStringArray[x, y] == "*")
                {
                    Cells[x, y] = new Cell(new Location(x, y), 9, CellType.Mine);
                    mines.Add(Cells[x,y]);
                }
                else
                    Cells[x, y] = new Cell(new Location(x, y), 0, CellType.NotAMine);
            }
            return mines;
        }
        
        public void IncrementNeighbourCellsIfTouchingAMine(List<Cell> mines)
        {
            foreach (var mine in mines)
            {
                var neighbouringCells = AddValidNeighboursToList(mine);
                foreach (var neighbour in neighbouringCells)
                {
                    if (neighbour.CellType == CellType.NotAMine)
                        IncrementCellNeighbouringMines(neighbour);
                }
            }
        }

        public List<Cell> AddValidNeighboursToList(Cell currentCell)
            {
                var upperBoundRowLimit = Cells.GetUpperBound(0);
                var lowerBoundRowLimit = Cells.GetLowerBound(0);
                var upperBoundColLimit = Cells.GetUpperBound(1);
                var lowerBoundColLimit = Cells.GetLowerBound(1);
            
                var neighbouringCells = new List<Cell>();
                // add neighbouring cells to list if in bounds
                var isCellPreviousColSameRowInBounds = currentCell.Location.Col - 1 >= lowerBoundColLimit;
                if (isCellPreviousColSameRowInBounds)
                    neighbouringCells.Add(Cells[currentCell.Location.Row, currentCell.Location.Col - 1]);
            
                var isCellPreviousColPreviousRowInBounds = currentCell.Location.Col - 1 >= lowerBoundColLimit &&
                                                           currentCell.Location.Row - 1 >= lowerBoundRowLimit;
                if(isCellPreviousColPreviousRowInBounds)
                    neighbouringCells.Add(Cells[currentCell.Location.Row - 1, currentCell.Location.Col - 1]);

                var isCellPreviousColNextRowInBounds = currentCell.Location.Col - 1 >= lowerBoundColLimit &&
                                                       currentCell.Location.Row + 1 <= upperBoundRowLimit;
                if(isCellPreviousColNextRowInBounds)
                    neighbouringCells.Add(Cells[currentCell.Location.Row + 1, currentCell.Location.Col - 1]);

                var isCellNextColSameRowInBounds = currentCell.Location.Col + 1 <= upperBoundColLimit;
                if(isCellNextColSameRowInBounds)
                    neighbouringCells.Add(Cells[currentCell.Location.Row, currentCell.Location.Col + 1]);
            
                var isCellNextColPreviousRowInBounds = currentCell.Location.Col + 1 <= upperBoundColLimit &&
                                                       currentCell.Location.Row - 1 >= lowerBoundRowLimit;
                if (isCellNextColPreviousRowInBounds)
                {
                    neighbouringCells.Add(Cells[currentCell.Location.Row - 1, currentCell.Location.Col + 1]); // previous row, next col
                    neighbouringCells.Add(Cells[currentCell.Location.Row - 1, currentCell.Location.Col]); // previous row same col
                }
            
                var isCellNextColNextRowInBounds = currentCell.Location.Col + 1 <= upperBoundColLimit &&
                                                   currentCell.Location.Row + 1 <= upperBoundRowLimit;
                if (isCellNextColNextRowInBounds)
                {
                    neighbouringCells.Add(Cells[currentCell.Location.Row + 1, currentCell.Location.Col + 1]); // next row, next col
                    neighbouringCells.Add(Cells[currentCell.Location.Row + 1, currentCell.Location.Col]); // next row, same col
                }
                
            
                var isCellSameColPreviousRowInBounds = currentCell.Location.Col <= upperBoundColLimit &&
                                                       currentCell.Location.Row - 1 >= lowerBoundRowLimit;
                if (isCellSameColPreviousRowInBounds)
                {
                
                }
                return neighbouringCells;
            }

            private static bool IsCellAMine(Cell currentCell)
            {
                return currentCell.CellType == CellType.Mine;
            }
        
            public int GetCountOfMines()
            {
                for (var x = 0; x < Rows; x++)
                {
                    for (var y = 0; y < Cols; y++)
                    {
                        var currentCell = Cells[x, y];
        
                        if (currentCell.CellType == CellType.Mine)
                        {
                            NumberOfMines += 1;
                        }
                    }
                }
                return NumberOfMines;
            }
        
            private void IncrementCellNeighbouringMines(Cell cell) => cell.NeighbouringMines += 1;
        
            public Cell RevealCell(Location location)
            {
                var selectedCell = Cells[location.Row, location.Col];
                if (selectedCell.IsRevealed) return Cells[location.Row, location.Col];
                selectedCell.IsRevealed = true;
                IncrementCountOfRevealedCells();
                return Cells[location.Row, location.Col];
            }

            private int IncrementCountOfRevealedCells() => NumberOfRevealedCells += 1;
        
            public int IncrementCellValue(Location location) => Cells[location.Row, location.Col].NeighbouringMines += 1;
        
            public static string BuildGrid(Grid grid)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("   ");
                for (var i = 0; i < grid.Cols; i++)
                {
                    sb.Append($"{i}  ");
                }
                sb.Append(Environment.NewLine);
                sb.Append("   ");
                for (var i = 0; i < grid.Cols; i++)
                {
                    sb.Append($"_  ");
                }
                sb.Append(Environment.NewLine);
                for (var x = 0; x < grid.Rows; x++)
                {
                    sb.Append($"{x}| ");
                    for (var y = 0; y < grid.Cols; y++)
                    {
                        var currentCell = grid.Cells[x, y];

                        if (!currentCell.IsRevealed && !currentCell.IsFlagged)
                        {
                            SetForegroundColour("hidden");
                            sb.Append(Constants.HiddenCell);
                        }
                    
                        else if (currentCell.IsFlagged)
                        {
                            SetForegroundColour("is flagged");
                            sb.Append(Constants.FlaggedCell);
                        }

                        else if (currentCell.IsRevealed && currentCell.CellType == CellType.NotAMine)
                        {
                            SetForegroundColour("revealed not a mine");
                            sb.Append(currentCell.NeighbouringMines + Constants.RevealedCell);
                        }

                        else if (currentCell.IsRevealed && currentCell.CellType == CellType.Mine)
                        {
                            SetForegroundColour("revealed is a mine");
                            sb.Append(Constants.MineCell);
                        }
                    }
                    sb.Append(Environment.NewLine);
                    SetForegroundColour("default");
                }
                return sb.ToString(); //string needed
            }
        
            private static void SetForegroundColour(string type)
            {
                switch (type)
                {
                    case "hidden":
                        Console.ForegroundColor = Constants.HiddenCellColour;
                        break;
                    case "revealed not a mine":
                        Console.ForegroundColor = Constants.RevealedCellNotAMine;
                        break;
                    case "revealed is a mine":
                        Console.ForegroundColor = Constants.RevealedCellMine;
                        break;
                    case "is flagged":
                        Console.ForegroundColor = Constants.FlaggedCellColour;
                        break;
                    case "default":
                        Console.ForegroundColor = Constants.DefaultTextColour;
                        break;
                }
            }
    }
}