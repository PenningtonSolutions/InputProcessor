using System.Collections.Generic;

namespace InputProcessor
{
    public interface IParser
    {
        /// <summary>
        /// Method to be definied to be used to parse input using the list of delimiters supplied
        /// </summary>
        /// <param name="input"></param> Input string to parse
        /// <param name="delimiter"></param> List of delimiters used to parse the input string
        /// <returns></returns>
        IEnumerable<ProcessedWord> ParseString(string input, string delimiter);
    }
}
