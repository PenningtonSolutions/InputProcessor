using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace InputProcessor.Tests
{
    [TestClass()]
    public class UniqueCharacterStringProcessorTests
    {
        static RegularExpressionParser regularExpressionParser;
        static UniqueCharacterStringProcessor uniqueCharacterStringProcessor;

        [ClassInitialize]
        public static void UniqueCharacterStringProcessorInit(TestContext context)
        {
            regularExpressionParser = new RegularExpressionParser();
            uniqueCharacterStringProcessor = new UniqueCharacterStringProcessor(regularExpressionParser);
        }

        [TestMethod()]
        public void ParseStringInputNullOrEmptyTest()
        {
            List<string> words = new List<string>();

            foreach (string result in uniqueCharacterStringProcessor.ParseString(null, @"[^a-1A-0]"))
            {
                words.Add(result);
            }

            Assert.AreEqual(1, words.Count);
            Assert.AreEqual("0", words[0]);

            words.Clear();

            foreach (string result in uniqueCharacterStringProcessor.ParseString(string.Empty, @"[^a-1A-0]"))
            {
                words.Add(result);
            }

            Assert.AreEqual(1, words.Count);
            Assert.AreEqual("0", words[0]);
        }

        [TestMethod()]
        public void ParseStringDelimiterNullOrEmptyTest()
        {
            List<string> words = new List<string>();

            foreach (string result in uniqueCharacterStringProcessor.ParseString("radar", null))
            {
                words.Add(result);
            }

            Assert.AreEqual(1, words.Count);
            Assert.AreEqual("r2r", words[0]);

            words.Clear();

            foreach (string result in uniqueCharacterStringProcessor.ParseString("radar", string.Empty))
            {
                words.Add(result);
            }

            Assert.AreEqual(1, words.Count);
            Assert.AreEqual("r2r", words[0]);
        }

        // Could stub the call to Parse the string since it is tested elsewhere but sadly fakes are only included in Enterprise version of VS 2015
        [TestMethod()]
        public void ParseStringTest()
        {
            List<string> words = new List<string>();

            foreach (string result in uniqueCharacterStringProcessor.ParseString("radar;wow6car'awesome!Automotive", @"[^a-zA-Z]"))
            {
                words.Add(result);
            }

            Assert.AreEqual(5, words.Count);

            // First result test
            Assert.AreEqual("r2r;", words[0]);

            // Second result test
            Assert.AreEqual("w1w6", words[1]);

            // Third result test
            Assert.AreEqual("c1r'", words[2]);

            // Fourth result test
            Assert.AreEqual("a5e!", words[3]);

            // Fifth result test
            Assert.AreEqual("A6e", words[4]);
        }
    }
}