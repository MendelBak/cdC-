using System;

namespace WizardNinjaSamurai
{
    public class Wizard : Human
    {
        public Wizard(string person) : base(person)
        {
            health = 50;
            intelligence = 25;
        }

        public void Heal() 
        {
                this.health += 10 * this.intelligence;
                System.Console.WriteLine("{0} was healed. Health is now {1}", name, health);
        }

        public void Fireball(object val)
        {
            if(val is Human)
            {
                // Unbox object
                Human enemy = val as Human;

                Random rand = new Random();
                int damage = rand.Next(20, 51); 
                enemy.health -= damage;
                System.Console.WriteLine("{0} was attacked by {1} and lost {2} health. His health is now {3}", enemy.name, name, damage, enemy.health);
            }
        }
    }
}