using System;
using System.Collections.Generic;

namespace puzzles
{
    class Program
    {

        public static int[] RandomArray()
        {
            // Create an empty array with 10 empty spots
            int[] RandArr = new int[10];
            Random rand = new Random();

            // Insert random values into every spot in the array
            for (int i = 0; i < 10; i++)
            {
                RandArr[i] = rand.Next(5, 25);
            }

            int max = RandArr[0];
            int min = RandArr[0];
            int sum = RandArr[0];

            for(int j = 1; j < RandArr.Length; j++)
            {
                sum += RandArr[j];
                if (RandArr[j] > max)
                {
                    max = RandArr[j];
                }
                if (RandArr[j] < min)
                {
                    min = RandArr[j];
                }
            }
                System.Console.WriteLine($"Max: {max}. Min: {min}. Sum: {sum}.");
                return RandArr;
        }

        public static string[] TossCoin(int attempts) 
        {
            Random rand = new Random();
            // Create empty array of string that will hold the results of each coin toss.
            string[] results = new string[attempts];
            double wins = 0;
            // Run rand and save the resultss to resultss array.
            for(int i = 0; i < attempts; i ++)
            {
                int temp = rand.Next(0, 2);
                if (temp == 1)
                {
                    results[i] = "Heads";
                    wins ++;
                }
                else 
                {
                    results[i] = "Tails";
                }
            }
            for(int j = 0; j < results.Length; j++)
            {
                System.Console.WriteLine($"Attempt #{j+1}: {results[j]}!");
            }
            System.Console.WriteLine($"Ratio of Wins to Losses: {wins/results.Length}");
            return results;
        }

        public static string[] Names() {
            string[] NamesArr = {"Menachem", "Berel", "Leibel", "Chaim", "Batyah", "Yankel", "Yisroel Dovid", "Aryeh Leib", "Dov Ber"};
            List<string> NamesList = new List<string>();

            // Only save names over 5 chars in list. List wil be converted to array later.
            foreach(string name in NamesArr)
            {
                if(name.Length > 5) 
                {
                    NamesList.Add(name);
                }
            }
            // Randomize order of names in the list.
            Random rand = new Random();
            for(int i = 0; i < NamesList.Count; i++)
            {
                int RandomIndex = rand.Next(0, NamesList.Count - 1);
                string temp = NamesList[i];
                NamesList[i] = NamesList[RandomIndex];
                NamesList[RandomIndex] = temp;
            }

            // Convert list to Array
            string[] NewNamesArr = NamesList.ToArray();

            // Console log and return result array.
            System.Console.WriteLine("Names that are longer than 5 characters:");
            foreach(string name in NewNamesArr)
                System.Console.WriteLine(name);
                return NewNamesArr;
        }




        static void Main(string[] args)
        {

        // RandomArray();
        // TossCoin(10);
        Names();




        }
    }
}
