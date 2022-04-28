using System.Collections.Generic;

namespace MinesweeperGame
{
    public class Grid
    {
        public readonly Cell[,] Cells;

        public Grid(int rows, int cols, string[,] input) // current minesweeper game creates this already.
        {
            Rows = rows;
            Cols = cols;
            Cells = new Cell[Rows, Cols];
            InitialStringArray = input;
            NumberOfCellsInGrid = rows * cols;
        }

        private string[,] InitialStringArray { get; }

        public int Rows { get; set; }
        public int Cols { get; set; }
        public int NumberOfCellsInGrid { get; }
        public int NumberOfMines { get; private set; }
        public int NumberOfRevealedCells { get; private set; }

        public List<Cell> InitialiseCells()
        {
            var mines = new List<Cell>();
            for (var x = 0; x < Rows; x++)
            for (var y = 0; y < Cols; y++)
                if (InitialStringArray[x, y] == "*")
                {
                    Cells[x, y] = new Cell(new Location(x, y), 9, CellType.Mine);
                    mines.Add(Cells[x, y]);
                    NumberOfMines++;
                }
                else
                {
                    Cells[x, y] = new Cell(new Location(x, y), 0, CellType.NotAMine);
                }
            return mines;
        }

        public void IncrementNeighbourCellsIfTouchingAMine(List<Cell> mines)
        {
            foreach (var mine in mines)
            {
                var neighbouringCells = GetNeighbouringCells(mine);
                IncrementTheValueOfAllNonMineNeighbouringCells(neighbouringCells);
            }
        }

        private void IncrementTheValueOfAllNonMineNeighbouringCells(List<Cell> neighbouringCells)
        {
            foreach (var neighbour in neighbouringCells) IncrementNeighbourCellIfNotAMine(neighbour);
        }

        private static void IncrementNeighbourCellIfNotAMine(Cell cell)
        {
            cell.NeighbouringMines = cell.CellType == CellType.NotAMine
                ? cell.NeighbouringMines + 1
                : cell.NeighbouringMines;
        }

        private List<Cell> GetNeighbouringCells(Cell currentCell)
        {
            var upperBoundRowLimit = Cells.GetUpperBound(0);
            var lowerBoundRowLimit = Cells.GetLowerBound(0);
            var upperBoundColLimit = Cells.GetUpperBound(1);
            var lowerBoundColLimit = Cells.GetLowerBound(1);

            var currentRow = currentCell.Location.Row;
            var currentCol = currentCell.Location.Col;
            var previousRow = currentRow - 1;
            var nextRow = currentRow + 1;
            var previousCol = currentCol - 1;
            var nextCol = currentCol + 1;

            var rowsToCheck = new List<int>();
            var neighbouringCells = new List<Cell>();
            // add neighbouring cells to list if in bounds

            rowsToCheck.Add(currentRow);

            if (previousRow >= lowerBoundRowLimit)
                rowsToCheck.Add(previousRow);

            if (nextRow <= upperBoundRowLimit)
                rowsToCheck.Add(nextRow);

            foreach (var row in rowsToCheck)
            {
                if (previousCol >= lowerBoundColLimit)
                    neighbouringCells.Add(Cells[row, previousCol]);

                if (nextCol <= upperBoundColLimit)
                    neighbouringCells.Add(Cells[row, nextCol]);

                if (row != currentRow)
                    neighbouringCells.Add(Cells[row, currentCol]);
            }

            return neighbouringCells;
        }

        public Cell RevealCell(Location location)
        {
            var selectedCell = Cells[location.Row, location.Col];
            if (selectedCell.IsRevealed) return Cells[location.Row, location.Col];
            selectedCell.IsRevealed = true;
            IncrementCountOfRevealedCells();
            return Cells[location.Row, location.Col];
        }

        private int IncrementCountOfRevealedCells()
        {
            return NumberOfRevealedCells += 1;
        }

        public int IncrementCellValue(Location location)
        {
            return Cells[location.Row, location.Col].NeighbouringMines += 1;
        }
        
        public Cell GetSelectedCell(Location location)
        {
            return Cells[location.Row, location.Col];
        }

        public void SetAllCellsToRevealed()
        {
            foreach (var c in Cells)
            {
                c.IsRevealed = true;
            }
        }

        public bool IsFlagged(Location location)
        {
            return Cells[location.Row, location.Col].IsFlagged;
        }

        public Cell RevealCellAtSelectedLocationAndNeighboursIfNotTouchingAMine(Location location)
        {
            var selectedCell = RevealCell(location);
            if(selectedCell.NeighbouringMines == 0) RevealNeighbouringCellsIfNotTouchingAMine(selectedCell);
            return selectedCell;
        }

        private void RevealNeighbouringCellsIfNotTouchingAMine(Cell selectedCell)
        {
            var neighbouringCells = GetNeighbouringCells(selectedCell);
            foreach (var neighbour in neighbouringCells)
                if (neighbour.NeighbouringMines == 0)
                    RevealCell(neighbour.Location);
        }

        public void AddFlagToCell(Cell selectedCell)
        {
            selectedCell.IsFlagged = true;
        }

        public static void RemoveFlagFromCell(Cell selectedCell)
        {
            selectedCell.IsFlagged = false;
        }

        public Location GetCellLocation(string cellLocation)
        {
            var userSelectedRow = cellLocation.Split(',')[0];
            var userSelectCol = cellLocation.Split(',')[1];
            int.TryParse(userSelectedRow, out var row);
            int.TryParse(userSelectCol, out var col);
            return new Location(row, col);
        }
    }
}