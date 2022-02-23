using System;

namespace MinesweeperGame
{
    public class Constants
    {
        // Console cell selection actions
        public const string Left = "a";
        public const string Right = "d";
        public const string Up = "w";
        public const string Down = "s";
        public const string SelectDeselect = "e";
        public const string FinishSelecting = "q";
        
        // Console responses
        public const string YesResponse = "y";
        public const string NoResponse = "n";
        
        public const int MinimumGridRowOrColDimension = 2;
        public const int MaximumGridRowOrColDimension = 10;

        // Cell display symbols
        public const string HiddenCell = "\u25fc  ";
        public const string RevealedCell = "  "; // empty string
        public const string FlaggedCell = "\u2690  ";
        public const string MineCell = "*  ";
        
        // Display Colours
        public const ConsoleColor RevealedCellNotAMine = ConsoleColor.Yellow;
        public const ConsoleColor FlaggedCellColour = ConsoleColor.Blue;
        public const ConsoleColor DefaultTextColour = ConsoleColor.White;
        public const ConsoleColor HiddenCellColour = ConsoleColor.Cyan;
        public const ConsoleColor RevealedCellMine = ConsoleColor.Red;
    }
}