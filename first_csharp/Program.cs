using System;
using System.Collections.Generic;


namespace first_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 256; i++)
            {
                Console.WriteLine(i);
            }

            for (int i = 1; i < 101; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine(i);
                }
            }

            for (int i = 1; i < 101; i++) {
                List<string> fizzBuzzArr = new List<string>();
                if (i % 3 == 0)
                    fizzBuzzArr.Add(i + " Fizz");
                {
                }
                if (i % 5 == 0)
                {
                    fizzBuzzArr.Add(i + " Buzz");
                }
                foreach(string result in fizzBuzzArr) {
                Console.WriteLine(result);
                }
            }

        }
    }
}
