using System;

namespace MinesweeperGame
{
    public class Cell : IEquatable<Cell>
    {
        public Location Location { get; set; }
        public int NeighbouringMines;
        public bool IsRevealed = false;
        public bool IsFlagged = false;
        public CellType CellType { get; set; }

        public Cell(Location location, int neighbouringMines, CellType cellType)
        {
            Location = location;
            NeighbouringMines = neighbouringMines;
            CellType = cellType;
        }
        
        public bool Equals(Cell other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return NeighbouringMines == other.NeighbouringMines && IsRevealed == other.IsRevealed &&
                   Location.Equals(other.Location) && CellType == other.CellType;
        }
    }
}