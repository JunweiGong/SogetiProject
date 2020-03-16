using Dapper;
using ManageHouse.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ManageHouse.Repository
{
    public class HouseRepository : IHouseRepository
    {
        IConfiguration _configuration;
        public HouseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Add(House house)
        {
            string connectionString = this.GetConnectionString();
            var count = 0;
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO House(Id, Name, Owner, Description) VALUES(@Id, @Name, @Owner, @Description);";
                    count = con.Execute(query, house);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public int Delete(int id)
        {
            string connectionString = this.GetConnectionString();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM House WHERE Id =" + id; // NOTE: (kbg) Should be @Parameter to be safe
                    count = con.Execute(query);
                }
                catch (Exception ex)
                {
                    throw ex;  // NOTE: (kbg) Should be logged 
                }
                return count;
            }
        }

        public int Update(House house) // NOTE: (kbg) Naming suggest UPDATE 
        {
            string connectionString = this.GetConnectionString();
            var count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE House SET Name = @Name, Owner = @Owner, Description = @Description WHERE Id = @Id";
                    count = con.Execute(query, house);
                }
                catch (Exception ex)
                {
                    throw ex; // NOTE: (kbg) Logging
                }
                return count;
            }
        }

        public House GetHouse(int id)
        {
            string connectionString = this.GetConnectionString();
            var house = new House();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM House WHERE Id =" + id;
                    house = con.Query<House>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return house;
            }
        }

        public List<House> GetList()
        {
            string connectionString = this.GetConnectionString(); // NOTE: (kbg) See below. For readability what type is connectionString?
            var houses = new List<House>(); // NOTE: (kbg) When declaring variables we usually use var when you see the type on the right side 

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM House";
                    houses = con.Query<House>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return houses;
            }
        }

        public List<int> GetStage(House house)
        {
            string connectionString = this.GetConnectionString(); 
            var stages = new List<int>(); 

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT DISTINCT Stage FROM Image WHERE House_id = @Id ORDER BY Stage";
                    stages = con.Query<int>(query,house).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return stages;
            }
        }
        // .     not return a connection
        public string GetConnectionString()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("HouseContext").Value;
            return connection;
        }
    }
}
