using System;
using System.IO;
using System.Xml;
using Castle.Core.Internal;
using MinesweeperGame.Output;

namespace MinesweeperGame
{
    public class InputValidation
    {
        private TextReader _input;
        private TextWriter _output;
        public InputValidation(TextReader textReader, TextWriter textWriter)
        {
            _input = textReader;
            _output = textWriter;
        }

        public string UserGridGenerationInputValid(string str)
        {
            while (str is not ("1" or "2"))
            {
                _output.WriteLine(OutputMessages.InvalidGridSelection());
                str = _input.ReadLine();
            }
            return str;
        }

        public string GetValidUserAction(string str)
        {
            while (str.IsNullOrEmpty() || str?.ToUpper() is not ("R" or "F" or "D"))
            {
                _output.WriteLine(OutputMessages.InvalidUserAction());
                str = _input.ReadLine();
            }
            return str.ToUpper();
        }
        
        public bool IsUserCellLocationInputValid(string str, int rows, int cols)
        {
            var splitString = str.Split(',');
            int.TryParse(splitString[0], out var row);
            int.TryParse(splitString[1], out var col);
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
                _output.WriteLine(OutputMessages.InvalidGridDimension());
                var newInput = _input.ReadLine();
                int.TryParse(newInput, out int result2);
                number = result2;
            }
            return number;
        }
    }
}