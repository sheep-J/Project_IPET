using Project_IPET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Services
{
    public interface IPetService
    {
        PetListModel.Response GetPetList(PetListModel.Request request);

        PetModel GetPet(int id);
        List<CityModel> GetCityList();
    }
}