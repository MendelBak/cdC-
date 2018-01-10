namespace human
{
    public class Human 
    {
        public string Name = "Mendel";
        public int Strength = 3;
        public int Intelligence = 3;
        public int Dexterity = 3;
        public int Health = 100;

        public Human(string newName)
        {
            Name = newName;     
            Name = newName;     
        }
        public Human (string newName = "Mendel", int newStrength = 3, int newIntelligence = 3, int newDexterity = 3, int newHealth = 100) 
        {
                Name = newName;     
                Strength = newStrength;
                Intelligence = newIntelligence;
                Dexterity = newDexterity;
                Health = newHealth;
        }

        public void Attack(Human enemy) 
        {
            if(enemy is Human)
            {
            enemy.Health -= Strength * 5;
            }
            else 
            {
                System.Console.WriteLine("You are not using a Human object to attack. Please make sure to only attack using a Human (bureaucracy rules; clearly not Krav Maga).");
            }
        }

    }   

}
