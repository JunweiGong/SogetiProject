using System;
using System.Collections.Generic;
using ManageHouse.Models;
using ManageHouse.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ManageHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private IStageRepository stageRepository;

        public StageController(IStageRepository stageRepository)
        {
            this.stageRepository = stageRepository;
        }

        [HttpGet("{objectId}")]
        public IEnumerable<Stage> Get(string objectId)
        {
            var stage = this.stageRepository.List(new House { Object = objectId });
            return stage;
        }

        [HttpGet("{id}", Name = "GetStage")]
        public Stage Get(Guid id)
        {
            var stage = this.stageRepository.Read(id);
            return stage;
        }

        [HttpPost]
        public void Post([FromBody] Stage value)
        {
            this.stageRepository.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Stage value)
        {
            this.stageRepository.Update(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void Delete([FromBody] Stage value)
        {
            this.stageRepository.Delete(value);
        }
    }
}
