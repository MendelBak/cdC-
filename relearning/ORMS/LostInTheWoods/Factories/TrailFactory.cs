using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using LostInTheWoods.Models;

namespace LostInTheWoods.Factory
{
    public class TrailFactory : IFactory<Trails>
    {
        private string connectionString;
        public TrailFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=lostinthewoods;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(connectionString);
            }
        }

        // Method to get all trails from DB
        public IEnumerable<Trails> GetAllTrails()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trails>("SELECT * FROM trails");
            }
        }

        //Method to add a new trail to DB
        public void AddTrail(Trails trail)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string Query = "INSERT INTO trails (name, description, length, elevchange, latitude, longitude, created_at, updated_at) VALUES (@Name, @Description, @Length, @ElevChange, @Latitude, @Longitude, NOw(), NOW())";
                dbConnection.Open();
                // We pass our model to the Execute function in order for it to map the proper values to the correct fields based on the "@" symbols in the "Query" string built above. 
                dbConnection.Execute(Query, trail);
            }
        }

        //Method to get specific trail from DB
        public Trails GetOneTrail(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                // This line is gave me some trouble at first. I can't simply pass in the Id from the above parameter. Instead, I need to use the "@" Dapper symbol. However, since there is no model for the "@Id" to bind to, I need to create an anonymous object to have it bound to. (new { id = Id }).
                return dbConnection.Query<Trails>("SELECT * FROM trails WHERE id = @Id", new { id = Id}).SingleOrDefault();
            }
        }
    }


}

