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
        private readonly InputValidation _inputValidation;
        private int _rows;
        private int _cols;

        public Game(TextReader textReader, TextWriter textWriter)
        {
            _textReader = textReader;
            _textWriter = textWriter;
            _inputValidation = new InputValidation(textReader, textWriter);
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Typewrite(OutputMessages.Welcome());
            Console.ResetColor();
            Typewrite(OutputMessages.Instructions);
            Typewrite(OutputMessages.GridGenerationOptions);
            Typewrite(OutputMessages.GridSelection);
            var userGridSelection = _textReader.ReadLine();
            var validGridSelection = _inputValidation.UserGridGenerationInputValid(userGridSelection);
            if (validGridSelection == "1")
            {
                Random rnd = new Random();
                _rows = rnd.Next(3, 11);
                _cols = rnd.Next(3, 11);
            }
            else if (validGridSelection == "2")
            {
                _textWriter.Write(OutputMessages.EnterNumberOfRows);
                var userInputRows = _textReader.ReadLine();
                _rows = _inputValidation.CheckGridDimensions(userInputRows);
                _textWriter.Write(OutputMessages.EnterNumberOfCols);
                var userInputCols = _textReader.ReadLine();
               _cols = _inputValidation.CheckGridDimensions(userInputCols);
            }

            _grid = Generate2DStringGrid(_rows, _cols);
            InitialiseGrid();
            PrintGrid();

// Cases
// The cell selected is covered and a mine is in the cell. Reveal the bomb. Game Over :(
// The cell selected is covered and a number is in the cell. Reveal the number.
// The cell selected is already uncovered - do nothing.
// When a player selects a cell with 0 mine neighbours it automatically uncovers all adjacent empty cells and their neighbours

            while (!_gameOver)
            {
                var message = OutputMessages.CellSelection;
                _textWriter.Write(message);
                var userSelectedLocation = _textReader.ReadLine();
                var userSelectedCellLocation = GetLocation(userSelectedLocation);
                var selectedCell = GetSelectedCell(userSelectedCellLocation);
                if (!selectedCell.IsRevealed)
                {
                    GetUserInputAndAdjustCellVisibility(selectedCell, userSelectedCellLocation);
                }
                PrintGrid();
                if (IsWin())
                {
                    EndGame("win");
                }
                else if (IsLoss(selectedCell))
                {
                    EndGame("loss");
                }
            }
        }

        private Cell GetUserInputAndAdjustCellVisibility(Cell selectedCell, Location userSelectedLocation)
        {
            string message = selectedCell.IsFlagged
                ? OutputMessages.ListFlaggedCellGuessActions()
                : OutputMessages.ListUnflaggedCellGuessActions();
            _textWriter.Write(message);
            var userActionResponse = _textReader.ReadLine();
            var validUserAction = _inputValidation.GetValidUserAction(userActionResponse);
            if (validUserAction == "F" && !IsFlagged(userSelectedLocation))
            {
                AddFlagToCell(selectedCell);
            }
            else if (validUserAction == "D" && IsFlagged(userSelectedLocation))
            {
                RemoveFlagFromCell(selectedCell);
            }
            else if (validUserAction == "R")
            {
                RemoveFlagFromCell(selectedCell);
                selectedCell = RevealCellAtSelectedLocationAndNeighboursIfNotTouchingAMine(userSelectedLocation);
            }
            return selectedCell;
        }

        private static void RemoveFlagFromCell(Cell selectedCell)
        {
            selectedCell.IsFlagged = false;
        }

        private static void Typewrite(string message)
        {
            foreach (var t in message)
            {
                _textWriter.Write(t);
                Thread.Sleep(15);
            }
        }
        private static Grid Generate2DStringGrid(int rows, int cols)
        {
            var randomMineGenerator = new RandomMineGenerator(rows, cols);
            var random2DMineStringArray = randomMineGenerator.GenerateRandomMinesAndNonMines();
            return new Grid(rows, cols, random2DMineStringArray);
        }
        void PrintGrid()
        {
            Console.Clear();
            var gridArray = BuildGrid(_grid).ToCharArray();
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


        void InitialiseGrid()
        {
            var mines = _grid.InitialiseCells();
            _grid.IncrementNeighbourCellsIfTouchingAMine(mines);
        }

        void AddFlagToCell(Cell selectedCell)
        {
            selectedCell.IsFlagged = true;
        }

        public Cell GetSelectedCell(Location location)
        {
            return _grid.Cells[location.Row, location.Col];
        }

        private bool IsFlagged(Location location)
        {
            return _grid.Cells[location.Row, location.Col].IsFlagged;
        }

        Cell RevealCellAtSelectedLocationAndNeighboursIfNotTouchingAMine(Location location)
        {
            var selectedCell = _grid.RevealCell(location);
            if(selectedCell.NeighbouringMines == 0)
                RevealNeighbouringCellsIfNotTouchingAMine(selectedCell);
            return selectedCell;
        }

        private void RevealNeighbouringCellsIfNotTouchingAMine(Cell selectedCell)
        {
            var neighbouringCells = _grid.AddValidNeighboursToList(selectedCell);
            foreach (var neighbour in neighbouringCells)
                if (neighbour.NeighbouringMines == 0)
                    _grid.RevealCell(neighbour.Location);
        }

        public Location GetLocation(string cellLocation)
        {
            // var userSelectedCellCoords = _textReader.ReadLine();
            while (string.IsNullOrEmpty(cellLocation) ||
                   !_inputValidation.IsUserCellLocationInputValid(cellLocation, _grid.Rows, _grid.Cols))
            {
                _textWriter.Write(OutputMessages.InvalidGuessLocation());
                cellLocation = _textReader.ReadLine();
            }

            var userSelectedRow = cellLocation.Split(',')[0];
            var userSelectCol = cellLocation.Split(',')[1];
            int.TryParse(userSelectedRow, out var row);
            int.TryParse(userSelectCol, out var col);
            return new Location(row, col); // passes but we don't want this
        }

        bool IsWin() =>
            _grid.NumberOfRevealedCells + _grid.NumberOfMines == _grid.NumberOfCellsInGrid;

        bool IsLoss(Cell selectedCell) => selectedCell.CellType == CellType.Mine && selectedCell.IsRevealed;


        void EndGame(string result)
        {
            _gameOver = true;
            SetAllCellsToRevealed();
            PrintGrid();
            var message = result == "win" ? OutputMessages.GameOverYouWin : OutputMessages.GameOverMineSelected;
            Typewrite(message);
            ExitGame();
        }

        void ExitGame() => Environment.Exit(0);

        void SetAllCellsToRevealed()
        {
            foreach (var c in _grid.Cells)
            {
                c.IsRevealed = true;
            }
        }
    }
}