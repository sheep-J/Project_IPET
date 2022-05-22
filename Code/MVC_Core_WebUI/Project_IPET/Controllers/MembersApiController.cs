using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjTest.Models;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class MembersApiController : Controller
    {
        private readonly MyProjectContext _context;
        public MembersApiController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult chkIdRepetition(CEmptySignupViewModel vModel)
        {
            Regex regUserId = new Regex(@"[A-Z,a-z,0-9]$");
            var datas = _context.Members.Any(m => m.UserId == vModel.UserId);

            if (!string.IsNullOrEmpty(vModel.UserId))
            {
                if (!datas && regUserId.IsMatch(vModel.UserId))
                    return Content("true");
                return Content("false");
            }
            return Content("null");
        }

        public IActionResult chkemailRepetition(CEmptySignupViewModel vModel)
        {
            Regex regEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!string.IsNullOrEmpty(vModel.Email))
            {
                if (regEmail.IsMatch(vModel.Email))
                    return Content("true");
                return Content("false");
            }
            return Content("null");
        }

        public IActionResult chkpasswordRepetition(CEmptySignupViewModel vModel)
        {
            Regex regPassword = new Regex(@"[A-Z,a-z,0-9]{5,16}$");

            if (!string.IsNullOrEmpty(vModel.CurrentPwd))
            {
                if (regPassword.IsMatch(vModel.CurrentPwd) && vModel.CurrentPwd == vModel.ConfirmPwd)
                    return Content("true");
                return Content("false");
            }
            return Content("null");
        }

        public IActionResult chkphoneRepetition(CEmptySignupViewModel vModel)
        {
            Regex regPassword = new Regex(@"^09[0-9]{8}$");

            if (!string.IsNullOrEmpty(vModel.Phone))
            {
                if (regPassword.IsMatch(vModel.Phone))
                    return Content("true");
                return Content("false");
            }
            return Content("null");
        }

        public IActionResult City()
        {
            var cites = _context.Cities.Select(c => new { c.CityName })
                .OrderBy(c => c.CityName);
            var json = Json(cites);
            return Json(cites);
        }

        public IActionResult Region(string cityname)
        {
            var region = _context.Regions.Where(r => r.City.CityName == cityname)
                .Select(r => new { r.RegionName }).OrderBy(r => r.RegionName);
            return Json(region);
        }

        public IActionResult Logout()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (!string.IsNullOrEmpty(json))
            {
                HttpContext.Session.Remove(CDictionary.SK_LOGINED_USER);
                return RedirectToAction("Index", "Empty_Signin");
            }
            else
                return RedirectToAction("Index", "Empty_Signin");
        }

    }
}
