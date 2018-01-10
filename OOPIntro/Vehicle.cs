namespace OOPIntro
{
    public class Vehicle
    {
        public int numPassenger = 4;
        public double distance = 0;

        public Vehicle(int val = 0) {
            numPassenger = val;
            distance = 0.0;
        }

        public void Move(double miles) {
            distance += miles;
        }

    }
}
