using MinesweeperGame;
using Xunit;

namespace MinesweeperGameTests
{
    public class CellTests
    {
        [Fact]
        public void Given_ACell_WhenACellObjectIsCreatedAndTheValueIsIncrementedBy1_ThenTheCellValueis1()
        {
            var actual = new Cell(new Location(0, 0), 0, CellType.NotAMine)
            {
                NeighbouringMines = 1 //Should be covered by a method.
            };
            var expected = new Cell(new Location(0, 0), 1, CellType.NotAMine);
            Assert.Equal(expected, actual);
        }
    }
}