using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageHouse.Models;
using ManageHouse.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageRepository imageRespository;

        public ImageController(IImageRepository imageRespository)
        {
            this.imageRespository = imageRespository;
        }

        // GET: api/Image/objectid
        [HttpGet("{house}/{stage}")]
        public IEnumerable<string> Get(string house, string stage)
        {
            var images = this.imageRespository.List(house, stage);
            return images.Select(img => img.URI);
        }

        // GET: api/Image/5
        [HttpGet("{id}", Name = "GetImage")]
        public Image Get(Guid id)
        {
            var image = this.imageRespository.Read(id);
            return image;
        }

        // POST: api/Image
        [HttpPost]
        public void Post([FromBody] Image image)
        {
            this.imageRespository.Create(image);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void Delete([FromBody] Image image)
        {
            this.imageRespository.Delete(image);
        }
    }
}
