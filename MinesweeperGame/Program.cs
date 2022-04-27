using System;
using MinesweeperGame.Output;

namespace MinesweeperGame
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var game = new Game(Console.In, Console.Out, new Random(), new TypeWriter(Console.Out));
            game.Run();
        }
    }
}