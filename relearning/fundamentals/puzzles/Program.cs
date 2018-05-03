using System;

namespace puzzles
{
    class Program
    {
        static void Main(string[] args)
        {
            // randomArray();
            // coinToss();
            // tossMultipleCoins(7);
            names();
        }

        static void names()
        {
            Random rand = new Random();
            string[] myNamesArr = {"Mendel", "Michael", "Garret", "Adam", "Dennis", "Liz"};
            int count = myNamesArr.Length;
            string temp;
            int randomInt1;
            int randomInt2;
            for(int i = 0; i < count; i++)
            {
                // Create two random ints
                randomInt1 = rand.Next(0, myNamesArr.Length);
                randomInt2 = rand.Next(0, myNamesArr.Length);
                // Shuffle the names
                temp = myNamesArr[randomInt1];
                myNamesArr[randomInt1] = myNamesArr[randomInt2]; 
                myNamesArr[randomInt2] = temp;
            }
            foreach(string name in myNamesArr)
            {
                System.Console.WriteLine(name);
            }
        }

        static void coinToss()
        {
            Random rand = new Random();
            System.Console.WriteLine("Tossing a coin!");
            int tossResult = rand.Next(0, 2);
            if(tossResult == 1)
            {
                System.Console.WriteLine("Heads!");
            }
            else
            {
                System.Console.WriteLine("Tails!");
            }
        }

        static double tossMultipleCoins(int tosses)
        {
            Random rand = new Random();
            int totalHeadWins = 0;
            int currentTossResult;
            for(int i = 0; i <= tosses; i++)
            {
                currentTossResult = rand.Next(0, 2);
                if(currentTossResult == 1)
                {
                    System.Console.WriteLine("Heads!");
                    totalHeadWins++;
                }
                else
                {
                    System.Console.WriteLine("Tails!");
                }
            }
            System.Console.WriteLine($"Ratio of Heads wins over Tails: {(double)totalHeadWins / tosses}");
            return totalHeadWins / tosses;
        }

        static void randomArray()
        {
            int[] myArr = new int[10];
            Random rand = new Random();
            for(int i = 0; i < 10; i++)
            {
                myArr[i] = rand.Next(5, 25);
            }

            // I figured out how to find the index while using a foreach loop!
            int idx = 0;
            int min = myArr[0];
            int max = myArr[0];
            int sum = 0;
            foreach(int num in myArr)
            {
                if(num < min)
                {
                    min = myArr[idx];
                }
                if(num > max)
                {
                    max = myArr[idx];
                }
                idx++;
                sum+= num;
            }
            System.Console.WriteLine($"Max:{max} Min:{min} Sum:{sum}");
        }
    }
}
