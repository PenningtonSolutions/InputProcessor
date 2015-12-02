using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InputProcessor.Tests
{
    [TestClass()]
    public class RegularExpressionParserTests
    {
        static RegularExpressionParser regularExpressionParser;

        [ClassInitialize]
        public static void RegularExpressionParserInit(TestContext context)
        {
            regularExpressionParser = new RegularExpressionParser();
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseStringThrowArgumentNullExceptionTestForDelimiter()
        {
            regularExpressionParser = new RegularExpressionParser();

            foreach (ProcessedWord word in regularExpressionParser.ParseString("ab6tf4", null))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseStringThrowArgumentExceptionTestForDelimiter()
        {
            regularExpressionParser = new RegularExpressionParser();

            foreach (ProcessedWord word in regularExpressionParser.ParseString("ab6tf4", @"[^a-1A-0]"))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseStringThrowArgumentNullExceptionTestForInput()
        {
            regularExpressionParser = new RegularExpressionParser();

            foreach (ProcessedWord word in regularExpressionParser.ParseString(null, @"[^a-zA-Z]"))
            {
                Assert.Fail();
            }
        }

        //would need add a shim but sadly it is only available in VS Enterprise
        [Ignore]
        [TestMethod()]
        [ExpectedException(typeof(RegexMatchTimeoutException))]
        public void ParseStringThrowRegexMatchTimeoutExceptionTest()
        {
            regularExpressionParser = new RegularExpressionParser();

            foreach (ProcessedWord word in regularExpressionParser.ParseString(null, @"[^a-zA-Z]"))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ParseStringWithFirstCharacterBeingADelimiter()
        {
            regularExpressionParser = new RegularExpressionParser();

            List<ProcessedWord> words = new List<ProcessedWord>();

            foreach (ProcessedWord word in regularExpressionParser.ParseString(";wow6car'", @"[^a-zA-Z]"))
            {
                words.Add(word);
            }

            Assert.AreEqual(3, words.Count);

            // Asserts for the first word
            Assert.AreEqual(string.Empty, words[0].word);
            Assert.AreEqual(";", words[0].delimiter);
        }

        [TestMethod()]
        public void ParseStringWithDelimiterAtEnd()
        {
            regularExpressionParser = new RegularExpressionParser();

            List<ProcessedWord> words = new List<ProcessedWord>();

            foreach (ProcessedWord word in regularExpressionParser.ParseString("radar;wow6car'", @"[^a-zA-Z]"))
            {
                words.Add(word);
            }

            Assert.AreEqual(3, words.Count);

            // Asserts for the first word
            Assert.AreEqual("radar", words[0].word);
            Assert.AreEqual(";", words[0].delimiter);

            // Asserts for the second word
            Assert.AreEqual("wow", words[1].word);
            Assert.AreEqual("6", words[1].delimiter);

            // Asserts for the third word
            Assert.AreEqual("car", words[2].word);
            Assert.AreEqual("'", words[2].delimiter);
        }

        [TestMethod()]
        public void ParseStringWithoutDelimiterAtEnd()
        {
            regularExpressionParser = new RegularExpressionParser();

            List<ProcessedWord> words = new List<ProcessedWord>();

            foreach (ProcessedWord word in regularExpressionParser.ParseString("radar;wow6car", @"[^a-zA-Z]"))
            {
                words.Add(word);
            }

            Assert.AreEqual(3, words.Count);

            // Asserts for the first word
            Assert.AreEqual("radar", words[0].word);
            Assert.AreEqual(";", words[0].delimiter);

            // Asserts for the second word
            Assert.AreEqual("wow", words[1].word);
            Assert.AreEqual("6", words[1].delimiter);

            // Asserts for the third word
            Assert.AreEqual("car", words[2].word);
            Assert.AreEqual(string.Empty, words[2].delimiter);
        }

        [TestMethod()]
        public void ParseStringWithDelimitersNextToEachOther()
        {
            regularExpressionParser = new RegularExpressionParser();

            List<ProcessedWord> words = new List<ProcessedWord>();

            foreach (ProcessedWord word in regularExpressionParser.ParseString("radar;wow65car", @"[^a-zA-Z]"))
            {
                words.Add(word);
            }

            Assert.AreEqual(4, words.Count);

            // Asserts for the first word
            Assert.AreEqual("radar", words[0].word);
            Assert.AreEqual(";", words[0].delimiter);

            // Asserts for the second word
            Assert.AreEqual("wow", words[1].word);
            Assert.AreEqual("6", words[1].delimiter);

            // Asserts for the third word
            Assert.AreEqual(string.Empty, words[2].word);
            Assert.AreEqual("5", words[2].delimiter);

            // Asserts for the fourth word
            Assert.AreEqual("car", words[3].word);
            Assert.AreEqual(string.Empty, words[3].delimiter);
        }

        [TestMethod()]
        public void ParseStringWithSingleCharacterDelimiter()
        {
            regularExpressionParser = new RegularExpressionParser();

            List<ProcessedWord> words = new List<ProcessedWord>();

            foreach (ProcessedWord word in regularExpressionParser.ParseString("radar;wow;;car;", ";"))
            {
                words.Add(word);
            }

            Assert.AreEqual(4, words.Count);

            // Asserts for the first word
            Assert.AreEqual("radar", words[0].word);
            Assert.AreEqual(";", words[0].delimiter);

            // Asserts for the second word
            Assert.AreEqual("wow", words[1].word);
            Assert.AreEqual(";", words[1].delimiter);

            // Asserts for the third word
            Assert.AreEqual(string.Empty, words[2].word);
            Assert.AreEqual(";", words[2].delimiter);

            // Asserts for the fourth word
            Assert.AreEqual("car", words[3].word);
            Assert.AreEqual(";", words[3].delimiter);
        }
    }
}