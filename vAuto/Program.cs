using InputProcessor;
using System;

namespace vAuto
{
    /// <summary>
    /// PP 1.5: 
    //  In the programming language of your choice, write a program that modifies a string using the following rules:
    //  1.  Each word in the input string is replaced with the following:  the first letter of the word, the count of distinct letters between 
    //      the first and last letter, and the last letter of the word. For example, "Automotive" would be replaced by "A6e".
    //  2.  A "word" is defined as a sequence of alphabetic characters, delimited by any non-alphabetic characters.
    //  3.  Any non-alphabetic character in the input string should appear in the output string in its original relative location.
    /// </summary>
    class Program
    {
        static void Main (string[] args)
        {
            string input = null;
            string delimiter = null;

            // Use if you would like to enter in your input/delimiter string
            //while (String.IsNullOrEmpty(input))
            //{
            //    Console.WriteLine("Please enter in the input string");
            //    input = Console.ReadLine();
            //}

            //while (String.IsNullOrEmpty(delimiter))
            //{
            //    Console.WriteLine("Please enter in the delimiter string");
            //    delimiter = Console.ReadLine();
            //}

            // Use if you would like to manually enter in your input/delimiter string
            input = "@=Auto;wow6car.4john$Automotive";
            delimiter = @"[^a-zA-Z]";

            UniqueCharacterStringProcessor processor = new UniqueCharacterStringProcessor(new RegularExpressionParser());

            Console.WriteLine("Input: " + input);
            Console.WriteLine("Delimiter: " + delimiter);
            Console.WriteLine("Result:");

            try
            {
                foreach (string result in processor.ParseString(input, delimiter))
                {
                    Console.Write(result);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Please press enter to end the program");
            Console.ReadLine();
        }
    }
}
