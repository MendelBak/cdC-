using System;

class MainClass 
{
  public static void Main (string[] args) 
  {
    // oneTo255();
    fizzBuzz();
  }
  
  public static void oneTo255()
  {
    for(var i = 1; i <= 255; i++) 
    {
      Console.WriteLine(i);
    }
  }
  
  public static void fizzBuzz()
  {
    int three = 3;
    int five = 5;
    for(var i = 1; i <= 100; i++)
    {
      three--;
      five --;
      if(three == 0 && five == 0)
      {
        Console.WriteLine("FizzBuzz");
        three = 3;
        five = 5;
      }
      if(three == 0) 
      {
        Console.WriteLine(i + "Fizz");
        three = 3;
      }
      if(five == 0) 
      {
        Console.WriteLine("Buzz");
        five = 5;
      }
      
    }
  }
  
  
}