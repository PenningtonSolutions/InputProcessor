using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputProcessor
{
    public class ProcessedWord
    {
        // Word parsed off of the input string
        public string word { get; set; }
        // Delimiter used to parse string from input string
        public string delimiter { get; set; }

        /// <summary>
        /// Container used to house the parsed words and delimiters found to parse it from main input string
        /// </summary>
        /// <param name="word"></param>
        /// <param name="delimiter"></param>
        public ProcessedWord(string word, string delimiter)
        {
            this.word = word;
            this.delimiter = delimiter;
        }
    }
}
