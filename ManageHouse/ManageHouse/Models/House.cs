using System;
using Microsoft.SqlServer.Types;

namespace ManageHouse.Models
{
    public class House : Entity
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        
        public string Object { get; set; }
        public string ObjectDescription { get; set; }
    }

     public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; } // Null = Not deleted
    }
}
