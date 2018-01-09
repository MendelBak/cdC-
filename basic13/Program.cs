using System;
using System.Collections.Generic;

namespace basic13
{
    class Program
    {

        // Print 1-255
        public static void print2255()
        {
            for (int i = 1; i < 256; i++)
            {
                System.Console.WriteLine(i);
            }
        }


        // Print odds 1-255
        public static void printOdds255()
        {
            for (int i = 1; i < 256; i += 2)
            {
                System.Console.WriteLine(i);
            }
        }
        // You can also do an if check with a modulo while iterating through each number (not skipping by 2) to verify that it's an odd num.


        // Print sum of numbers that have printed so far from 0-255
        public static void printSum255()
        {
            int sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += i;
                System.Console.WriteLine("Current number " + i);
                System.Console.WriteLine("Total of all printed numers so far " + sum);
            }
        }


        // Iterate through Array
        public static void iterArr(int[] myarr)
        {
            foreach (int num in myarr)
            {
                System.Console.WriteLine(num);
            }
        }

        // Find Max in array
        public static void findMax(int[] myArr)
        {
            int max = myArr[0];
            for (int i = 1; i < myArr.Length; i++)
            {
                if (myArr[i] > max)
                {
                    max = myArr[i];
                }
            }
        System.Console.WriteLine($"The max num in the array is {max}");
        }


        // Get Average from Array
        public static void getAvg(int[] myArr)
        {
            int sum = myArr[0];
            int counter = 1;
            for (int i = 1; i < myArr.Length; i++)
            {
                sum += myArr[i];
                counter++;
            }
            System.Console.WriteLine("The avg sum of the array is: " + sum / counter);
        }

        //  Create an array with all odds from 1-255 
        public static void arrayOdds() {
            // Create list which will be converted to an array later, since arrays must be hardcoded in regards to their length.
            List<int> myList = new List<int>();

            for(int i = 1; i < 256; i++) {
                if(i % 2 != 0) {
                    myList.Add(i);
                }
            }
            int[] myArr = myList.ToArray();
            foreach(int x in myArr) {
                System.Console.WriteLine(x);
            }
        }
        // Takes an array and Y and returns number larger than Y
        public static void greaterThanY(int[] myArr, int y) {
            int result = y;
            foreach(int x in myArr) 
            {
                if(x > y) 
                {
                    result = x;
                }
            }
            System.Console.WriteLine("This is the number in the array that is larger than Y, (or is Y itself): {0}", result);
        }

        // Squares all values in given array 
        public static void squareArr(int[] myArr) {
            for(int x = 0; x < myArr.Length; x++){
                myArr[x] *= myArr[x];
            }
            System.Console.WriteLine("This is the array with all its values squared");
            foreach(int x in myArr){
            System.Console.WriteLine(x);
            }
        }

        // Replaces negatives in a given array with zeros
        public static void noNegNums(int[] myArr) {
            for(int i = 0; i < myArr.Length; i++)
            {
                if(myArr[i] < 0) 
                {
                    myArr[i] = 0;
                }
            }
            System.Console.WriteLine("Returning array after removing all negative values");
            foreach(int value in myArr){
                System.Console.WriteLine(value);
            }
        }


        // Finds Min, Max, anf Avg in a given array
        public static void minMaxAvg(int[] myArr) 
        {
            int min = myArr[0];
            int max = myArr[0];
            int avg = myArr[0];
            for(int i = 1; i < myArr.Length; i ++) 
            {
                avg += myArr[i];

                if(myArr[i] < min) 
                {
                    min = myArr[i];
                }
                if(myArr[i] > max)
                {
                    max = myArr[i];
                }
            }
            System.Console.WriteLine("Max: {0}. Min: {1}. Avg: {2}.", max, min, avg/myArr.Length);
        }


        // Shift values in array frontwards
        public static void shiftValsFront(int[] myArr) {
            for(int i = 0; i < myArr.Length - 1; i++) 
            {
                myArr[i] = myArr[i + 1];
            }
            myArr[myArr.Length-1] = 0;
            System.Console.WriteLine("Here is your updated array with all the values shifted to the front and a zero added to the end");
            foreach(int value in myArr) 
            {
                System.Console.WriteLine(value);
            }
        }


        public static void numberToString(object[] myArr) {
            for(int i = 0; i < myArr.Length; i++) 
            {
                if((int)myArr[i] < 0) 
                {
                    myArr[i] = "Dojo";
                }
            }
            foreach(object x in myArr) 
            {
                System.Console.WriteLine(x);
            }
        }









        static void Main(string[] args)
        {

            // Print 1-255 function call
            //  print2255();

            // Print odds 1-255 function call
            //  printOdds255();

            // Print sum of numbers that have printed so far from 0-255 function call
            // printSum255();

            // Iterate through Array function call
            // iterArr(new int[] { 1, 3, 5, 7, 9, 13 });


            // Find Max in array function call
            // int[] myArr = { -5, -7, -1, 770, 0, -3 };
            // findMax(myArr);


            // Get Average from Array function call
            // int[] avgArr = {2, 10, 3}; // This line is optional and allows me to pass in the array as a variable to the function call argument.
            // getAvg(new int[] { 2, 10, 3, 10, 10});


            //  Create an array with all odds from 1-255 function call
            // arrayOdds();


            // Takes an array and Y and returns number larger than Y: Function call.
            // int[] myArr = {1, 3, 7, 8, 13, -4, -7, 0, 3};
            // int y = 7; 
            // greaterThanY(myArr, y);


            // Squares all values in given array: Function Call.
            // int[] myArr = {1, 3, 7, 613, 770};
            // squareArr(myArr);


            // Replaces negatives in a given array with zeros: Function Call.
            // int[] myArr = {-1, 770, -613, -306, 306};
            // noNegNums(myArr);


            // Finds Min, Max, and Avg in a given array: Function Call.
            // minMaxAvg(new int[] {1, 3, 5, 7, 9, 614, -614, 306});


            // Shift values in array frontwards: Function Call.
            // shiftValsFront(new int[] {1, 2, 3, 4, 5});


            // Negative number to String: Function Call.
            numberToString(new object[] {1, 3, -5, -2, 7, 13});







        }

    }



}
