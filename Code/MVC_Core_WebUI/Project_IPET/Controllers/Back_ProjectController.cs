using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_ProjectController : Controller
    {
        private readonly MyProjectContext _context;
        public Back_ProjectController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<CProjectViewModel> list = null;
            list = _context.ProjectDetails.Select(n => new CProjectViewModel
            {
                fId = n.PrjId,
                fTitle = n.Title,
                fGoal = (int)n.Goal,
                fStarttime = n.Starttime.ToString(),
                fEndtime = n.Endtime.ToString(),
                fFoundation = n.Foundation.FoundationName
            });
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult ProjectProduct(int Id)
        //{
        //    var productlist = _context.PrjConnects.Where(n => n.PrjId == Id).Select(n=>n.ProductId).ToList();

        //    var list = new List<Product>();
        //    foreach (var item in productlist)
        //    {
        //        var prod = _context.Products.FirstOrDefault(n => n.ProductId == item);
        //        list.Add(prod);
        //    }
        //    return Json(list);
        //}
        //新版取得專案商品(數量為庫存加售出)
        public IActionResult ProjectProduct(int Id)
        {
            List<dynamic> list = new List<dynamic>();
            var prod = _context.PrjConnects.Where(n => n.PrjId == Id).Select(n => n.ProductId).ToList();
            foreach (var item in prod)
            {
                var other = _context.Products.Where(n => n.ProductId == item).Select(n => new
                {
                    name = n.ProductName,
                    price = n.UnitPrice,
                    stock = n.UnitsInStock + _context.OrderDetails.Where(n => n.ProductId == item).Sum(n => n.Quantity)
                }).FirstOrDefault();
                list.Add(other);
            }
            return Json(list);
        }

        public IActionResult ProjectDetail(int Id)
        {
            var Project = _context.ProjectDetails.Where(n=>n.PrjId == Id).Select(p=>new
            {
                detailimg = p.PrjImage,
                detailtitle = p.Title,
                detailfoundation = p.Foundation.FoundationName,
                detailgoal = p.Goal,
                detailstarttime = p.Starttime.ToString(),
                detailendtime = p.Endtime.ToString(),
            });
            return Json(Project);
        }
    }
}
