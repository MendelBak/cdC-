using System;
using System.Collections.Generic;


namespace first_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Collection Assignment Q1
            int[] oneToNine = {0,1,2,3,4,5,6,7,8,9};
            foreach(int num in oneToNine) {
            Console.WriteLine(num);
            }

            // Collection Assignment Q2
            string[] namesArr = {"Tim", "Martin", "Niki", "Sara"};
            foreach(string name in namesArr) {
                Console.WriteLine(name);
            }
            
            // Collection Assignment Q3
            bool[] boolArr = new bool[10];
            for(int i = 0; i < boolArr.Length; i++) {
                if(i % 2 == 0) {
                    boolArr[i] = true;
                }
                else {
                    boolArr[i] = false;
                }
            }
            foreach(bool entry in boolArr) {
            Console.WriteLine(entry);
            }

            // Collection Assignment Q4
            int[,] multiplicationArr = new int [10, 10];
            for(int i = 1; i < 11; i++){
                for(int j = 1; j < 11; j++) {
                    multiplicationArr[i - 1, j -1] = i * j;
                }
            }
            foreach(int num in multiplicationArr) {
                Console.WriteLine(num);
            }

            // Collections Assignment Q5
            List<string> iceCreamFlavors = new List<string>() {
                "Vanilla", 
                "Chocolate", 
                "Strawberry", 
                "Neopolitan", 
                "Swirl", 
                "Mixed", 
                "MultiFlavored"
            };
            Console.WriteLine("Number of Ice Cream Flavors: " + iceCreamFlavors.Count);
            Console.WriteLine("Third flavor (to be removed) is: " + iceCreamFlavors[2]);
            iceCreamFlavors.RemoveAt(2);
            Console.WriteLine("New number of Ice Cream Flavors: " + iceCreamFlavors.Count);

            // Collection Assignment Q6
            Dictionary<string,string> users = new Dictionary<string,string>();
            foreach(string name in namesArr) {
            users.Add(name, null);
            }

            Random rand = new Random();
            foreach(string name in namesArr) {
                users[name] = iceCreamFlavors[rand.Next(0, iceCreamFlavors.Count)];
            }
            foreach(KeyValuePair<string,string> entry in users) {
            Console.WriteLine(entry);
            }



            // Dictionary Practice

            // Dictionary<string,string> profile = new Dictionary<string,string>();
            
            // profile.Add("Name", "Mendel");
            // profile.Add("Language", "Hebrew");
            // profile.Add("Location", "Eretz Yisroel");
            // foreach(KeyValuePair<string,string> entry in profile) {
            // Console.WriteLine(entry.Key + " " + entry.Value);
            // }
         


            // List Practice

            // List<string> bikes = new List<string>();
            // bikes.Add("Kawasaki");
            // bikes.Add("Triumph");
            // bikes.Add("BMW");
            // bikes.Add("Moto Guzzi");
            // bikes.Add("Harley Davidson");
            // bikes.Add("Suzuki");

            // Console.WriteLine("Total bike manufacturers", bikes.Count);
            // foreach(string x in bikes) {
            // Console.WriteLine("Manufacturer: " + x);
            // }


            // MultiDimensional Arrays

            // int [,] array2D = new int[3,2];
            // foreach(int x in array2D) {
            // Console.WriteLine(x);
            // }

            // int[,,] array3D = new int[2, 3, 4] {
            //    { {1, 2, 3, 4}, {5, 6, 7, 8}, {9, 10, 11, 12} },
            //    { {-1, -2, -3, -4}, {-5, -6, -7, -8}, {-9, -10, -11, -12} }
            // };
            // foreach(int x in array3D) {
            // Console.WriteLine(x);
            // }
            // Console.WriteLine(array3D[1, 1, 2]);


            // Working with Arrays

            // int[] numArray = new int[5];
            // numArray.SetValue(613, 0);

            // foreach( int x in numArray) {
            // Console.WriteLine(x);
            // }

            // int[] numArray2 = {1,2,3,4,5,6};
            // Console.WriteLine("numArray2");

            // string[] strArray =  {"Magpul", "Smith and Wesson", "M&P", "IWS"};


            // for( int idx = 0; idx < numArray2.Length -1; idx++) {
            //     Console.WriteLine("My favorite manufacturer is now " + strArray[idx]);
            // }
            // foreach(int x in numArray2) {
            // Console.WriteLine(x);
            // }



            // Fizz Buzz function

            // for (int i = 1; i < 256; i++)
            // {
            //     Console.WriteLine(i);
            // }

            // for (int i = 1; i < 101; i++)
            // {
            //     if (i % 3 == 0 && i % 5 == 0)
            //     {
            //         Console.WriteLine(i);
            //     }
            // }

            // for (int i = 1; i < 101; i++) {
            //     List<string> fizzBuzzArr = new List<string>();
            //     if (i % 3 == 0)
            //         fizzBuzzArr.Add(i + " Fizz");
            //     {
            //     }
            //     if (i % 5 == 0)
            //     {
            //         fizzBuzzArr.Add(i + " Buzz");
            //     }
            //     foreach(string result in fizzBuzzArr) {
            //     Console.WriteLine(result);
            //     }
            // }

        }
    }
}
