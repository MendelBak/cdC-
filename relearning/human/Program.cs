using System;

namespace human
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Mendel = new Human("Mendel");
            Human Bob = new Human("Bob");

            Mendel.Attack(Bob);
        }   
 
        public class Human
        {
            public string name;
            public int intelligence = 3;
            public  int dexterity = 3; 
            public int strength = 3;
            public int health = 100; 

            public Human(string newName)
            {
                name = newName;

                System.Console.WriteLine($"Welcome to life, {name}! Here are your starting stats: Name:{name} Intelligence:{intelligence} Dexterity:{dexterity} Strength:{strength} Health:{health}");
            }
            public Human(string newName, int newIntelligence, int newDexterity, int newStrength, int newHealth)
            {
                name = newName;
                int intelligence = newIntelligence;
                int dexterity = newDexterity;
                int strength = newStrength;
                int health = newHealth;

                System.Console.WriteLine($"Welcome to life, {name}! Here are your starting stats: Name:{name} Intelligence:{intelligence} Dexterity:{dexterity} Strength:{strength} Health:{health}");
            }

            public void Attack(object potentialVictim)
            {
                if (potentialVictim is Human)
                {
                    Human victim = potentialVictim as Human;
                    victim.health -= (this.strength * 5);
                    System.Console.WriteLine($"{victim.name} was attacked by {this.name} and suffered {this.strength * 5} damage!");
                    System.Console.WriteLine($"{victim.name}'s health is now {victim.health}.");

                } 
                else  
                {
                    System.Console.WriteLine("Your victim is not human. Please only attack humans...");
                }
            }
        }
            
    }
}
