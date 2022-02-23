using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinesweeperGame.Output
{
    public class FakeWriter : TextWriter
    {
        public List<string> Output;
        
        public FakeWriter()
        {
            Output = new List<string>();
        }

        public override Encoding Encoding { get; }

        public override void WriteLine(string str)
        {
            Output.Add(str);
        }
    }
}