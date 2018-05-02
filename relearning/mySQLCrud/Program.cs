using System;
using DbConnection;

namespace mySQLCrud
{

    class Program
    {
        static void Main(string[] args)
        {
            // getAllUsers();
            // createUser();
            // updateUser();
            deleteUser();
        }

        public static void getAllUsers()
        {
            var allUsers = DbConnector.Query("SELECT * FROM users");
            System.Console.WriteLine($"Here are all the users in the 'users' Database table.");
            foreach (var user in allUsers)
            {
                System.Console.WriteLine($"{user["id"]} {user["first_name"]} {user["last_name"]} - {user["created_at"]} - {user["updated_at"]}");
            }
        }

        public static void createUser()
        {
            Console.WriteLine("What's your first name?");
            string fname = Console.ReadLine();
            string fNameString = '"' + fname + '"';
            Console.WriteLine("What's your last name?");
            string lname = Console.ReadLine();
            string lNameString = '"' + lname + '"';
            DbConnector.Execute($"INSERT INTO users(first_name, last_name, created_at, updated_at) VALUES({fNameString}, {lNameString}, NOW(), NOW())");
            System.Console.WriteLine("Successfully inserted a new user into the DB");
            getAllUsers();
        }

        public static void updateUser()
        {
            System.Console.WriteLine("Here are all the users currently in the DB.");
            getAllUsers();
            System.Console.WriteLine("Type the ID of the user you want to update.");
            int userId = Int32.Parse(Console.ReadLine());
            System.Console.WriteLine("What do you want to change the first name to?");
            string fname = Console.ReadLine();
            string fNameString = '"' + fname + '"';
            System.Console.WriteLine("What do you want to change the last name to?");
            string lname = Console.ReadLine();
            string lNameString = '"' + lname + '"';
            string updatedAtString = "NOW()";
            DbConnector.Execute($"UPDATE users SET first_name = {fNameString},  last_name = {lNameString}, updated_at = {updatedAtString} WHERE id = {userId}");
            System.Console.WriteLine("Success!");
        }

        public static void deleteUser()
        {
            System.Console.WriteLine("Here are all the users currently in the DB.");
            getAllUsers();
            System.Console.WriteLine("Type the ID of the user you want to destroy.");
            int userId = Int32.Parse(Console.ReadLine());
            DbConnector.Execute($"DELETE FROM users WHERE id = {userId}");
            System.Console.WriteLine("User Deleted.");
        }
    }
}
