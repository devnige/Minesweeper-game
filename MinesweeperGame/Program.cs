using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Xml;
using MinesweeperGame.Output;

namespace MinesweeperGame
{
    static class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(Console.In, Console.Out, new Random(), new TypeWriter(Console.Out));
            game.Run();
        }
    }
}