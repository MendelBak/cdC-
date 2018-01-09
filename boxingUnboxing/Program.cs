using System;
using System.Collections.Generic;

namespace boxingUnboxing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> testList = new List<object>();
            testList.Add(7);
            testList.Add(28);
            testList.Add(-1);
            testList.Add(true);
            testList.Add("Chair");

            foreach(var entry in testList) {
            Console.WriteLine("Here is one of the values in the testList: " + entry);
            }

            int sum = 0;
            foreach(var entry in testList) {
                if(entry is int) {
                sum += (int)entry;
                }
            }
            Console.WriteLine("The sum of all the ints in the list is: " + sum);
        }
    }
}
