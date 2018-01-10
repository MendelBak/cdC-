using System;
using System.Collections.Generic;

namespace WizardNinjaSamurai
{
    class Program
    {
        static void Main(string[] args)
        {
            Human newHuman = new Human("Mendel");
            // System.Console.WriteLine("Your name is: {0}", newHuman.name);
            // System.Console.WriteLine("{0}'s health is: {1}", newHuman.name, newHuman.health);
            // System.Console.WriteLine("{0}'s strength is: {1}", newHuman.name, newHuman.strength);
            // System.Console.WriteLine("{0}'s intelligence is: {1}", newHuman.name, newHuman.intelligence);
            // System.Console.WriteLine("{0}'s dexterity is: {1}", newHuman.name, newHuman.dexterity);

            Wizard Gandalf = new Wizard("Gandalf");
            // Gandalf.Heal();
            Gandalf.Fireball(Gandalf);
            Gandalf.Fireball(Gandalf);

            Ninja Thomas = new Ninja("Thomas");
            // Thomas.Steal(Gandalf);
            // Thomas.GetAway();

            Samurai Jordan = new Samurai("Jordan");
            Jordan.DeathBlow(Gandalf);
            Jordan.Meditate();
            Samurai.HowMany();
        }
    }
}
