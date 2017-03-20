using System;
using System.Linq;
using Xunit;
using Chaiwatmat.StringProcessor;

namespace Chaiwatmat.StringProcessor.Test
{
    public class StringProcessorTest
    {
        private StringProcessor _stringProcessor;

        public StringProcessorTest(){
            _stringProcessor = new StringProcessor();
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        public void TakeMost2Numbers_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1,2,3", 6)]
        [InlineData("1,2,3,10,10", 26)]
        public void UnknownNumbers_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1\n2\n3", 6)]
        [InlineData("1\n2,3", 6)]
        public void AllowNewLineInsteadOfComma_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//;\n1;2", 3)]
        public void SpecificDelimeter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("-1,2,-3")]
        public void NotAllowNegativeNumbers_ShouldThrowException(string input){
            var ex = Record.Exception(() => _stringProcessor.Add(input));
            Assert.NotNull(ex);
        }

        [Theory]
        [InlineData("-1,2,-3", "-1,-3")]
        public void NotAllowNegativeNumbers_ShouldThrowExceptionWithShowNegativeNumbers(string input, string expectedMessage){
            var ex = Record.Exception(() => _stringProcessor.Add(input));

            Assert.Equal(expectedMessage, ex.Message);
        }

        [Theory]
        [InlineData("2,1001", 2)]
        public void IgnoreNumberGreaterThan1000_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//--\n1--2--3", 6)]
        public void SpecificDelimeterWithFlexibleLength_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//[%][-]\n1%2-3", 6)]
        public void SupportMultipleDelimeter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//[%%][--]\n1%%2--3", 6)]
        public void SupportMultipleDelimeterWithLongerThanOneCharacter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.Equal(expected, result);
        }

    }
}
