using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {
    // int[] myArr1 = {-1, -5, -34, 0, 1, 17, 77};
    // num1();
    // num2();
    // num3();
    // num4();
    // num5(myArr1);
    // num6(myArr1);
    // num7();
    // num8(myArr1, 5);
    // num9();
    // num10(myArr1);
    // num11(myArr1);
    // num12(myArr1);
    object[] myArrayOfObjects = {-1, -5, -34, 0, 1, 17, 77};
    num13(myArrayOfObjects);
    
  }
  
  public static void num1()
  {
   for(int i = 1; i <= 255; i++)
   {
     Console.WriteLine(i);
   }
  }
  
  private static void num2()
  {
    for(int i = 1; i < 255; i++)
    {
      if(i % 2 != 0)
      {
        Console.WriteLine(i);
      }
    }
  }
  static void num3()
  {

    int sum = 0;
    for(int i = 0; i < 255; i++)
    {
        sum += i;
        Console.WriteLine("Current Number:{0} Current Sum{1}", i, sum);
        
    }
  }
  
  static void num4() 
  {
    int[] myArr = {1,3,5,7,9};
    foreach(int num in myArr)
    {
      Console.WriteLine(num);
    }
  }
  
  public static void num5(int[] arr)
  {
    int max = arr[0];
    for(int i = 1; i < arr.Length; i++)
    {
      if(arr[i] > max)
      {
        max = arr[i];
      }
    }
    Console.WriteLine(max);
  }
  
  public static void num6(int[] arr)
  {
    int sum = arr[0];
    foreach(int num in arr)
    {
      sum += num;
    }
    Console.WriteLine(sum / arr.Length);
  }
  
  public static void num7()
  {
    List<int> oddsList = new List<int>();
    for(int i = 0; i <= 255; i++)
    {
      if(i % 2 != 0)
      {
        oddsList.Add(i);
      }
    }
    int[] oddsArr = oddsList.ToArray();
    foreach(int num in oddsArr)
    {
      Console.WriteLine(num);
    }
  }
  
  public static void num8(int[] arr, int y)
  {
    foreach(int num in arr)
    {
      if(num > y)
      {
        Console.WriteLine(num);
      }
    }
  }
  
  public static void num9()
  {
    int[] myArr2 = {1,3,5,7,-7,0};
    for(int i = 0; i < myArr2.Length; i++)
    {
      myArr2[i] *= myArr2[i];
    }
    foreach(int num in myArr2)
    {
      Console.WriteLine(num);
    }
  }
  
  public static void num10(int[] arr)
  {
    for(int i = 0; i < arr.Length; i++)
    {
      if(arr[i] < 0)
      {
        arr[i] = 0;
      }
    }
    
    foreach(int num in arr)
    {
      Console.WriteLine(num);
    }
  }
  
  public static void num11(int[] arr) 
  {
    int min = arr[0];
    int max = arr[0];
    int sum = arr[0];
    
    for(int i = 1; i < arr.Length; i++)
    {
      if(arr[i] < min)
      {
        min = arr[i];
      }
      if(arr[i] > max)
      {
        max = arr[i];
      }
      sum += arr[i];
    }
    Console.WriteLine("Min:{0} Max:{1} Avg{2}", min, max, (sum / arr.Length));
  }
  
  public static void num12(int[] arr)
  {
    for(int i = 0; i < arr.Length - 1; i++)
    {
      arr[i] = arr[i + 1];
    }
    arr[arr.Length - 1] = 0;
    foreach(int num in arr)
    {
      Console.WriteLine(num);
    }
  }
  public static void num13(object[] arr)
  {
    for(int i = 0; i < arr.Length; i++)
    {
      if((int)arr[i] < 0)
      {
        arr[i] = "Dojo";
      }
    }
    foreach(object item in arr)
    {
      Console.WriteLine(item);
    }
  }
  
}