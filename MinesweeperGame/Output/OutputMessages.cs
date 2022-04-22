using System;
using System.Threading;

namespace MinesweeperGame.Output
{
    public class OutputMessages
    {
        protected static readonly string eNL = Environment.NewLine;
        
        public static readonly string Welcome = eNL + "                              WELCOME TO" + eNL +
                                                "   _____  .__" + eNL +
                                                "  /     \\ |__| ____   ____   ________  _  __  ____   ____ ______    ____________" + eNL +
                                                " /  \\ /  \\|  |/    \\_/ __ \\ /  ___/\\ \\/ \\/ /_/ __ \\_/ __ \\\\  ___ \\_/ __ \\\\_  __ \\" + eNL +
                                                "/    Y    \\  |   |  \\  ___/ \\___ \\  \\     / \\  ___/\\  ___/|  |_> >   ___/ |  | \\/" + eNL +
                                                "\\____|__  /__|___|  /\\___ >  ____  > \\/\\_/   \\___ > \\___ >   \\__/  \\___   >__|" + eNL +
                                                " \\/               \\/     \\/     \\/               \\/     \\/|__|         \\/" + eNL + eNL;

        public static readonly string HowToPlay = "HOW TO PLAY" + eNL;
        
        public static readonly string Instructions = "MineSweeper is a game where you need to guess the location of mines." + eNL +
                                                     "You win by selecting and revealing all the locations that are not mines." + eNL +
                                                     "If you select a mine location, the game is over!" + eNL;

        public static readonly string GridGenerationOptions = "You can play a random pre-determined grid or generate your own custom grid." + eNL;
            
        public static readonly string GridSelection = "Enter 1 for random grid or 2 for custom grid." + eNL;

        public const string EnterNumberOfCols = "Please enter the number of columns:";
        public const string EnterNumberOfRows = "Please enter the number of rows:";
        
        public static readonly string CellSelection = "Select a cell, e.g. 0,0 for the top left cell" + eNL;

        public static readonly string GameOverMineSelected = "***KABOOM***" + eNL +
                                                             "You selected a mine! :(" + eNL +
                                                             "GAME OVER";
        
        public static readonly string GameOverYouWin = "!!!YOU WIN!!!" + eNL +
                                                             "Congratulations! :)" + eNL +
                                                             "GAME OVER";
        
        public static string ListUnflaggedCellGuessActions()
        {
            return "R = reveal" + Environment.NewLine
                   + "F = flag" + Constants.Down + Environment.NewLine
                   + "Please select an action:";
        }
        
        public static string ListFlaggedCellGuessActions()
        {
            return "R = reveal" + Environment.NewLine
                                + "D = deflag" + Environment.NewLine
                                + "Please select an action:";
        }
        
        public static string InvalidGuessLocation()
        {
            return
                $"Invalid input. Please select a cell location that is on the grid." + eNL;
        }

        public static string InvalidGridSelection()
        {
            return
                "Invalid input. Please select 1 for a random grid or 2 for a custom grid";
        }
        public static string InvalidGridDimension()
        {
            return
                $"Invalid input. Please select a grid dimension between {Constants.MinimumGridRowOrColDimension}" +
                $" and {Constants.MaximumGridRowOrColDimension}";
        }

        public static string InvalidUserAction()
        {
            return
                "Invalid user action. You can (R)eveal, (F)lag or (D)eflag a location on the grid";
        }
    }
}