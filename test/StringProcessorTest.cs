using System;
using System.Linq;
using NUnit.Framework;
using Chaiwatmat.StringProcessor;

namespace Chaiwatmat.StringProcessor.Test
{
    [TestFixture]
    public class StringProcessorTest
    {
        private StringProcessor _stringProcessor;

        public StringProcessorTest(){
            _stringProcessor = new StringProcessor();
        }

        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2", 3)]
        public void TakeMost2Numbers_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("1,2,3", 6)]
        [TestCase("1,2,3,10,10", 26)]
        public void UnknownNumbers_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("1\n2\n3", 6)]
        [TestCase("1\n2,3", 6)]
        public void AllowNewLineInsteadOfComma_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.AreEqual(expected, result);
        }


        [TestCase("//;\n1;2", 3)]
        public void SpecificDelimeter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.AreEqual(expected, result);
        }


        [TestCase("-1,2,-3")]
        public void NotAllowNegativeNumbers_ShouldThrowException(string input){
            Assert.That(() => _stringProcessor.Add(input), Throws.Exception);
        }

        [TestCase("-1,2,-3", "-1,-3")]
        public void NotAllowNegativeNumbers_ShouldThrowExceptionWithShowNegativeNumbers(string input, string expectedMessage){
            // Assert.That(() => _stringProcessor.Add(input), Throws.Exception);
            var ex = Assert.Throws<Exception>(() => _stringProcessor.Add(input));

            Assert.AreEqual(expectedMessage, ex.Message);
        }


        [TestCase("2,1001", 2)]
        public void IgnoreNumberGreaterThan1000_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("//--\n1--2--3", 6)]
        public void SpecificDelimeterWithFlexibleLength_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.AreEqual(expected, result);
        }


        [TestCase("//[%][-]\n1%2-3", 6)]
        public void SupportMultipleDelimeter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("//[%%][--]\n1%%2--3", 6)]
        public void SupportMultipleDelimeterWithLongerThanOneCharacter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringProcessor.Add(input);
            Assert.AreEqual(expected, result);
        }


    }
}
