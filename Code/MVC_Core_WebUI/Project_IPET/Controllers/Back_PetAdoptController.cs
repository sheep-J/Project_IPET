using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models;
using Project_IPET.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_PetAdoptController : Controller
    {
        private IPetService _petService;

        public Back_PetAdoptController(IPetService petService)
        {
            _petService = petService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PetList(PetListModel.Request request)
        {
            var result = _petService.GetPetList(request);
            return View(result);
        }

        public IActionResult CreatePet()
        {
            return View();
        }

        [HttpPost]
        public bool CreatePet(PetModel petModel, IList<IFormFile> files)
        {
            bool result = true;
            try
            {
                petModel.PetImages = new List<byte[]>();
                foreach (IFormFile source in files)
                {
                    MemoryStream ms = new MemoryStream();
                    source.CopyTo(ms);
                    petModel.PetImages.Add(ms.ToArray());
                }

                //傳進資料庫
                _petService.CreatePet(petModel);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public IActionResult EditPet(int id)
        {
            var result = _petService.GetPet(id);
            return View(result);
        }

        [HttpPost]
        public bool EditPet(PetModel pet, IList<IFormFile> files)
        {
            bool result = true;
            try
            {
                pet.PetImages = new List<byte[]>();
                foreach (IFormFile source in files)
                {
                    MemoryStream ms = new MemoryStream();
                    source.CopyTo(ms);
                    pet.PetImages.Add(ms.ToArray());
                }

                //傳進資料庫
                _petService.EditPet(pet);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
