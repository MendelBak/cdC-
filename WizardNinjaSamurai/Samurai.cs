using System;

namespace WizardNinjaSamurai
{


    public class Samurai : Human
    {
        // Counter variable is used by the HowMany() method.
        public static int counter = 0;
        public Samurai(string name) : base(name)
        {
            health = 200;
            counter++;
        }

        public void DeathBlow(Human enemy)
        {
            if(enemy.health < 50)
            {
                enemy.health = 0;
                System.Console.WriteLine("{0} gave the gift of eternal sleep to {1}. Goodbye {2}. {3} Health: {4}", name, enemy.name, enemy.name, enemy.name, enemy.health);
            }
        }

        public void Meditate()
        {
            health = 200;
            System.Console.WriteLine("{0} ate some chocolate Chanukah coins while meditating and is now back to full health. Health: {1}", name, health);
        }

        public static void HowMany()
        {
            System.Console.WriteLine("There are currently {0} Samurai running around in their Fundoshi", counter);
        }
    }
}