using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InputProcessor
{
    public class RegularExpressionParser : IParser
    {
        /// <summary>
        /// Parses the given string based upon the supplied regular expression delimiter
        /// Returns a list of ProcessedWords which contain both the word found and the delimiter found to split it off of the main input string
        /// </summary>
        /// <param name="input"></param> Input string used to parse from
        /// <param name="delimiter"></param> Regular Expression delimiter string used to parse input with
        /// <returns></returns>
        public IEnumerable<ProcessedWord> ParseString(string input, string delimiter)
        {
            Regex regex;
            Match m;

            // Try to create a new Regex based upon the passed in delimiter string
            try
            {
                regex = new Regex(delimiter);
            }
            catch (ArgumentNullException e)
            {
                throw e;
            }
            catch (ArgumentException e)
            {
                throw e;
            }

            // Find the first match with the given input
            try
            {
                m = regex.Match(input);
            }
            catch (ArgumentNullException e)
            {
                throw e;
            }
            catch (RegexMatchTimeoutException e)
            {
                throw e;
            }

            int previousWordLocation = 0;
            int currentWordLocation = 0;

            // Continue to find new words
            while (m != null && m.Success)
            {
                // setting end of current word
                currentWordLocation = m.Index;

                // case where delimiter is the first character
                // case where the delimiter is next to the previou delimiter
                if ((currentWordLocation - previousWordLocation) <= 1)
                {
                    yield return new ProcessedWord(string.Empty, m.Value);
                }
                // case where the delimiter is after a "word"
                else
                {
                    // need to grab the next "word"
                    yield return new ProcessedWord(input.Substring(previousWordLocation, (currentWordLocation - previousWordLocation)), m.Value);
                }

                // setting spot for previous word to next location for next loop through
                previousWordLocation = currentWordLocation + 1;
                m = m.NextMatch();
            }

            // need to grab the word at the end if the input string does not end with a delimiter
            if (previousWordLocation < input.Length)
            {
                yield return new ProcessedWord(input.Substring(previousWordLocation), string.Empty);
            }
        }
    }
}
