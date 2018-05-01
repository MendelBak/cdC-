using System;

namespace WizardNinjaSamurai1
{
    public class Samurai : Human
    {
        // This variables tracks the number of Samurai in existence.
        private static int numberOfSamurai = 0;


        public Samurai(string name) :base(name)
        {
            this.health = 200;
            numberOfSamurai++;
        }

        public void deathBlow(object victim)
        {
            if(victim is Human)
            {
                Human humanVictim = victim as Human;
                if(humanVictim.health < 50)
                {
                    System.Console.WriteLine($"{this.name} attacked {humanVictim.name} with Death Blow! {humanVictim.name} has died!");
                    humanVictim.health = 0;
                }
            }
        }

        public void Meditate()
        {
            System.Console.WriteLine($"{this.name} Meditated and now has full HP!");
            this.health = 200;
        }

        public static void HowManySamurai()
        {
            System.Console.WriteLine($"There are {numberOfSamurai} running around!");
        }
    }
}