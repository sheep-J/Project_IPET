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
            ViewBag.TOTALPAGE = ((new CMembersFactory(_context)).memberFilter(filter).Count() / 10) + 1;
            ViewBag.TOTALMEMBER = (new CMembersFactory(_context)).memberFilter(filter).Count();

            return View();
        }

        [HttpPost]
        public IActionResult ListView(CMembersFilter filter)
        {
            IEnumerable<CBackMembersViewModel> datas = null;
            datas = (new CMembersFactory(_context)).memberFilter(filter);
            return PartialView(datas);
        }

    }
}
