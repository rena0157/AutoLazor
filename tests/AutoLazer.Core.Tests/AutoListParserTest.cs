using System;
using Xunit;
using Xunit.Abstractions;
using AutoLazer.Core;
using System.Collections.Generic;

namespace AutoLazer.Core.Tests
{
    /// <summary>
    /// Test class for the AutoListParser
    /// </summary>
    public class AutoListParserTest
    {
        /// <summary>
        /// The logger for the class
        /// </summary>
        private readonly ITestOutputHelper _logger;

        /// <summary>
        /// Constructor that takes in a logger 
        /// </summary>
        /// <param name="testOutputHelper">Logger DI</param>
        public AutoListParserTest(ITestOutputHelper testOutputHelper) =>
            _logger = testOutputHelper;

        /// <summary>
        /// Get objects Test for double values
        /// </summary>
        /// <param name="inputText">The input text to be passed to the function</param>
        /// <param name="pattern">The pattern that is passed to the function</param>
        /// <param name="expected">The expected value from the function</param>
        /// <remarks>
        /// Test 1: Testing two lengths on multiple lines
        /// Test 2: Testing Length with a capital L
        /// Test 3: Testing Length without a decimal
        /// Test 4: Testing Length with 4 decimals
        /// Test 5: Testing area with a lowercase a
        /// Test 6: Testing area with a decimal only
        /// Test 7: Testing area with a capital 'A'
        /// </remarks>
        [Theory]
        [InlineData("length 23.23 \n length 12.23", AutoListPatterns.LinesLengthPattern, new double[] {23.23, 12.23})]
        [InlineData("Length 23.23", AutoListPatterns.LinesLengthPattern, new double[] {23.23})]
        [InlineData("Length 23", AutoListPatterns.LinesLengthPattern, new double[]{23})]
        [InlineData("length 23.8347", AutoListPatterns.LinesLengthPattern, new double[] {23.8347})]
        [InlineData("area 34.34", AutoListPatterns.HatchAreaPattern, new double[] {34.34})]
        [InlineData("area 34.", AutoListPatterns.HatchAreaPattern, new double[] {34})]
        [InlineData("Area 34.", AutoListPatterns.HatchAreaPattern, new double[] {34})]
        public void GetObjectsTestDouble(string inputText, string pattern, double[] expected) =>
            Assert.Equal(expected, AutoListParser
                .GetObjects<double>(inputText, pattern));

        /// <summary>
        /// GetObjects test for strings
        /// </summary>
        /// <param name="inputText">The input text to be tests</param>
        /// <param name="pattern">The pattern for the regex</param>
        /// <param name="expected">The expected text </param>
        [Theory]
        [InlineData("text Block 1", AutoListPatterns.TextPattern, new string[]{"Block 1"})]
        [InlineData("Text Block 1", AutoListPatterns.TextPattern, new string[]{"Block 1"})]
        [InlineData("Contents: Block 1", AutoListPatterns.TextPattern, new string[]{"Block 1"})]
        public void GetObjectsTestString(string inputText, string pattern, string[] expected) =>
            Assert.Equal(expected, AutoListParser
                .GetObjects<string>(inputText, pattern));
    }
}
