using System;

namespace human1
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Bob = new Human("Bob");
            Human Tom = new Human("Tom");
            Bob.Attack(Tom);
        }
    }
}