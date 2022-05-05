using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_OrdersController : Controller
    {
        private readonly MyProjectContext _context;
        public Back_OrdersController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<COrderViewModel> list = null;
            list = _context.Orders.Select(n => new COrderViewModel
            {
                fId = n.OrderId,
                fMemberName = n.Member.Name,
                fRequiredDate = n.RequiredDate.Substring(0, 8),
                fTotal = (_context.OrderDetails.Where(a => a.OrderId == n.OrderId).Sum(n => n.UnitPrice * n.Quantity) + n.Frieght).ToString(),
                fType = n.TransactionType.TransactionTypeName,
                fStatus = n.OrderStatus.OrderStatusName
            });

            return View(list);
        }

        [HttpPost]
        public IActionResult EditOrder(COrderViewModel vModel)
        {
            var fId = vModel.fId;
            var Order = _context.Orders.Find(fId);
            switch (vModel.fStatus)
            {
                case "1":
                    Order.OrderStatusId = 1;
                    break;
                case "2":
                    Order.OrderStatusId = 2;
                    break;
                case "3":
                    Order.OrderStatusId = 3;
                    break;
                case "4":
                    Order.OrderStatusId = 4;
                    break;
                case "5":
                    Order.OrderStatusId = 5;
                    break;
            }
            _context.SaveChanges();
            return Content("資料已修改完成");
        }

        public IActionResult EditOrder(int Id)
        {
            var Order = _context.Orders.Where(n => n.OrderId == Id).Select(n => new
            {
                Id = Id,
                MemberName = n.Member.Name,
                RequiredDate = n.RequiredDate.Substring(0,8),
                Total = (_context.OrderDetails.Where(a => a.OrderId == n.OrderId).Sum(n => n.UnitPrice * n.Quantity) + n.Frieght).ToString(),
                Type = n.TransactionType.TransactionTypeName,
                Status = n.OrderStatus.OrderStatusName
            }).FirstOrDefault();
            return Json(Order);
        }

        public IActionResult delete(int Id)
        {
            var Detail = _context.OrderDetails.Where(n => n.OrderId == Id);
            foreach (var item in Detail)
            {
                _context.Remove(item);
            }
            var Order = _context.Orders.FirstOrDefault(n => n.OrderId == Id);
            _context.Remove(Order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult OrderDeatil(int Id)
        {
            var result = _context.OrderDetails.Where(n => n.OrderId == Id).Select(n => new
            {
                Name = n.Product.ProductName,
                Price = n.UnitPrice,
                Quantity = n.Quantity,
                Total = (n.UnitPrice * n.Quantity)
            });
            return Json(result);
        }
        public IActionResult OrderOther(int Id)
        {
            var result = _context.Orders.Where(n => n.OrderId == Id).Select(o => new
            {
                detailfrieght = o.Frieght,
                detaliprice = (_context.OrderDetails.Where(a => a.OrderId == o.OrderId).Sum(n => n.UnitPrice * n.Quantity) + o.Frieght).ToString(),
                detailwhere = o.ShippedTo,
                detailwho = o.OrderName,
                detailtype = o.TransactionType.TransactionTypeName
            }).FirstOrDefault();
            return Json(result);
        }
    }
}
