using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using prjTest.Models;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Empty_SignupController : Controller
    {
        private readonly MyProjectContext _context;
        private readonly IWebHostEnvironment _host;
        public Empty_SignupController(MyProjectContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult chkFlag(CEmptySignupViewModel vModel)
        {
            if (vModel.FlagUserId && vModel.FlagEmail && vModel.FlagPassword && vModel.FlagPhone)
            {
                return Content("true");
            }
            return Content("false");
        }


        [HttpPost]
        public IActionResult Register(CEmptySignupViewModel vModel)
        {
                string ImagesFolder = Path.Combine(_host.WebRootPath,
                "Front/images/register/members", vModel.Photo.FileName);
                using (var fileStream = new FileStream(ImagesFolder, FileMode.Create))
                {
                    vModel.Photo.CopyTo(fileStream);
                }

                var member = new Member
                {
                    Name = vModel.LastName + vModel.FirstName,
                    UserId = vModel.UserId,
                    Gender = (new CMembersFactory(_context)).getGender(vModel.Gender),
                    BirthDate = vModel.BirthDate,
                    Email = vModel.Email,
                    Password = vModel.NewPwd,
                    Phone = vModel.Phone,
                    RegionId = (new CMembersFactory(_context)).getRegionId(vModel.Region),
                    Address = vModel.Address,
                    RegisteredDate = DateTime.Now,
                    Avatar = vModel.Photo.FileName,
                };

                _context.Members.Add(member);
                _context.SaveChanges();
                return RedirectToAction("Index", "Empty_Signin");
        }

    }
}
