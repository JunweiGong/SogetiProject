using Dapper;
using ManageHouse.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ManageHouse.Repository
{

    public interface IHouseRepository 
    {
        void Create(House house);

        House Read(Guid id);

        House Read(string objectId);
        
        void Update(House house);

        void Delete(House house);

        List<House> List();  
    }

    public class HouseRepository : IHouseRepository
    {
        private ILogger<HouseRepository> _logger;
        private IConfiguration _configuration;
        private string _connection; // Connectionstring

        public HouseRepository(IConfiguration configuration, ILogger<HouseRepository> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _connection = _configuration.GetSection("ConnectionStrings").GetSection("HouseContext").Value;
        }

        public void Create(House house)
        {
            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @"INSERT INTO[dbo].[Houses]
                                    ([Id],[Longitude],[Latitude],[Object],[ObjectDescription])
                                   VALUES (@Id, @Longitude, @Latitude, @Object, @ObjectDescription)";

                    con.Execute(query, house);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error inserting image", ex);
                    throw ex;
                }
            }
        }

        public void Delete(House house)
        {
            house.Deleted = DateTime.Now;

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @"UPDATE [dbo].[Houses] SET [Deleted] = @Deleted WHERE [Id] = @Id";

                    con.Execute(query, house);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error inserting image", ex);
                    throw ex;
                }
            }
        }

        public List<House> List()
        {
            var houses = new List<House>();

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @" SELECT TOP (1000) * FROM [dbo].[Houses] WHERE Deleted IS NULL";

                    var found = con.Query<House>(query);

                    houses.AddRange(found);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error listing images", ex);
                    throw ex;
                }
            }

            return houses;
        }

        public House Read(Guid id)
        {
            House house;

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @" SELECT TOP (1000) * FROM [dbo].[Houses] WHERE Id = @Id";

                    house = con.QuerySingle<House>(query, new { Id = id }); 
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error listing images", ex);
                    throw ex;
                }
            }

            if (house == null)
            {
                throw new Exception("Unable to fetch house");
            }

            return house;
        }

        public House Read(string objectId)
        {
            House house;

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @" SELECT TOP (1000) * FROM [dbo].[Houses] WHERE Object = @Id";

                    house = con.QuerySingle<House>(query, new { Id = objectId });
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error listing images", ex);
                    throw ex;
                }
            }

            if (house == null)
            {
                throw new Exception("Unable to fetch house");
            }

            return house;
        }

        public void Update(House house)
        {
            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @"UPDATE [dbo].[Houses]
                                    SET 
                                       [Latitude] = @Latitude,
                                       [Longitude] = @Longitude,
                                       [Object] = @Object,
                                       [ObjectDescription] = @ObjectDescription
                                    WHERE Id = @Id";

                    con.Execute(query, house);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error inserting image", ex);
                    throw ex;
                }
            }
        }
    }

}
