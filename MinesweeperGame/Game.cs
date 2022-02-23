using System;
using System.IO;
using MinesweeperGame.Output;
using static MinesweeperGame.Output.ConsoleOutput;

namespace MinesweeperGame
{
    public class Game
    {
        private Grid _grid;
        private readonly TextReader _textReader;
        private readonly TextWriter _textWriter;
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
            OutputMessages.Welcome().Typewrite();
            // Typewrite class to do this job?
            // Ambiguity on how this is implemented
            // We have an object that can output strings to a certain location
            OutputMessages.HowToPlay.Typewrite();
            OutputMessages.Instructions.Typewrite();
            OutputMessages.GridGenerationOptions.Typewrite();
            OutputMessages.GridSelection.Typewrite();
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
                OutputMessages.EnterNumberOfRows.DisplayMessage();
                var userInputRows = _textReader.ReadLine();
                _inputValidation.CheckGridDimensions(userInputRows);
                int.TryParse(userInputRows, out var rows);
                OutputMessages.EnterNumberOfCols.DisplayMessage();
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
            _grid.DisplayGrid();
            while (!_gameOver)
            {
                // Display grid
                OutputMessages.CellSelection.DisplayMessage();
                var userSelectedLocation = GetCellLocation();
                var selectedCell = GetSelectedCell(userSelectedLocation);
                OutputMessages.ListAllCellGuessActions().DisplayMessage();
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
                    
                _grid.DisplayGrid();
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
                OutputMessages.InvalidGuessLocation().DisplayMessage();
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
            _grid.DisplayGrid();
            if(result == "win")
                OutputMessages.GameOverYouWin.Typewrite();
            else
                OutputMessages.GameOverMineSelected.Typewrite();
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