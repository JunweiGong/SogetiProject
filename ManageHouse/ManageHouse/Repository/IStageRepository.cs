using ManageHouse.Models;
using System;
using System.Collections.Generic;

namespace ManageHouse.Repository
{
    public interface IStageRepository
    {
        void Create(Stage stage);
        Stage Read(Guid id);
        void Update(Stage stage);
        void Delete(Stage stage);

        List<Stage> List(House house);
    }
}
