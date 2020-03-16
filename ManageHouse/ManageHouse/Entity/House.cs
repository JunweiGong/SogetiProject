using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManageHouse.Entity
{
    public class House
    {
        [Key]
        [Display(Name = "House Id")] // NOTE: (kbg) Object Id? Should be != from Id of Functional key. 
        public int Id { get; set; }

        [Required]
        [Display(Name = "House Name")]
        [StringLength(100, ErrorMessage = "House name should be 1 to 100 char in length")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "House Owner")]
        [StringLength(100, ErrorMessage = "Owner name should be 1 to 100 char in length")]
        public string Owner { get; set; }

        [Display(Name = "House Description")]
        [StringLength(1000, ErrorMessage = "Description should be less than 1000 char in length")]
        public string Description { get; set; }
    }
}
