using Microsoft.AspNetCore.Mvc;
using prjTest.Models;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Empty_SignupController : Controller
    {
        private readonly MyProjectContext _context;
        public Empty_SignupController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CEmptySignupViewModel vModel)
        {

            var member = new Member
            {
                Name = vModel.LastName + vModel.FirstName,
                UserId = vModel.UserId,
                Email = vModel.Email,
                Gender = (new CMembersFactory(_context)).getGender(vModel.Gender),
                BirthDate = vModel.BirthDate,
                Password = vModel.CurrentPwd,
                Phone = vModel.Phone,
                RegionId = (new CMembersFactory(_context)).getRegionId(vModel.Region),
                Address = vModel.Address,
                RegisteredDate = DateTime.Now,
            };

            _context.Members.Add(member);
            _context.SaveChanges();
            return RedirectToAction("Index", "Empty_Signin");
        }

    }
}
