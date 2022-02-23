using System;
using System.IO;
using System.Xml;
using MinesweeperGame.Output;

namespace MinesweeperGame
{
    public class InputValidation
    {
        private TextReader _input;
        public InputValidation(TextReader textReader)
        {
            _input = textReader;
        }
        public bool IsUserInputValid(string str, int rows, int cols)
        {
            var userSelectedRow = str.Split(',')[0];
            var userSelectCol = str.Split(',')[1];
            int.TryParse(userSelectedRow, out var row);
            int.TryParse(userSelectCol, out var col);
            if ((row >= 0 && row < rows) && (col >= 0 && col < cols))
                return true;
            return false;
        }

        public int CheckGridDimensions(string str)
        {
            int.TryParse(str, out int result);
            var number = result;
            while ((number is < Constants.MinimumGridRowOrColDimension or > Constants.MaximumGridRowOrColDimension))
            {
                var newInput = _input.ReadLine();
                int.TryParse(newInput, out int result2);
                number = result2;
            }
            return number;
        }
    }
}