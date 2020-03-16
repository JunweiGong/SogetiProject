using Dapper;
using ManageHouse.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ManageHouse.Repository
{
    public class StageRepository : IStageRepository
    {
        private ILogger<StageRepository> _logger;
        private IConfiguration _configuration;
        private string _connection; // Connectionstring

        public StageRepository(IConfiguration configuration, ILogger<StageRepository> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _connection = _configuration.GetSection("ConnectionStrings").GetSection("HouseContext").Value;
        }

        public void Create(Stage stage)
        {
            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @"INSERT INTO [dbo].[Stages] ([Id],[Stage],[HouseId],[ImageId]) 
                                                      VALUES (@Id, @StageName, @HouseId, @ImageId)";
                    
                    con.Execute(query, stage);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error inserting stage", ex);
                    throw ex;
                }
            }
        }

        public void Delete(Stage stage)
        {
            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @"UPDATE [dbo].[Stages] SET [Stage] = @StageName, [HouseId] = @HouseId, [ImageId] = @ImageId WHERE Id = @Id";
                    con.Execute(query, stage);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Updating stage", ex);
                    throw ex;
                }
            }
        }

        public List<Stage> List(House house)
        {
            var list = new List<Stage>();

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @"SELECT TOP (1000) [Id] ,[Stage] as StageName,[HouseId],[ImageId],[Created],[Deleted] FROM [dbo].[Stages] WHERE [HouseId] = @Id";
                    list.AddRange(con.Query<Stage>(query, house));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error inserting stage", ex);
                    throw ex;
                }
            }

            return list;
        }

        public Stage Read(Guid id)
        {
            Stage stage;

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @"SELECT TOP (1000) [Id] ,[Stage] as StageName, [HouseId],[ImageId],[Created],[Deleted] FROM [dbo].[Stages] WHERE [Id] = @Id";
                    stage = con.QuerySingle<Stage>(query, new { Id = id });
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error inserting stage", ex);
                    throw ex;
                }
            }

            if (stage == null)
            {
                throw new Exception("Unable to read stage");
            }

            return stage;
        }

        public void Update(Stage stage)
        {
            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @"UPDATE [dbo].[Stages] SET [Stage] = @StageName, [HouseId] = @HouseId, [ImageId] = @ImageId WHERE Id = @Id";
                    con.Execute(query, stage);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Updating stage", ex);
                    throw ex;
                }
            }
        }
    }
}
