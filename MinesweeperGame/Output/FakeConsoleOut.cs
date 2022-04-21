using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinesweeperGame.Output
{
    public class FakeConsoleOut : TextWriter
    {
        //public List<string> Output;
        public Dictionary<string, int> writtenStrings = new Dictionary<string, int>();
        
        public override Encoding Encoding { get; }
        
        public override void Write(string str)
        {
            AddStringToWrittenString(str);
        }
        
        private void AddStringToWrittenString(string writtenString)
        {
            if (writtenStrings.ContainsKey(writtenString))
            {
                writtenStrings[writtenString]++;
            }
            else
            {
                writtenStrings.Add(writtenString, 1);
            }
        }
    }
}