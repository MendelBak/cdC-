using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using DojoLeague.Models;

namespace DojoLeague.Factory
{
    public class NinjaFactory : IFactory<Ninja>
    {
        private string connectionString;
        public NinjaFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=dojoleague_db;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(connectionString);
            }
        }


        public void Add(Ninja ninja)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO trails (name, description, length, elevation_gain, longitude, latitude, created_at, updated_at) VALUES (@Name, @Description, @Length, @ElevationGain, @Longitude, @Latitude, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, ninja);
            }
        }

        
        public List<Ninja> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM trails").ToList();
            }
        }


        public Ninja FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM trails WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

    }
}