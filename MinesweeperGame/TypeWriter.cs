using System.IO;
using System.Net.Mime;
using System.Threading;

namespace MinesweeperGame
{
    public class TypeWriter : ITypeWriter
    {
        private readonly TextWriter _textWriter;

        public TypeWriter(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }
        public void Typewrite(string message)
        {
            foreach (var t in message)
            {
                _textWriter.Write(t);
                Thread.Sleep(15);
            }
        }
    }
}