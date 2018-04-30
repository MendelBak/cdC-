using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    collectionsPractice();
  }
  
  public static void collectionsPractice()
  {
    // Multiplication Multidimensional Arrays
    int[,] multiplicationArr = new int[10, 10];
    
    for(int i = 1; i < 11; i++) 
    {
      for(int j = 1; j < 11; j++)
      {
        multiplicationArr[i - 1, j - 1] = i * j;
      }
    }
    foreach(int num in multiplicationArr)
    {
      Console.WriteLine(num);
    }

    // Ice Cream Flavors
    List<string> iceCreamFlavors = new List<string>();
    iceCreamFlavors.Add("Vanilla");
    iceCreamFlavors.Add("Chocolate");
    iceCreamFlavors.Add("Strawberry");
    iceCreamFlavors.Add("Rocky Road");
    iceCreamFlavors.Add("Pecan");
    
    Dictionary<string, string> peopleDict = new Dictionary<string, string>();
    Random rand = new Random();
    foreach(string name in arr2)
    {
      peopleDict.Add(name, "null");
    }
    List<string> namesList = new List<string>(peopleDict.Keys);
    int randomInt;
    foreach(string name in namesList)
    {
      randomInt = rand.Next(1, 6);
      peopleDict[name] = iceCreamFlavors[randomInt];
    }
    foreach(KeyValuePair<string, string> person in peopleDict)
    {
      Console.WriteLine(person);
    }
  }
  
  
  
}