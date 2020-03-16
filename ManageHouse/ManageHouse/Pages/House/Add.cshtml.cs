using ManageHouse.Repository;
using ManageHouse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ManageHouse.Pages.House
{
    public class AddModel : PageModel
    {
        #region Fields

        IHouseRepository _houseRepository;
        IImageRepository _imageRepository;
        IStageRepository _stageRepository;

        #endregion

        #region Constructor

        public AddModel(IHouseRepository houseRepository, IStageRepository stageRepository, IImageRepository imageRepository)
        {
            _houseRepository = houseRepository;
            _imageRepository = imageRepository;
            _stageRepository = stageRepository;
        }

        #endregion

        #region Properties

        [BindProperty]
        public HouseViewModel House { get; set; }

        [TempData]
        public string Message { get; set; }
        
        #endregion

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var house = new Models.House
                {
                    Id = Guid.NewGuid(),
                    Object = this.House.Object,
                    ObjectDescription = this.House.ObjectDescription,
                    Longitude = this.House.Longitude,
                    Latitude = this.House.Latitude
                };

                _houseRepository.Create(house);
                return RedirectToPage("/House/HouseList");
            }

            return Page();
        }
    }
}
