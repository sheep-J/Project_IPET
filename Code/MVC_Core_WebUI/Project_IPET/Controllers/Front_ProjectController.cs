using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_ProjectController : Controller
    {
        private readonly MyProjectContext _context;
        public Front_ProjectController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddToCart(int? id)
        {
            //找到prjId為參數的專案
            //找到prjId為參數的connect再去找商品
            //找到已付款的訂單, 找出是誰, 消費甚麼, 數量多少
            //上述所有物件包裝成vModel回傳至View()
            return View();
        }

        public IActionResult readProject()
        {
            var list = _context.ProjectDetails.Where(n => DateTime.Compare((DateTime)n.Endtime, DateTime.Now.Date) != -1).Select(n => new CFrontProjectViewModel
            {
                fId = n.PrjId,
                fTitle = n.Title,
                fContent = n.PrjContent,
                fDescription = n.Description,
                fPrjImage = n.PrjImage,
                fDeadline = ((TimeSpan)(n.Endtime - DateTime.Now.Date)).Days.ToString()
            });
            return Json(list);
        }
        public IActionResult readProjectHistory()
        {            
            var list = _context.ProjectDetails.Where(n=>DateTime.Compare((DateTime)n.Endtime,DateTime.Now.Date)==-1).Select(n => new CFrontProjectViewModel
            {
                fId = n.PrjId,
                fTitle = n.Title,
                fContent = n.PrjContent,
                fDescription = n.Description,
                fPrjImage = n.PrjImage,
                fDeadline = ((TimeSpan)(n.Endtime - DateTime.Now.Date)).Days.ToString()
            });
            return Json(list);
        }
    }
}
