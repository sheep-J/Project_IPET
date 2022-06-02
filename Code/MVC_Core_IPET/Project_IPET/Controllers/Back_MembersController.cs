using Microsoft.AspNetCore.Mvc;
using prjTest.Models;
using Project_IPET.Models.EF;
using Project_IPET.Services;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_MembersController : Controller
    {
        private readonly MyProjectContext _context;
        public Back_MembersController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index(CMembersFilter filter)
        {
            int pagesize = 10;
            int totalmember = (new CMembersFactory(_context)).memberFilter(filter).Count();
            (new CTools()).Page(pagesize, totalmember, out int totalpage);
            ViewBag.PAGESIZE = pagesize;
            ViewBag.TOTALMEMBER = totalmember;
            ViewBag.TOTALPAGE = totalpage;

            var datas = new CMembersFactory(_context).memberFilter(filter).ToList();
            return View(datas);
        }

        [HttpPost]
        public IActionResult Banned(bool Banned, int ID) {
            var member = _context.Members.FirstOrDefault(m => m.MemberId == ID);
            if (member != null)
            {
                if (Banned)
                {
                    member.Banned = false;
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Back_Members");
                }
                else
                {
                    member.Banned = true;
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Back_Members");
                }
            }
            return RedirectToAction("Index", "Back_Members");
        }
    }
}
