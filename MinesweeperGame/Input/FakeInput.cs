using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinesweeperGame
{
    public class FakeInput : TextReader
    {
        private readonly Queue<string> _stringQueue = new Queue<string>();
        // public readonly Dictionary<string, int> ReadStrings = new Dictionary<string, int>();

        
        public void SetupSequence(List<string> someString)
        {
            foreach(string str in someString)
            {
                _stringQueue.Enqueue(str);
            }
        }

        public override string ReadLine()
        {
            return _stringQueue.Dequeue();
        }
    }
}