using System;

namespace WizardNinjaSamurai
{

    public class Ninja : Human
    {
        public Ninja(string name) : base(name)
        {
            dexterity = 175;
        }

        public void Steal(Human enemy)
        {
            enemy.health -= 10;
            health += 10;
            System.Console.WriteLine("{0} Stole 10 health from {1} whose health is now {2}. {3}'s health increased to {4}.", name, enemy.name, enemy.health, name, health);
        }

        public void GetAway()
        {
            health -= 15;
            System.Console.WriteLine("{0} Used GetAway and lost 15 health while escaping.", name);
        }

    }

}