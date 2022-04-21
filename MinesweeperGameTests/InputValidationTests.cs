using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using MinesweeperGame;
using MinesweeperGame.Output;
using Xunit;

namespace MinesweeperGameTests
{
    public class InputValidationTests
    {
        [Fact]
        public void Given_UserInput_WhenInputIsWithinMinAndMax_ReturnsTheNumber()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"5"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            var actual = inputValidation.CheckGridDimensions(fakeInput.ReadLine());
            var expected = 5;
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_CheckGridDimensions_WhenInputIsOutsideMinAndMax_Then_GetsAnotherInputUntilInputIsInRange()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"11", "5"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            var actual = inputValidation.CheckGridDimensions(fakeInput.ReadLine());
            var expected = 5;
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_CheckGridDimensions_WhenInputContainsANegative_Then_GetsAnotherInputUntilInputIsInRange()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"-1", "5", "6"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            var actual = inputValidation.CheckGridDimensions(fakeInput.ReadLine());
            var expected = 5;
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_IsUserInputValid_WhenInputContainsAValidCellLocation_Then_ReturnsTrue()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"0,0"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            Assert.True(inputValidation.IsUserInputValid(fakeInput.ReadLine(), 5, 5 ));
        }
        
        [Fact]
        public void Given_IsUserInputValid_WhenInputContainsANonValidCellLocation_Then_ReturnsTFalse()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"11,11"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            Assert.False(inputValidation.IsUserInputValid(fakeInput.ReadLine(), 3, 3));
        }
        
        // [Fact]
        // public void Given_IsUserInputValid_WhenInputContainsANonValidCellLocation_Then_ReturnsTFalse()
        // {
        //     FakeInput fakeInput = new FakeInput();
        //     FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
        //     fakeInput.SetupSequence(new List<string> {"4,4"});
        //     InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
        //     Assert.False(inputValidation.IsUserInputValid(fakeInput.ReadLine(), 3, 3));
        // }
    }
}