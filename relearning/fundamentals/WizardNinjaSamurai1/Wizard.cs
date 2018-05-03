using System;

namespace WizardNinjaSamurai1
{
    public class Wizard :Human
    {
        public Wizard(string name) :base(name)
        {
            this.name = name;
            this.health = 50;
            this.intelligence = 25;
        } 

        public void Heal()
        {
            this.health =+ this.intelligence * 10;
        }

        public void Fireball(object victim)
        {
            if(victim is Human)
            {
                Human humanVictim = victim as Human;
                Random rand = new Random();
                int damage = rand.Next(20, 51);
                System.Console.WriteLine($"{humanVictim.name} was attacked by {this.name} and lost {damage} HP!");
                humanVictim.health -= damage;
            }
            else
            {
                System.Console.WriteLine("You can only attack Humans with Fireball.");
            }
        }

    }
}