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

        public IActionResult Index(/*string txtKeyword*/)
        {
            int countOnePage = 10;
            int totalMembers = (new CMembersFactory(_context)).memberFilter(new CMembersFilter()).Count();
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
            datas = (new CMembersFactory(_context)).memberFilter(new CMembersFilter
            {
                keyword = txtKeyword,
                male = false,
                female = false,
            });

            int totalMembers = datas.Count();
            (new CTools()).Page(countOnePage, totalMembers, out int totalPage);
            ViewBag.PAGE = totalPage;

            datas = datas
                 .Skip((page - 1) * countOnePage).Take(countOnePage).ToList();

            return PartialView(datas);
        }
    }
}
