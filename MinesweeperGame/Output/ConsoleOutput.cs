using System;
using System.IO;
using System.Text;

namespace MinesweeperGame.Output
{
    public class ConsoleOutput
    {
        private static TextWriter _textWriter;

        public ConsoleOutput(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }
        public void PrintGrid(Grid grid)
        {
            Console.Clear();
            var gridArray = BuildGrid(grid).ToCharArray();
            foreach (char c in gridArray)
            {
                SetTextColourDependingOnCharacter(c);
            }
            _textWriter.WriteLine();
        }

        private static void SetTextColourDependingOnCharacter(char c)
        {
            if (c == '*')
            {
                Console.ForegroundColor = Constants.RevealedCellMine;
                _textWriter.Write(c);
            }
            else if (c == '\u25fc')
            {
                Console.ForegroundColor = Constants.HiddenCellColour;
                _textWriter.Write(c);
            }
            else if (c == '\u2690')
            {
                Console.ForegroundColor = Constants.FlaggedCellColour;
                _textWriter.Write(c);
            }
            else
            {
                Console.ForegroundColor = Constants.RevealedCellNotAMine;
                _textWriter.Write(c);
            }
        }

        private string BuildGrid(Grid grid)
        {
            var sb = new StringBuilder();
            sb.Append("   ");
            for (var i = 0; i < grid.Cols; i++) sb.Append($"{i}  ");
            sb.Append(Environment.NewLine);
            sb.Append("   ");
            for (var i = 0; i < grid.Cols; i++) sb.Append("_  ");
            sb.Append(Environment.NewLine);
            for (var x = 0; x < grid.Rows; x++)
            {
                sb.Append($"{x}| ");
                for (var y = 0; y < grid.Cols; y++)
                {
                    var currentCell = grid.Cells[x, y];
                    sb.Append(GetCellString(currentCell));
                }

                sb.Append(Environment.NewLine);
            }
            return sb.ToString(); //string needed
        }
        
        private string GetCellString (Cell currentCell)
        {
            if (!currentCell.IsRevealed && !currentCell.IsFlagged)
                return Constants.HiddenCell;

            if (currentCell.IsFlagged) return Constants.FlaggedCell;

            return currentCell.IsRevealed switch
            {
                true when currentCell.CellType == CellType.NotAMine => 
                    currentCell.NeighbouringMines + Constants.RevealedCell,
                true when currentCell.CellType == CellType.Mine => Constants.MineCell,
                _ => ""
            };
        }
    }
}