using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models;
using Project_IPET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_PetAdoptController : Controller
    {
        private IPetService _petService;
        public Front_PetAdoptController(IPetService petService)
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

        public IActionResult PetDetail(int id)
        {
            var result = _petService.GetPet(id);
            return View(result);
        }
    }
}
