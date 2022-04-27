using System.Collections.Generic;
using MinesweeperGame;
using Xunit;

namespace MineSweeperTests
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
            Assert.True(inputValidation.IsUserCellLocationInputValid(fakeInput.ReadLine(), 5, 5 ));
        }
        
        [Fact]
        public void Given_IsUserInputValid_WhenInputContainsANonValidCellLocation_Then_ReturnsTFalse()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"11,11"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            Assert.False(inputValidation.IsUserCellLocationInputValid(fakeInput.ReadLine(), 3, 3));
        }
        
        [Fact]
        public void Given_UserGridGenerationInputValid_WhenInputContainsANonValidCharacter_Then_OnlyAcceptsValidCharacter()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"3", "1"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            var actual = inputValidation.UserGridGenerationInputValid(fakeInput.ReadLine());
            var expected = "1";
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_GetValidUserAction_WhenInputContainsANonValidCharacter_Then_OnlyAcceptsValidCharacter()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"Z", "R"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            var actual = inputValidation.GetValidUserAction(fakeInput.ReadLine());
            var expected = "R";
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_IsUserCellLocationInputValid_WhenInputContainsAValidSelection_Then_ResultIsTrue()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"1,1"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            var result = inputValidation.IsUserCellLocationInputValid(fakeInput.ReadLine(), 3, 3);
            Assert.True(result);
        }
        
        [Fact]
        public void Given_IsUserCellLocationInputValid_WhenInputContainsAnInvalidSelection_Then_ResultIsFalse()
        {
            FakeInput fakeInput = new FakeInput();
            FakeConsoleOut fakeConsoleOut = new FakeConsoleOut();
            fakeInput.SetupSequence(new List<string> {"3,3"});
            InputValidation inputValidation = new InputValidation(fakeInput, fakeConsoleOut);
            var result = inputValidation.IsUserCellLocationInputValid(fakeInput.ReadLine(), 3, 3);
            Assert.False(result);
        }
    }
}