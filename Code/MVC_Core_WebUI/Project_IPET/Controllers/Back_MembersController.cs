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

        public IActionResult Index()
        {
            int countOnePage = 10;
            //int totalMembers = (new CBackMembersFactory(_context)).memberFilter(txtKeyword,false).Count();
            int totalMembers = 100;
            (new CTools()).Page(countOnePage, totalMembers, out int totalPage);
            ViewBag.PAGE = totalPage;

            return View();
        }
        [HttpPost]
        public IActionResult ListView(int inputPage, string txtKeyword)
        {
            int page = 1;
            int countOnePage = 10;
            if (inputPage > 0)
                page = inputPage;

            IEnumerable<CBackMembersViewModel> datas = null;
            datas = (new CBackMembersFactory(_context)).memberFilter(txtKeyword, false)
                .Skip((page - 1) * countOnePage).Take(countOnePage).ToList();

            return PartialView(datas);
        }
    }
}
