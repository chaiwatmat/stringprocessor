using System;
using System.Linq;
using NUnit.Framework;
using Chaiwatmat.StringCalculator;

namespace Chaiwatmat.StringCalculator.Test
{
    [TestFixture]
    public class StringCalculatorTest
    {
        private StringCalculator _stringCalculator;

        public StringCalculatorTest(){
            _stringCalculator = new StringCalculator();
        }

        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2", 3)]
        public void TakeMost2Numbers_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringCalculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("1,2,3", 6)]
        [TestCase("1,2,3,10,10", 26)]
        public void UnknownNumbers_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringCalculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("1\n2\n3", 6)]
        [TestCase("1\n2,3", 6)]
        public void AllowNewLineInsteadOfComma_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringCalculator.Add(input);
            Assert.AreEqual(expected, result);
        }


        [TestCase("//;\n1;2", 3)]
        public void SpecificDelimeter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringCalculator.Add(input);
            Assert.AreEqual(expected, result);
        }


        [TestCase("-1,2,-3")]
        public void NotAllowNegativeNumbers_ShouldThrowException(string input){
            Assert.That(() => _stringCalculator.Add(input), Throws.Exception);
        }

        [TestCase("-1,2,-3", "-1,-3")]
        public void NotAllowNegativeNumbers_ShouldThrowExceptionWithShowNegativeNumbers(string input, string expectedMessage){
            // Assert.That(() => _stringCalculator.Add(input), Throws.Exception);
            var ex = Assert.Throws<Exception>(() => _stringCalculator.Add(input));

            Assert.AreEqual(expectedMessage, ex.Message);
        }


        [TestCase("2,1001", 2)]
        public void IgnoreNumberGreaterThan1000_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringCalculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("//--\n1--2--3", 6)]
        public void SpecificDelimeterWithFlexibleLength_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringCalculator.Add(input);
            Assert.AreEqual(expected, result);
        }


        [TestCase("//[%][-]\n1%2-3", 6)]
        public void SupportMultipleDelimeter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringCalculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("//[%%][--]\n1%%2--3", 6)]
        public void SupportMultipleDelimeterWithLongerThanOneCharacter_ShouldReturnSumCorrect(string input, int expected){
            var result = _stringCalculator.Add(input);
            Assert.AreEqual(expected, result);
        }


    }
}
