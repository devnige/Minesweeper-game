using System;
using System.Text;

namespace MinesweeperGame
{
    public class RandomMineGenerator
    {
        private readonly bool[] _arr;
        private readonly int _row;
        private readonly int _col;
        private readonly int _arrLength;
        private Random _rnd;
        private string[,] _gridArray;

    public RandomMineGenerator(int row, int col)
        {
            _rnd = new Random();
            _row = row;
            _col = col;
            _arrLength = _row * _col;
            _arr = new bool[_arrLength];
            _gridArray = new string[row, col];
        }
        
        public string[,] GenerateRandomMinesAndNonMines()
        {
            var numNonMines = (int) Math.Round(_arrLength * 0.9);
            GenerateNonMines(numNonMines);
            GenerateMines(numNonMines);
            RandomSwapLocationOfMinesAndNonMines();
            MapArrayTo2DArray();
            return _gridArray;
            // TODO test that ~90% of the grid returned is not a mine
        }

        private void MapArrayTo2DArray()
        {
            int i = 0;
            for (int x = 0; x < _row; x++)
            for (int y = 0; y < _col; y++)
            {
                var gridChar = _arr[i] ? '*' : '.';
                _gridArray[x, y] = gridChar.ToString();
                i++;
            }
        }

        private void RandomSwapLocationOfMinesAndNonMines()
        {
            for (int index = 0; index < _arrLength; index++)
            {
                int pos = _rnd.Next(_arrLength);
                (_arr[index], _arr[pos]) = (_arr[pos], _arr[index]);
            }
        }

        private void GenerateMines(int numNonMines)
        {
            for (var index = numNonMines; index < _arrLength; index++)
            {
                _arr[index] = true;
            }
        }

        private void GenerateNonMines(int numNonMines)
        {
            for (var index = 0; index < numNonMines; index++)
            {
                _arr[index] = false;
            }
        }
    }
}