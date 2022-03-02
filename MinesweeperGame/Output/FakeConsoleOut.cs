using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinesweeperGame.Output
{
    public class FakeConsoleOut : TextWriter
    {
        //public List<string> Output;
        public StringBuilder StringBuilder;
        
        public FakeConsoleOut()
        {
            //Output = new List<string>();
            StringBuilder = new StringBuilder();
        }

        public override Encoding Encoding { get; }

        public override void Write(string str)
        {
            //Output.Add(str);
            StringBuilder.Append(str);
        }
        
        public string GetOutput()
        {
            return StringBuilder.ToString();
        }
    }
}