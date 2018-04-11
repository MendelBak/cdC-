namespace OOPIntro
{
    public class Vehicle
    {
        public int numPassenger = 4;
        public double distance = 0;

        public Vehicle(int val = 0) {
            numPassenger = val;
            distance = 0.0;
            System.Console.WriteLine("Your new vehicle can hold {0} passengers and has a starting mileage of {1}", numPassenger, distance);
        }
        public Vehicle(int val = 0, double startingMileage = 0.0) {
            numPassenger = val;
            distance = startingMileage;
            System.Console.WriteLine("Your used vehicle can hold {0} passengers and has a starting mileage of {1}", numPassenger, distance);
        }
        public void Move(double miles) {
            distance += miles;
        }
    }

    public class Car : Vehicle
    {
        public int NumWheels = 4;
        public string Condition;
        public Car() : base(5)
        {
            Condition = "New";
        }
        
        public Car(int passengers, double odo) : base(passengers, odo)
        {
            Condition = "Used";
        }
    }
}
