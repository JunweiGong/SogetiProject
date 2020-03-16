using System;

namespace ManageHouse.Models
{
    public class Stage : Entity
    {
        public string StageName { get; set; }

        public Guid HouseId { get; set; }
        public Guid ImageId { get; set; }
    }
}
