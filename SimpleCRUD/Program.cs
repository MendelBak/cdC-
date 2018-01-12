using System;
using DbConnection;


namespace SimpleCRUD
{
    class Program
    {

        public static void UserInfo()
        {
            var allUsers = DbConnector.Query("SELECT * FROM users;");
            System.Console.WriteLine("Here is the info on all the users currently in your DB.");
            foreach(var user in allUsers)
            {
                System.Console.WriteLine($"{user["id"]} {user["first_name"]} {user["last_name"]} {user["favorite_number"]} {user["created_at"]} {user["updated_at"]}");
            }
        }

        public static void CreateUser() 
        {
            System.Console.WriteLine("Let's create a new user! Please give me the data you'd like me to insert into the DB. Let's start with your First Name: ");
            string FirstName = Console.ReadLine();
            string FirstNameSQL = '"' + FirstName + '"';
            System.Console.WriteLine("What is your last name?");
            string LastName = Console.ReadLine();
            string LastNameSQL = '"' + LastName + '"';
            System.Console.WriteLine("What is your favorite number?");
            int FavNumber = Convert.ToInt32(Console.ReadLine());
            var NowTime = "NOW()";
            // Insert new user into DB.
            DbConnector.Execute($"INSERT INTO users (first_name, last_name, favorite_number, created_at, updated_at) VALUES ({FirstNameSQL}, {LastNameSQL}, {FavNumber}, {NowTime}, {NowTime})");
            // Display newly created user.
            UserInfo();
        }

        public static void UpdateUser() 
        {
            UserInfo();
            System.Console.WriteLine("What's the ID of the user that you'd like to update?");
            int UserID = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("What is your new first name?");
            string FirstName = Console.ReadLine();
            string FirstNameSQL = '"' + FirstName + '"';
            System.Console.WriteLine("What is your new last name?");
            string LastName = Console.ReadLine();
            string LastNameSQL = '"' + LastName + '"';
            System.Console.WriteLine("What is your new favorite number?");
            int FavNumber = Convert.ToInt32(Console.ReadLine());
            var NowTime = "NOW()";
            DbConnector.Execute($"UPDATE users SET first_name = {FirstNameSQL}, last_name = {LastNameSQL}, favorite_number = {FavNumber}, updated_at = {NowTime} WHERE id = {UserID}");
            System.Console.WriteLine("User has been succesfully updated.");
            UserInfo();
        }

        public static void DeleteUser()
        {
            System.Console.WriteLine("What's the ID of the user that you'd like to delete?");
            int UserID = Convert.ToInt32(Console.ReadLine());
            DbConnector.Execute($"Delete FROM users WHERE id = {UserID}");
            System.Console.WriteLine("User has been deleted from the DB");
            UserInfo();

        }
            

        static void Main(string[] args)
        {
            // UserInfo();
            CreateUser();
            // UpdateUser();
            // DeleteUser();
            
            

        }
    }
}
