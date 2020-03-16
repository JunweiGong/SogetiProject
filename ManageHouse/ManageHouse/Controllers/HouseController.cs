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
    public class HouseController : ControllerBase
    {
        private IHouseRepository houseRepository;

        public HouseController(IHouseRepository houseRepository)
        {
            this.houseRepository = houseRepository;
        }

        // GET: api/House
        [HttpGet("list")]
        public IEnumerable<House> List()
        {
            var houses = this.houseRepository.List();
            return houses;
        }

        // GET: api/House/5
        [HttpGet("{id}", Name = "GetHouse")]
        public House Get(Guid id)
        {
            var house = this.houseRepository.Read(id);
            return house;
        }

        // POST: api/House
        [HttpPost]
        public void Post([FromBody] House value)
        {
            this.houseRepository.Create(value);
        }

        // PUT: api/House/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] House value)
        {
            this.houseRepository.Update(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id, [FromBody] House value)
        {
            this.houseRepository.Delete(value);
        }
    }
}
