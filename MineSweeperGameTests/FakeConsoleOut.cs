using System.Collections.Generic;
using System.IO;
using System.Text;
using MinesweeperGame.Output;

namespace MinesweeperGameTests
{
    public class FakeConsoleOut : TextWriter, ITypeWriter
    {
        public readonly Dictionary<string, int> _writtenStrings = new ();
        
        public override Encoding Encoding { get; }
        
        public override void Write(string str)
        {
            AddStringToWrittenString(str);
        }

        private void AddStringToWrittenString(string str)
        {
            if (_writtenStrings.ContainsKey(str))
            {
                _writtenStrings[str]++;
            }
            else
            {
                _writtenStrings.Add(str, 1);
            }
        }

        public void Typewrite(string message)
        {
            AddStringToWrittenString(message);
        }
    }
}