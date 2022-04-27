using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinesweeperGameTests
{
    public class FakeInput : TextReader
    {
        private readonly Queue<string> _stringQueue = new Queue<string>();
        
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