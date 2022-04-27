using System;
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
        public void Given_Run_When_UserGameRunsToCompletion_Then_AllWelcomeMessagesAndInstructionsArePrintedOnce()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(mk =>
                mk.Next()).Returns(0);
            var fakeInput = new FakeInput();
            var fakeConsoleOut = new FakeConsoleOut();
            var game = new Game(fakeInput, fakeConsoleOut, mockRandom.Object, fakeConsoleOut);
            fakeInput.SetupSequence(new List<string> {"2", "4", "4", "0,0", "R"});
            game.Run();
            Assert.True(fakeConsoleOut._writtenStrings[OutputMessages.Welcome] == 1);
        }
        
        [Fact]
        public void Given_Run_When_UserInputSelectsAMineLocation_Then_TheGameEndsWithGameOverMineSelectedOutputMessage()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(mk =>
                mk.Next()).Returns(0);
            var fakeInput = new FakeInput();
            var fakeConsoleOut = new FakeConsoleOut();
            var game = new Game(fakeInput, fakeConsoleOut, mockRandom.Object, fakeConsoleOut);
            fakeInput.SetupSequence(new List<string> {"2", "4", "4", "0,0", "R"});
            game.Run();
            var loseKey = OutputMessages.GameOverMineSelected;
            Assert.True(fakeConsoleOut._writtenStrings.ContainsKey(loseKey));
        }
        
        [Fact]
        public void Given_Run_When_UserInputSelectsAllNonMineLocations_Then_TheGameEndsWithGameOverYouWinOutputMessage()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(mk =>
                mk.Next()).Returns(0);
            var fakeInput = new FakeInput();
            var fakeConsoleOut = new FakeConsoleOut();
            var game = new Game(fakeInput, fakeConsoleOut, mockRandom.Object, fakeConsoleOut);
            fakeInput.SetupSequence(new List<string> {"2", "4", "4",
                "0,1", "R", "1,0", "R", "1,1", "R",
                "2,2", "R", "2,3", "R", "3,2", "R",
                "0,2", "R", "2,0", "R"
                }
            ); 
            game.Run();
            var winKey = OutputMessages.GameOverYouWin;
            Assert.True(fakeConsoleOut._writtenStrings.ContainsKey(winKey));
        }
    }
}