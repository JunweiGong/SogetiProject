using ManageHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageHouse.Repository
{
    public interface IImageRepository
    {
        void Create(Image image);

        Image Read(Guid id);

        void Delete(Image image);
                
        void Update(Image image);
        List<Image> List(string objectId, string stageId);
    }
}
