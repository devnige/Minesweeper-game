
// using System.Collections.Generic;
// using MinesweeperGame;
// using MinesweeperGame.Output;
// using Xunit;
//
// namespace IntegrationTests
// {
//     public class ApplicationTests
//     {
//         [Fact]
//         public void Given_RunApplication_WhenGameRunsWithMineLocationSelected_Then_PlayerLoses()
//         {
//             // Arrange
//             var grid = new Grid(3, 3, new string[,]
//                 {
//                     {".", "*", "."},
//                     {".", ".", "."},
//                     {".", ".", "."}
//                 }
//             );
//             var fakeInput = new FakeInput();
//             var fakeConsoleOut = new FakeConsoleOut();
//             var inputSequence = new List<string> {"1", "0,1", "R"};
//
//             var expectedFinalGridString = $"    0  1  2\n+" +
//                                           $"    _  _  _\n" +
//                                           $" 0| 1  *  1\n" +
//                                           $" 1| 1  1  1\n" +
//                                           $" 2| 0  0  0\n";
//
//             // Act
//             fakeInput.SetupSequence(inputSequence);
//             var game = new Game(grid, fakeInput, fakeConsoleOut);
//             game.Run();
//             // Could use a test spy which is a generic object which would watch a class as it is used.
//             // Testing that the output of the system is what it is supposed to be.
//             // Being too granular with my test
//             // Don't want to be duplicating strings or text
//             Assert.Equal(expectedFinalGridString, fakeConsoleOut.writtenStrings[fakeConsoleOut.writtenStrings.Count - 2]);
//                 
//                 
//                 
//                 
//                 
//                 "WELCOME TO" + enl +
//                          "   _____  .__" + enl +
//                          "  /     \\ |__| ____   ____   ________  _  __  ____   ____ ______    ____________" + enl +
//                          " /  \\ /  \\|  |/    \\_/ __ \\ /  ___/\\ \\/ \\/ /_/ __ \\_/ __ \\\\  ___ \\_/ __ \\\\_  __ \\" + enl +
//                          "/    Y    \\  |   |  \\  ___/ \\___ \\  \\     / \\  ___/\\  ___/|  |_> >   ___/ |  | \\/" + enl +
//                          "\\____|__  /__|___|  /\\___ >  ____  > \\/\\_/   \\___ > \\___ >   \\__/  \\___   >__|" + enl +
//                          " \\/               \\/     \\/     \\/               \\/     \\/|__|         \\/" + enl + enl +
//                          "HOW TO PLAY" + enl + "MineSweeper is a game where you need to guess the location of mines." + enl +
//                          "You win by selecting and revealing all the locations that are not mines." + enl +
//                          "If you select a mine location, the game is over!" + enl +
//                          "You can play a random pre-determined grid or generate your own custom grid." + enl +
//                          "Enter 1 for random grid or 2 for custom grid." + enl, output);
//         }
//     }
// }