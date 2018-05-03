namespace human1
{
    public class Human
    {
        private string name;
        public int strength = 3;
        public int intelligence = 3;
        public int dexterity = 3;
        public int health = 100; 

        public Human(string name)
        {
            this.name = name;
        }

        public Human(string name, int strength, int intelligence, int dexterity, int health)
        {
            this.name = name;
            this.strength = strength;
            this.intelligence = intelligence;
            this.dexterity = dexterity;
            this.health = health;
        }

        public object Attack(object victim)
        {
            if(victim is Human)
            {
                Human humanVictim = victim as Human;
                humanVictim.health -= (5 * this.strength);
                System.Console.WriteLine($"{humanVictim.name} was attacked by {this.name} and lost {5 * this.strength} hitpoints!");
                if(humanVictim.health <= 0)
                {
                    System.Console.WriteLine($"{humanVictim.name} was just slain!");
                    return humanVictim;
                }
                return humanVictim;
            }
            else
            {
                System.Console.WriteLine("Victim is not Human");
                return victim;
            }
        }
    }
}