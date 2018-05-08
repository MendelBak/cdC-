using System;
using System.Collections.Generic;

namespace dojodachi
{


    public class dachi
    {
        public int fullness { get; set; }
        public int happiness { get; set; }
        public int energy { get; set; }
        public int meals { get; set; }
        public string actionMessage { get; set; }
        public bool winStatus { get; set; }
        public bool loseStatus { get; set; }

        // Constructor
        public dachi()
        {
            this.fullness = 20;
            this.happiness = 20;
            this.energy = 50;
            this.meals = 3;
            this.actionMessage = "Welcome to DojoDachi. Use the buttons below to play!";
        }

        public dachi Eat()
        {
            if (this.meals < 1)
            {
                this.actionMessage = "You cannot feed your pet if you don't have any Meals.";
                return this;
            }
            this.meals -= 1;
            Random rand = new Random();
            this.fullness += rand.Next(5, 11);
            this.actionMessage = $"You fed your dojodachi and added ${rand.Next(5, 11)}! -1 Meal";
            return this;
        }
    }


}