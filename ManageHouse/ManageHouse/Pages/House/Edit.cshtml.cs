using System;
using System.Collections.Generic;
using System.Linq;
using ManageHouse.Models;
using ManageHouse.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageHouse.Pages.House
{
    public class EditModel : PageModel
    {
        #region Fields

        IHouseRepository _houseRepository;
        IImageRepository _imageRepository;
        IStageRepository _stageRepository;

        #endregion

        #region Constructor

        public EditModel(IHouseRepository houseRepository, IStageRepository stageRepository, IImageRepository imageRepository)
        {
            _houseRepository = houseRepository;
            _imageRepository = imageRepository;
            _stageRepository = stageRepository;
        }

        #endregion

        #region Properties

        [BindProperty]
        public ViewModels.HouseViewModel House { get; set; }

        [BindProperty]
        public Stage Stage { get; set; }

        public List<Stage> StageList { get; set; }
        public List<string> stageNameList { get; set; }
        public List<Image> ImageList { get; set; }

        #endregion

        #region Methods

        public void OnGet(string id)
        {


            // Fetch selected house
            var house = _houseRepository.Read(id);

            this.House = new ViewModels.HouseViewModel(house);
            this.StageList = _stageRepository.List(house);
            this.stageNameList = new List<string>();
            //stageNameList = this.StageList;

            for (int i = 0; i < this.StageList.Count; i++)
            {
                if (!this.stageNameList.Contains(this.StageList[i].StageName)) {
                    this.stageNameList.Add(this.StageList[i].StageName);
                }

            }

            if (StageList.Any())
            {
                this.Stage = StageList.First();
                this.ImageList = _imageRepository.List(this.House.Object, this.Stage.StageName);
            }
            else
            {
                this.ImageList = new List<Image>();
            }

        }

        public void OnPostManage(string stagenumber, string id)
        {
            var house = _houseRepository.Read(id);

            this.House = new ViewModels.HouseViewModel(house);
            this.StageList = _stageRepository.List(house);

            this.ImageList = _imageRepository.List(id, stagenumber);

            this.stageNameList = new List<string>();
            for (int i = 0; i < this.StageList.Count; i++)
            {
                if (!this.stageNameList.Contains(this.StageList[i].StageName))
                {
                    this.stageNameList.Add(this.StageList[i].StageName);
                }

            }

        }

        public void OnPostDelete(string id, string imageid)
        {
            this._imageRepository.Delete(new Image { Id = new Guid(imageid) });
          
            var house = _houseRepository.Read(id);

            this.House = new ViewModels.HouseViewModel(house);
            this.StageList = _stageRepository.List(house);

            this.stageNameList = new List<string>();
            for (int i = 0; i < this.StageList.Count; i++)
            {
                if (!this.stageNameList.Contains(this.StageList[i].StageName))
                {
                    this.stageNameList.Add(this.StageList[i].StageName);
                }

            }

            if (StageList.Any())
            {
                this.Stage = StageList.First();
                this.ImageList = _imageRepository.List(this.House.Object, this.Stage.StageName);
            }
            else
            {
                this.ImageList = new List<Image>();
            }
        }

        public void OnSelect(Guid id)
        {
            this.Stage = StageList.Single(stage => stage.Id == id);
        }

        public IActionResult OnPostEdit()
        {
            var house = _houseRepository.Read(this.House.Object);

            house.Latitude = this.House.Latitude;
            house.Longitude = this.House.Longitude;
            house.ObjectDescription = this.House.ObjectDescription;

            _houseRepository.Update(house);

            return RedirectToPage("/House/HouseList");
        }
        
        public IActionResult OnPost()
        {
            return Page();
        }
        
        #endregion
    }
}
