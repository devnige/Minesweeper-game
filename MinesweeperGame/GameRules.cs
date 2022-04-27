namespace MinesweeperGame
{
    public static class GameRules
    {
        public static string CheckForWinOrLoss(Grid grid, Cell selectedCell)
        {
            if (IsWin(grid))
            {
                return "win";
            }

            if (IsLoss(selectedCell))
            {
                return "loss";
            }
            return null;
        }
        
        static bool IsWin(Grid grid) =>
            grid.NumberOfRevealedCells + grid.NumberOfMines == grid.NumberOfCellsInGrid;

        static bool IsLoss(Cell selectedCell) => selectedCell.CellType == CellType.Mine && selectedCell.IsRevealed;
    }
}