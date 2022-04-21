using System.Collections.Generic;
using MinesweeperGame;
using MinesweeperGame.Output;
using Moq;
using Xunit;

namespace MinesweeperGameTests
{
    public class GameTests
    {
        [Fact]
        public void Given_Run_When_UserInputSelectsAMineLocation_Then_TheGameEndsWithGameOverMineSelectedOutputMessage()
        {
            var mockRandomMineGenerator = new Mock<RandomMineGenerator>();
            mockRandomMineGenerator.Setup(mk =>
                mk.GenerateRandomMinesAndNonMines()).Returns(
                new [,]
                    {
                        {".", ".", "*", "."},
                        {".", ".", ".", "."},
                        {".", ".", ".", "."},
                        {".", ".", ".", "."}
                    });
            var fakeInput = new FakeInput();
            var fakeConsoleOut = new FakeConsoleOut();
            var game = new Game(fakeInput, fakeConsoleOut);
            fakeInput.SetupSequence(new List<string> {"2", "4", "4", "0,2", "R"}); // enqueue
            game.Run(); // dequeue 1 = "2", 2 = "4", 3 = "4", 4 = "0,2", 5 = "R"
            var loseKey = OutputMessages.GameOverMineSelected;
            Assert.True(fakeConsoleOut.writtenStrings.ContainsKey(loseKey));
        }
    }
}