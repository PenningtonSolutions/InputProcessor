using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputProcessor
{
    public class UniqueCharacterStringProcessor
    {
        private IParser parser;

        public UniqueCharacterStringProcessor(IParser parser)
        {
            this.parser = parser;
        }

        /// <summary>
        /// Takes input string and delimiter and after parsing string based upon passed in delimiter, it returns the first character or the parsed substring
        /// the number of unique characters between the first and last character, the last character of the parsed subtring, 
        /// and the delimiter found to split this substring
        /// 
        /// Returns "0" if the input string is null or empty
        /// 
        /// Returns first character of input string, number of unique characters, and the last character of input string if delimiter string is null
        /// </summary>
        /// <param name="input"></param>The input string to parse and compute
        /// <param name="delimiter"></param>The delimiters used to parse the string
        /// <returns></returns>
        public IEnumerable<string> ParseString(string input, string delimiter)
        {
            // If input string is null or empty then simply return "0" since there are no first or last characters and 0 is the number of unique characters in string
            if (String.IsNullOrEmpty(input))
            {
                yield return "0";
            }
            // If delimiter string is null or empty then simply compute unique characters and build output string with first char of input, number of unique characters,
            // and last character of input string since we have nothing to parse the input string with
            else if (String.IsNullOrEmpty(delimiter))
            {
                yield return input.First() + CountUniqueChars(input).ToString() + input.Last();
            }
            // Parse string and comput results as detailed above
            else
            {
                foreach (ProcessedWord word in parser.ParseString(input, delimiter))
                {
                    // If resulting word is null or empty then there are no characters to count or place for result
                    // This would occur if there are two delimiters next to each other or if a delimiter character is the first character of the input string
                    if (string.IsNullOrEmpty(word.word))
                    {
                        yield return "0" + word.delimiter;
                    }
                    // Compute the resulting oupt based upon details given above
                    else
                    {
                        yield return word.word.First() + CountUniqueChars(word.word).ToString() + word.word.Last() + word.delimiter;
                    }
                }
            }
        }

        /// <summary>
        /// strips the first and last character from the word and returns the count of unique characters from the string
        /// </summary>
        /// <param name="word"></param> String to compute
        /// <returns></returns>
        private int CountUniqueChars(string word)
        {
            if (word.Length <= 2)
            {
                return 0;
            }
            else
            {
                return word.Skip(1).Take(word.Length - 2).Distinct().Count();
            }
        }
    }
}
