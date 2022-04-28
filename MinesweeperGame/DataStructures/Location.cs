using System;

namespace MinesweeperGame
{
    public struct Location : IEquatable<Location>
        {
            public int Row { get; set; }
            public int Col { get; set; }

            public Location(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public bool Equals(Location other)
            {
                return Row == other.Row && Col == other.Col;
            }

            public override bool Equals(object obj)
            {
                return obj is Location other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Row, Col);
            }
        }
}