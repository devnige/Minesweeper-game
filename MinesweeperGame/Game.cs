using System;
using System.IO;
using System.Resources;
using MinesweeperGame.Output;

namespace MinesweeperGame
{
    public class Game
    {
        private readonly TextReader _textReader;
        private static TextWriter _textWriter;
        
        private bool _gameOver;
        private readonly InputValidation _inputValidation;
        private int _rows;
        private int _cols;
        private readonly Random _rnd;
        private readonly ITypeWriter _typeWriter;
        private readonly ConsoleOutput _consoleOutput;

        public Game(TextReader textReader, TextWriter textWriter, Random rnd, ITypeWriter typeWriter)
        {
            _rnd = rnd;
            _textReader = textReader;
            _textWriter = textWriter;
            _inputValidation = new InputValidation(textReader, textWriter);
            _typeWriter = typeWriter;
            _consoleOutput = new ConsoleOutput(textWriter);
        }

        public Grid Grid { get; private set; }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            _typeWriter.Typewrite(OutputMessages.Welcome);
            Console.ResetColor();
            _typeWriter.Typewrite(OutputMessages.Instructions);
            _typeWriter.Typewrite(OutputMessages.GridTypeOptions);
            _typeWriter.Typewrite(OutputMessages.GridSelection);
            var userInput = GetUserInput();
            var validGridSelection = _inputValidation.UserGridGenerationInputValid(userInput);
            if (validGridSelection == "1")
            {
                Random rnd = new Random();
                _rows = rnd.Next(3, 11);
                _cols = rnd.Next(3, 11);
            }
            else if (validGridSelection == "2")
            {
                _textWriter.Write(OutputMessages.EnterNumberOfRows);
                userInput = GetUserInput();
                _rows = _inputValidation.CheckGridDimensions(userInput);
                _textWriter.Write(OutputMessages.EnterNumberOfCols);
                userInput = GetUserInput();
                _cols = _inputValidation.CheckGridDimensions(userInput);
            }

            Grid = Generate2DStringGrid(_rnd, _rows, _cols);
            InitialiseGrid();
            _consoleOutput.PrintGrid(Grid);

// Cases
// The cell selected is covered and a mine is in the cell. Reveal the bomb. Game Over :(
// The cell selected is covered and a number is in the cell. Reveal the number.
// The cell selected is already uncovered - do nothing.
// When a player selects a cell with 0 mine neighbours it automatically uncovers all adjacent empty cells and their neighbours

            while (!_gameOver)
            {
                _textWriter.Write(OutputMessages.CellSelection);
                userInput = GetUserInput();
                while (string.IsNullOrEmpty(userInput) ||
                       !_inputValidation.IsUserCellLocationInputValid(userInput, _rows, _cols))
                    {
                        _textWriter.Write(OutputMessages.InvalidGuessLocation());
                        userInput = GetUserInput();
                    }
                var cellLocation = Grid.GetCellLocation(userInput);
                var selectedCell = Grid.GetSelectedCell(cellLocation);
                if (!selectedCell.IsRevealed)
                {
                    GetUserInputAndAdjustCellVisibility(selectedCell);
                }
                _consoleOutput.PrintGrid(Grid);
                var result = GameRules.CheckForWinOrLoss(Grid, selectedCell);
                if (result is "win" or "loss")
                    EndGame(result);
            }
        }

        private string GetUserInput()
        {
            return _textReader.ReadLine();
        }
        
        private Cell GetUserInputAndAdjustCellVisibility(Cell selectedCell)
        {
            string message = selectedCell.IsFlagged ?
                OutputMessages.ListFlaggedCellGuessActions() :
                OutputMessages.ListUnflaggedCellGuessActions();
            _textWriter.Write(message);
            var userInput = GetUserInput();
            var validUserAction = _inputValidation.GetValidUserAction(userInput);
            if (validUserAction == "F" && !Grid.IsFlagged(selectedCell.Location))
            {
                Grid.AddFlagToCell(selectedCell);
            }
            else if (validUserAction == "D" && Grid.IsFlagged(selectedCell.Location))
            {
                Grid.RemoveFlagFromCell(selectedCell);
            }
            else if (validUserAction == "R")
            {
                Grid.RemoveFlagFromCell(selectedCell);
                selectedCell = Grid.RevealCellAtSelectedLocationAndNeighboursIfNotTouchingAMine(selectedCell.Location);
            }
            return selectedCell;
        }

        private static Grid Generate2DStringGrid(Random rnd, int rows, int cols)
        {
            var randomMineGenerator = new RandomMineGenerator(rnd, rows, cols);
            var random2DMineStringArray = randomMineGenerator.GenerateRandomMinesAndNonMines();
            return new Grid(rows, cols, random2DMineStringArray);
        }

        void InitialiseGrid()
        {
            var mines = Grid.InitialiseCells();
            Grid.IncrementNeighbourCellsIfTouchingAMine(mines);
        }
        
        void EndGame(string result)
        {
            _gameOver = true;
            Grid.SetAllCellsToRevealed();
            _consoleOutput.PrintGrid(Grid);
            var message = result == "win" ? OutputMessages.GameOverYouWin : OutputMessages.GameOverMineSelected;
            _textWriter.Write(message);
        }
    }
}