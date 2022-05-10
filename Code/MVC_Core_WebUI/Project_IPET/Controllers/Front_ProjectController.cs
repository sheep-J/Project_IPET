using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public IActionResult Detail(int? id)
        {
            //找到prjId為參數的專案
            var project = _context.ProjectDetails.Where(n=>n.PrjId==id).Select(p=>new CProjectDetailViewModel {
                fId = p.PrjId,
                fTitle = p.Title,
                fDescription = p.Description,
                fGoal = p.Goal.ToString(),
                fPrjContent = p.PrjContent,
                fStarttime = ((DateTime)p.Starttime).Day.ToString(),
                fEndtime = ((DateTime)p.Endtime).Day.ToString(),
                fPrjImage = p.PrjImage,
                fFoundation = p.Foundation.FoundationName,
                fDeadline = ((TimeSpan)(p.Endtime - DateTime.Now.Date)).Days < 0?"0": ((TimeSpan)(p.Endtime - DateTime.Now.Date)).Days.ToString()
            }).FirstOrDefault();
            //找到prjId為參數的connect再去找商品
            //找到已付款的訂單, 找出是誰, 消費甚麼, 數量多少
            //上述所有物件包裝成vModel回傳至View()
            return View(project);
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

        public IActionResult readContent(int Id)
        {
            var content = _context.ProjectDetails.Where(n => n.PrjId == Id).Select(n => n.PrjContent).FirstOrDefault();
            return Json(content);
        }

        public IActionResult readPercent(int Id)
        {
            var list = _context.PrjConnects.Where(n => n.PrjId == Id);
            int total = 0;
            foreach (var item in list.ToList())
            {
                var orderlist = _context.OrderDetails.Where(n => n.ProductId == item.ProductId).Sum(n => n.UnitPrice * n.Quantity);
                total += (int)orderlist;
            }
            var goal = _context.ProjectDetails.Where(n => n.PrjId == Id).Select(n => n.Goal).FirstOrDefault();
            int persent = (total / (int)goal) * 100;
            return Json(persent);
        }

        public IActionResult readCount(int Id)
        {
            int count = 0;
            var list = _context.PrjConnects.Where(n => n.PrjId == Id).ToList();
            foreach (var item in list)
            {
                var order = _context.OrderDetails.Where(n => n.ProductId == item.ProductId).ToList();
                foreach(var o in order)
                {
                    count++;
                }
            }
            return Json(count);
        }

        public IActionResult readList(int Id)
        {
            List<CProjectBuylistViewModel> buylist = new List<CProjectBuylistViewModel>();
            var list = _context.PrjConnects.Where(n => n.PrjId == Id).ToList();
            foreach (var item in list)
            {
                var orderlist = _context.OrderDetails.Where(n => n.ProductId == item.ProductId).Select(o => new CProjectBuylistViewModel
                {
                    UserName = o.Order.Member.Name,
                    ProductName = o.Product.ProductName
                }).ToList();
                foreach (var o in orderlist)
                {
                    buylist.Add(o);
                }
            }
            return Json(buylist);
        }

        public IActionResult readProd(int Id)
        {
            var list = _context.PrjConnects.Where(n => n.PrjId == Id).Select(n=>new CProjectProdViewModel
            {
                fId = n.Product.ProductId,
                fName = n.Product.ProductName,
                fDes = n.Product.Description,
                fPrice = (int)n.Product.UnitPrice,
                fQuantity = n.Product.UnitsInStock,
            }).ToList();

            return Json(list);
        }
        public IActionResult takeProdpic(int Id)
        {
            var img = _context.Products.Where(n => n.ProductId == Id).Select(n => n.ProductImagePaths.FirstOrDefault().ProductImage).FirstOrDefault();
            return File(img, "image/jepg");
        }

        public IActionResult readProdDetail(int Id)
        {
            var prod = _context.Products.Where(n => n.ProductId == Id).Select(n => new
            {
                id = n.ProductId,
                name = n.ProductName,
                des = n.Description,
                price = n.UnitPrice,
                stock = n.UnitsInStock,
            }).ToList();
            return Json(prod);
        }
    }
}
