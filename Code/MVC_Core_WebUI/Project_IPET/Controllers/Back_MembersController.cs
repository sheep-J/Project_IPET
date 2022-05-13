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
            int countOnePage = 10;
            int totalMembers = (new CMembersFactory(_context)).memberFilter(filter).Count();
            (new CTools()).Page(countOnePage, totalMembers, out int totalPage);
            ViewBag.COUNTONEPAGE = countOnePage;
            ViewBag.TOTALMEMBERS = totalMembers;
            ViewBag.TOTALPAGE = totalPage;

            IEnumerable<CBackMembersViewModel> datas = (new CMembersFactory(_context)).memberFilter(filter);

            return View(datas);
        }
    }
}
