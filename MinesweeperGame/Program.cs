using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using MinesweeperGame.Output;

namespace MinesweeperGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = null;
            grid = new Grid(3, 3, new string[,]
                {
                    {".", "*", "."},
                    {".", ".", "."},
                    {".", ".", "."}
                }
            );
                // grid = new Grid(9, 9, new string[,]
                // {
                //     {".", "*", ".", ".", ".", ".", ".", ".", "."},
                //     {".", ".", ".", ".", "*", ".", ".", "*", "."},
                //     {".", ".", ".", "*", "*", ".", ".", "*", "."},
                //     {".", "*", ".", ".", ".", ".", ".", ".", "."},
                //     {".", ".", ".", "*", ".", "*", ".", ".", "."},
                //     {".", ".", ".", ".", ".", ".", ".", ".", "."},
                //     {"*", ".", "*", "*", ".", ".", ".", "*", "."},
                //     {".", ".", ".", "*", "*", ".", ".", "*", "."},
                //     {".", ".", ".", "*", "*", ".", ".", "*", "."}
                // }
                // );
            
            // TODO Option 2 generate grid based on a row input and a col input, needs a randomizer

            var game = new Game(grid, Console.In, Console.Out);
            game.Run();

           
        }
    }
}