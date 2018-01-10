using System;

namespace human
{
    class Program
    {
        static void Main(string[] args)
        {
            Human newBob = new Human("Bob");
            System.Console.WriteLine("My Name is: " + newBob.Name);
            System.Console.WriteLine("{0}'s Strength is: {1}", newBob.Name, newBob.Strength);
            System.Console.WriteLine("{0}'s Intelligence is: {1}", newBob.Name,newBob.Intelligence);
            System.Console.WriteLine("{0}'s Dexterity is: {1}", newBob.Name,newBob.Dexterity);
            System.Console.WriteLine("{0}'s Health is: {1}", newBob.Name,newBob.Health);

            Human newWizard = new Human("Wizard Bob", 5, 5, 5, 100);
            System.Console.WriteLine("My Name is: " + newWizard.Name);
            System.Console.WriteLine("{0}'s Strength is: {1}", newWizard.Name, newWizard.Strength);
            System.Console.WriteLine("{0}'s Intelligence is: {1}", newWizard.Name,newWizard.Intelligence);
            System.Console.WriteLine("{0}'s Dexterity is: {1}", newWizard.Name,newWizard.Dexterity);
            System.Console.WriteLine("{0}'s Health is: {1}", newWizard.Name,newWizard.Health);

            newWizard.Attack(newBob);
            System.Console.WriteLine("My Name is: " + newBob.Name);
            System.Console.WriteLine("{0}'s Health is: {1}", newBob.Name,newBob.Health);
            System.Console.WriteLine("My Name is: " + newWizard.Name);
            System.Console.WriteLine("{0}'s Health is: {1}", newWizard.Name,newWizard.Health);

        }
    }
}
