using System;

namespace WizardNinjaSamurai1
{
    public class Ninja : Human
    {
        public Ninja(string name) :base(name)
        {
            dexterity = 175;
        }

        public void Steal(object victim)
        {
            if(victim is Human)
            {
                Human humanVictim = victim as Human;
                System.Console.WriteLine($"{this.name} used Steal on {humanVictim.name}! He regained 10 health.");
                this.health += 10;
            }
            else
            {
                System.Console.WriteLine("You can only use Steal on Humans!");
            }
        }

        public void getAway()
        {
            this.health -= 15;
            System.Console.WriteLine($"{this.name} got away but lost 15 HP!");
        }
    }
}