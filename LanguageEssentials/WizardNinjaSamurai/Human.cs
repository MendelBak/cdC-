namespace WizardNinjaSamurai
{
    public class Human
    {
        public string name;
        public int health { get; set; }
        public int strength {get; set; }
        public int intelligence {get; set; }
        public int dexterity {get; set; }

        public Human(string person)
        {
            name = person;
            health = 100;
            strength = 3;
            intelligence = 3;
            dexterity = 3;
        }
        public Human (string person, int hp, int s, int i, int d)
        {
            name = person;
            health = hp;
            strength = s;
            intelligence = i;
            dexterity = d;
        }

        public void Attack(object person)
        {
            if(person is Human)
            {
                Human enemy = person as Human;
                if(enemy == null)
                {
                    System.Console.WriteLine("Attack failed since enemy is null");
                }
                else 
                {
                    enemy.health -= strength * 5;
                    System.Console.WriteLine("{0} attacked {1} who lost {2} health!", name, enemy.name, strength * 5);
                    System.Console.WriteLine("{0}'s new total health is: {1}", enemy.name, enemy.health);
                }
            }
        }

    }
}