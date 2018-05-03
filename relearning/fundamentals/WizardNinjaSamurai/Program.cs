using System;

namespace WizardNinjaSamurai
{
    class Program
    {
        static void Main(string[] args)
        {
            Wizard WizardMendel = new Wizard("WizardMendel");

            WizardMendel.Heal(WizardMendel);
        }

        public class Wizard
        {
            public string name;
            public int health = 50;
            public int intelligence = 25;

            public Wizard(string newName)
            {
                name = newName;
                System.Console.WriteLine($"Welcome to Earth, Wizard! Your new name is {name}!");
            }

            public void Heal(object character)
            {
                if (character is Wizard)
                {
                    Wizard BeingHealed = character as Wizard;
                    BeingHealed.health += (BeingHealed.intelligence * 10);
                    System.Console.WriteLine($"{BeingHealed.name} cast Heal and increased their health by {BeingHealed.intelligence * 10} Health!");
                    System.Console.WriteLine($"{BeingHealed.name}'s Heath is now {BeingHealed.health}");
                }
                else
                {
                    System.Console.WriteLine("Heal can only be used on a Wizard.");
                }
            }

            public void Fireball(object character)
            {
                if (this.e == Program.Wizard)
                {
                    Wizard Victim = character as Wizard;
                    Random rand = new Random();
                    int Damage = rand.Next(20, 50);
                }
                else
                {
                    System.Console.WriteLine("Fireball can only be used by a Wizard.");
                }

            }
        }
    }
}
