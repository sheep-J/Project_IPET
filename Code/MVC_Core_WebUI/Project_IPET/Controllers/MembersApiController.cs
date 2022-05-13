using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
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
