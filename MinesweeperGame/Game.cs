using System;
using System.IO;
using System.Threading;
using MinesweeperGame.Output;
using static MinesweeperGame.Grid;

namespace MinesweeperGame
{
    public class Game
    {
        private Grid _grid;
        private readonly TextReader _textReader;
        private static TextWriter _textWriter;
        // How would you write to a stream as well as console?
        private bool _gameOver;
        private InputValidation _inputValidation;
        
        public Game(Grid grid, TextReader textReader, TextWriter textWriter)
        {
            _grid = grid;
            _textReader = textReader;
            _textWriter = textWriter;
            _inputValidation = new InputValidation(textReader);
            // instantiate Typewriter here
        }

        public void Run()
        {
            Typewrite(OutputMessages.Welcome());
            Typewrite(OutputMessages.HowToPlay);
            Typewrite(OutputMessages.Instructions);
            Typewrite(OutputMessages.GridGenerationOptions);
            Typewrite(OutputMessages.GridSelection);
            var userGridSelection = _textReader.ReadLine();
            // move the user 
            if (userGridSelection == "1")
            {
                _grid = new Grid(3, 3, new string[,]
                    {
                        {".", "*", "."},
                        {".", ".", "."},
                        {".", ".", "."}
                    }
                );
                //
                // _grid = new Grid(9, 9, new string[,]
                //     {
                //         {".", "*", ".", ".", ".", ".", ".", ".", "."},
                //         {".", ".", ".", ".", "*", ".", ".", "*", "."},
                //         {".", ".", ".", "*", "*", ".", ".", "*", "."},
                //         {".", "*", ".", ".", ".", ".", ".", ".", "."},
                //         {".", ".", ".", "*", ".", "*", ".", ".", "."},
                //         {".", ".", ".", ".", ".", ".", ".", ".", "."},
                //         {"*", ".", "*", "*", ".", ".", ".", "*", "."},
                //         {".", ".", ".", "*", "*", ".", ".", "*", "."},
                //         {".", ".", ".", "*", "*", ".", ".", "*", "."}
                //     }
                // );
            }
            else if (userGridSelection == "2")
            {
                _textWriter.Write(OutputMessages.EnterNumberOfRows);
                // Could put a sleeo in here
                var userInputRows = _textReader.ReadLine();
                _inputValidation.CheckGridDimensions(userInputRows);
                int.TryParse(userInputRows, out var rows);
                _textWriter.Write(OutputMessages.EnterNumberOfCols);
                var userInputCols = _textReader.ReadLine();
                int.TryParse(userInputCols, out var cols);
                var random2DMineStringArray = new string[3, 3]
                {
                    {".", ".", "."},
                    {".", ".", "."},
                    {".", ".", "."}
                };
                _grid = new Grid(rows, cols, random2DMineStringArray);
            }
            
            
            _grid.InitialiseCells();
            _grid.IncrementNeighbourMinesIfTouchingMines();
            var numberOfMines = _grid.GetCountOfMines();
            // _textWriter.WriteLine(
            Console.Write(BuildGrid(_grid));
            while (!_gameOver)
            {
                // Display grid
                _textWriter.Write(OutputMessages.CellSelection);
                var userSelectedLocation = GetCellLocation();
                var selectedCell = GetSelectedCell(userSelectedLocation);
                _textWriter.Write(OutputMessages.ListAllCellGuessActions());
                var userActionResponse = _textReader.ReadLine();
                if (userActionResponse == "F" && !IsFlagged(userSelectedLocation))
                {
                    AddFlagToCell(selectedCell);
                }
                else if (userActionResponse == "D" && IsFlagged(userSelectedLocation))
                {
                    selectedCell.IsFlagged = false;
                }
                else if (userActionResponse == "R")
                {
                    selectedCell = RevealSelectedCell(userSelectedLocation);
                }
                    
                Console.WriteLine(BuildGrid(_grid));
                if (IsWin(numberOfMines))
                {
                    EndGame("win");
                    ExitGame();
                }
                else if (IsLoss(selectedCell))
                {
                    EndGame("loss");
                    ExitGame();
                }
                // Cases
                // The cell selected is covered and a mine is in the cell. Reveal the bomb. Game Over :(
                // The cell selected is covered and a number is in the cell. Reveal the number.
                // The cell selected is already uncovered - do nothing.
                // When a player selects a cell with 0 mine neighbours it automatically uncovers all adjacent empty cells and their neighbours
            }
        }

        public static void Typewrite(string message)
        {
            foreach (var t in message)
            {
                _textWriter.Write(t);
                Thread.Sleep(15);
            }
        }
        
        public void AddFlagToCell(Cell selectedCell)
        {
            selectedCell.IsFlagged = true;
        }

        public Cell GetSelectedCell(Location location)
        {
            return _grid.Cells[location.Row, location.Col];
        }

        public bool IsFlagged(Location location)
        {
            return _grid.Cells[location.Row, location.Col].IsFlagged;
        }

        public Cell RevealSelectedCell(Location location)
        {
            return _grid.RevealCell(location);
        }

        public Location GetCellLocation()
        {
            var userSelectedCellCoords = _textReader.ReadLine();
            while (string.IsNullOrEmpty(userSelectedCellCoords) ||
                   !_inputValidation.IsUserInputValid(userSelectedCellCoords, _grid.Rows, _grid.Cols))
            {
                _textWriter.Write(OutputMessages.InvalidGuessLocation());
                userSelectedCellCoords = _textReader.ReadLine();
            }
            
            var userSelectedRow = userSelectedCellCoords.Split(',')[0];
            var userSelectCol = userSelectedCellCoords.Split(',')[1];
            int.TryParse(userSelectedRow, out var row);
            int.TryParse(userSelectCol, out var col);
            return new Location(row, col); // passes but we don't want this
        }

        private bool IsWin(int numberOfMines) =>
            _grid.NumberOfRevealedCells + numberOfMines == _grid.Cols * _grid.Rows;

        public bool IsLoss(Cell selectedCell) => selectedCell.CellType == CellType.Mine && selectedCell.IsRevealed;
        

        private void EndGame(string result)
        {
            _gameOver = true;
            SetAllCellsToRevealed();
           BuildGrid( _grid);
            if(result == "win")
                Typewrite(OutputMessages.GameOverYouWin);
            else
                Typewrite(OutputMessages.GameOverMineSelected);
        }

        private void ExitGame() => Environment.Exit(0);

        private void SetAllCellsToRevealed()
        {
            foreach (var c in _grid.Cells)
            {
                c.IsRevealed = true;
            }
        }
    }
}