using System;
using System.Collections.Generic;
using ManageHouse.Models;
using ManageHouse.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq;

namespace ManageHouse.Pages.House
{
    public class IndexModel : PageModel
    {
        #region Fields

        IHouseRepository _houseRepository;
        IImageRepository _imageRepository;
        IStageRepository _stageRepository;
        
        #endregion

        #region Counstructor

        public IndexModel(IHouseRepository houseRepository, IStageRepository stageRepository, IImageRepository imageRepository)
        {
            _houseRepository = houseRepository;
            _imageRepository = imageRepository;
            _stageRepository = stageRepository;
        }
        
        #endregion

        #region Properties
        
        [BindProperty]
        public List<ViewModels.HouseViewModel> Houses { get; set; }

        [BindProperty]
        public Models.House SelectedHouse { get; set; }

        [TempData]
        public string Message { get; set; }

        #endregion

        #region Methods

        public void OnPostDelete(string id)
        {
            var house = _houseRepository.Read(id);
            _houseRepository.Delete(house);
            this.Houses = _houseRepository.List().Select(n => new ViewModels.HouseViewModel(n)).ToList();
        }

        public void OnGet()
        {
            this.Houses = _houseRepository.List().Select(n => new ViewModels.HouseViewModel(n)).ToList();
        }

        #endregion
    }
}
