using System;
using System.IO;
using System.Text;
using System.Threading;

namespace MinesweeperGame.Output
{
    public static class ConsoleOutput
    {

        public static void DisplayGrid(this Grid grid)
        {
            Console.Clear();
            Console.Write("   ");
            for (var i = 0; i < grid.Cols; i++)
            {
                Console.Write($"{i}  ");
            }
            Console.Write(Environment.NewLine);
            Console.Write("   ");
            for (var i = 0; i < grid.Cols; i++)
            {
                Console.Write($"_  ");
            }
            Console.Write(Environment.NewLine);
            for (var x = 0; x < grid.Rows; x++)
            {
                Console.Write($"{x}| ");
                for (var y = 0; y < grid.Cols; y++)
                {
                    var currentCell = grid.Cells[x, y];

                    if (!currentCell.IsRevealed && !currentCell.IsFlagged)
                    {
                        SetForegroundColour("hidden");
                        Console.Write(Constants.HiddenCell);
                    }
                    
                    else if (currentCell.IsFlagged)
                    {
                        SetForegroundColour("is flagged");
                        Console.Write(Constants.FlaggedCell);
                    }

                    else if (currentCell.IsRevealed && currentCell.CellType == CellType.NotAMine)
                    {
                        SetForegroundColour("revealed not a mine");
                        Console.Write(currentCell.NeighbouringMines + Constants.RevealedCell);
                    }

                    else if (currentCell.IsRevealed && currentCell.CellType == CellType.Mine)
                    {
                        SetForegroundColour("revealed is a mine");
                        Console.Write(Constants.MineCell);
                    }
                }
                Console.Write(Environment.NewLine);
                SetForegroundColour("default");
            }
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


        public static void DisplayMessage(this string message)
        {
            Console.WriteLine(message);
        }
        
        public static void Typewrite(this string message)
        {
            foreach (var t in message)
            {
                Console.Write(t);
                Thread.Sleep(15);
            }
        }
    }
}