using Xunit;
using MinesweeperGame;

namespace MineSweeperTests;

public class GameRulesTests
{
    [Fact]
    public void Given_CheckForWinOrLoss_When_UserSelectsLastNonMineCellLocation_Then_WinIsReturned()
    {
        var grid = new Grid(1, 1, new[,] {{"."}});
        grid.InitialiseCells();
        var selectedCell = grid.RevealCellAtSelectedLocationAndNeighboursIfNotTouchingAMine(grid.GetCellLocation("0,0"));
        var actual = GameRules.CheckForWinOrLoss(grid, selectedCell);
        var expected = "win";
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Given_CheckForWinOrLoss_When_UserSelectsMineCellLocation_Then_LossIsReturned()
    {
        var grid = new Grid(1, 1, new[,] {{"*"}});
        grid.InitialiseCells();
        var selectedCell = grid.RevealCellAtSelectedLocationAndNeighboursIfNotTouchingAMine(grid.GetCellLocation("0,0"));
        var actual = GameRules.CheckForWinOrLoss(grid, selectedCell);
        var expected = "loss";
        Assert.Equal(expected, actual);
    }
}