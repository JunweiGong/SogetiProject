using ManageHouse.Models;
using System.ComponentModel.DataAnnotations;


namespace ManageHouse.ViewModels
{
    public class HouseViewModel
    {

        public HouseViewModel()
        {

        }

        public HouseViewModel(House house)
        {
            Latitude = house.Latitude;
            Longitude = house.Longitude;
            Object = house.Object;
            ObjectDescription = house.ObjectDescription;
        }

        [Required]
        [Display(Name = "Object Id")]
        [StringLength(20, ErrorMessage = "Object Id should max be 20")]
        public string Object { get; set; }

        [Required]
        [Display(Name = "Object Description")]
        [StringLength(100, ErrorMessage = "Owner name should be 1 to 100 char in length")]
        public string ObjectDescription { get; set; }

        [Display(Name = "Object Longitude")]
        public string Longitude { get; set; }

        [Display(Name = "Object Latitude")]
        public string Latitude { get; set; }
    }
}
