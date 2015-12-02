using InputProcessor;
using System;

namespace vAuto
{
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
            input = "@=Auto;wow6car.4john$";
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
