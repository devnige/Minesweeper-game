using System.IO;
using System.Net.Mime;
using System.Xml;

namespace MinesweeperGame
{
    public interface ITypeWriter
    {
        public void Typewrite(string message);
    }
}