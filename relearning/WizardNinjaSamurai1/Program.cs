using System;

namespace WizardNinjaSamurai1
{
    class Program
    {
        static void Main(string[] args)
        {
            Wizard wizard1 = new Wizard("Mendel");
            Ninja ninja1 = new Ninja("Dennis");
            Samurai samurai1 = new Samurai("Michael");

            wizard1.attack(ninja1);
            ninja1.getAway();
            wizard1.Fireball(samurai1);
            wizard1.Heal();
            ninja1.Steal(wizard1);
            samurai1.Meditate();
            wizard1.attack(samurai1);
            wizard1.attack(samurai1);
            wizard1.attack(samurai1);
            
        }
    }
}
