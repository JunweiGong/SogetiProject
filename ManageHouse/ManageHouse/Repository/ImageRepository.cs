using Dapper;
using ManageHouse.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ManageHouse.Repository
{
    public class ImageRepository : IImageRepository
    {
        private ILogger<ImageRepository> _logger;
        private IConfiguration _configuration;
        private string _connection; // Connectionstring

        public ImageRepository(IConfiguration configuration, ILogger<ImageRepository> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _connection = _configuration.GetSection("ConnectionStrings").GetSection("HouseContext").Value;
        }
        

        public void Create(Image image)
        {
            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO[dbo].[Images] ([Id], [URI]) VALUES (@Id, @URI)";
                    con.Execute(query, image);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error inserting image", ex);
                    throw ex;
                }
            }
        }

        public void Delete(Image image)
        {
            image.Deleted = DateTime.Now;

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE [dbo].[Images] SET [Deleted] = @Deleted WHERE [Id] = @Id";
                    con.Execute(query, image);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error deleting image", ex);
                    throw ex;
                }
            }
        }

        public List<Image> List(string objectId, string stageId)
        {
            var imageList = new List<Image>();

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @" SELECT TOP (1000) Images.* 
                                    FROM
                                    [dbo].[Stages] INNER JOIN Images on Stages.ImageId = Images.Id
                                                   INNER JOIN Houses on Stages.HouseId = Houses.Id
                                    WHERE Stages.Stage = @Stage AND [Houses].[Object] = @ObjectId AND Images.Deleted IS NULL";
                    
                    imageList.AddRange(con.Query<Image>(query, new { Stage = stageId, ObjectId = objectId }));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error listing images", ex);
                    throw ex;
                }
            }

            return imageList;
        }

        public Image Read(Guid id)
        {
            Image image;

            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = @" SELECT TOP (1000) * FROM Images WHERE [Id] = @Id";
                    image = con.QuerySingle<Image>(query, new { Id = id });
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error listing images", ex);
                    throw ex;
                }
            }

            if (image == null)
            {
                throw new Exception("Unabel to read image");
            }

            return image;
        }

        public void Update(Image image)
        {
            using (var con = new SqlConnection(_connection))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE [dbo].[Images] SET [URI] = @URI WHERE [Id] = @Id";
                    con.Execute(query, image);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error deleting image", ex);
                    throw ex;
                }
            }
        }
    }
}
