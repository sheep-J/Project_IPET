using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_ProjectController : Controller
    {
        private readonly MyProjectContext _context;
        public IWebHostEnvironment _environment;
        public Back_ProjectController(MyProjectContext context, IWebHostEnvironment e)
        {
            _context = context;
            _environment = e;
        }

        public IActionResult Index()
        {
            List<CProjectViewModel> list = null;
            list = _context.ProjectDetails.Select(n => new CProjectViewModel
            {
                fId = n.PrjId,
                fTitle = n.Title,
                fGoal = (int)n.Goal,
                fStarttime = n.Starttime.ToString(),
                fEndtime = n.Endtime.ToString(),
                fFoundation = n.Foundation.FoundationName
            }).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CProjectCreateViewModel vModel)
        {
            string PhotoName = string.Empty;
            string str = vModel.fContent.Replace(Environment.NewLine, "<br>");
            if (vModel.fPhoto != null)
            {
                PhotoName = Guid.NewGuid().ToString() + ".jpg";
                vModel.fPhotoPath = PhotoName;

                //dispose,為了刪掉圖片
                using (FileStream fs = new FileStream(_environment.WebRootPath + "/Front/images/project/" + PhotoName, FileMode.Create))
                {
                    vModel.fPhoto.CopyTo(fs);
                }
            }
            var prj = new ProjectDetail()
            {
                Title = vModel.fTitle,
                Description = vModel.fDes,
                Goal = Convert.ToDecimal(vModel.fGoal),
                Starttime = vModel.fStart,
                Endtime = vModel.fEnd,
                PrjContent = str,
                FoundationId = vModel.fFound,
                PrjImage = vModel.fPhotoPath
            };
            _context.ProjectDetails.Add(prj);
            _context.SaveChanges();
            foreach (int prod in vModel.fProds)
            {
                int project = _context.ProjectDetails.OrderByDescending(n => n.PrjId).Select(n => n.PrjId).FirstOrDefault();
                var newconnect = new PrjConnect()
                {
                    PrjId = project,
                    ProductId = prod
                };
                _context.PrjConnects.Add(newconnect);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            var prjconn = _context.PrjConnects.Where(n => n.PrjId == Id).ToList();
            _context.PrjConnects.RemoveRange(prjconn);
            _context.SaveChanges();
            var prj = _context.ProjectDetails.Find(Id);
            string imgPath = $"{_environment.WebRootPath}/Front/images/project/{prj.PrjImage}";
            _context.ProjectDetails.Remove(prj);
            _context.SaveChanges();
            System.IO.File.Delete(imgPath);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int fId, CProjectCreateViewModel vModel)
        {
            string PhotoName = string.Empty;
            string str = vModel.fContent.Replace(Environment.NewLine, "<br>");

            //刪除連結
            var conn = _context.PrjConnects.Where(n => n.PrjId == fId).ToList();
            _context.PrjConnects.RemoveRange(conn);

            //修改專案內容
            var prj = _context.ProjectDetails.Find(fId);
            prj.Title = vModel.fTitle;
            prj.Description = vModel.fDes;
            prj.FoundationId = vModel.fFound;
            prj.Goal = Convert.ToDecimal(vModel.fGoal);
            prj.Starttime = vModel.fStart;
            prj.Endtime = vModel.fEnd;
            prj.PrjContent = str;

            //圖片處裡
            if (vModel.fPhoto != null)
            {
                string imgPath = $"{_environment.WebRootPath}/Front/images/project/{prj.PrjImage}";
                System.IO.File.Delete(imgPath);
                PhotoName = Guid.NewGuid().ToString() + ".jpg";
                vModel.fPhotoPath = PhotoName;
                //dispose,為了刪掉圖片
                using (FileStream fs = new FileStream(_environment.WebRootPath + "/Front/images/project/" + PhotoName, FileMode.Create))
                {
                    vModel.fPhoto.CopyTo(fs);
                }
                prj.PrjImage = vModel.fPhotoPath;
            }

            //重建連結
            foreach (int prod in vModel.fProds)
            {
                var newconnect = new PrjConnect()
                {
                    PrjId = fId,
                    ProductId = prod
                };
                _context.PrjConnects.Add(newconnect);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //取得專案商品(數量為庫存加售出)
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

        //取得訂單除商品以外明細
        public IActionResult ProjectDetail(int Id)
        {
            var Project = _context.ProjectDetails.Where(n => n.PrjId == Id).Select(p => new
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

        //Create 基金會資料導入
        public IActionResult CreateLoadFound()
        {
            var list = _context.Foundations.Select(n => new
            {
                id = n.FoundationId,
                name = n.FoundationName
            }).ToList();
            return Json(list);
        }

        //Create 商品資料導入
        public IActionResult CreateLoadProd()
        {
            var list = _context.Products.Where(n => n.SubCategory.CategoryId == 1).Select(n => new
            {
                id = n.ProductId,
                name = n.ProductName
            }).ToList();
            return Json(list);
        }

        //Edit 專案內容導入
        public IActionResult EditLoad(int Id)
        {
            var prj = _context.ProjectDetails.Where(n => n.PrjId == Id).Select(n => new
            {
                fId = n.PrjId,
                fTitle = n.Title,
                fDes = n.Description,
                fContent = n.PrjContent,
                fGoal = n.Goal,
                fStart = n.Starttime,
                fEnd = n.Endtime,
                fImg = n.PrjImage,
                fFound = n.FoundationId,
                fAllfund = _context.Foundations.Select(n => new
                {
                    id = n.FoundationId,
                    name = n.FoundationName
                }).ToList(),
                fProd = _context.PrjConnects.Where(n => n.PrjId == Id).Select(n => n.ProductId).ToList(),
                fAllprod = _context.Products.Where(n => n.SubCategory.CategoryId == 1).Select(n => new
                {
                    prodId = n.ProductId,
                    prodName = n.ProductName
                }).ToList()
            }).FirstOrDefault();
            return Json(prj);
        }
    }
}
