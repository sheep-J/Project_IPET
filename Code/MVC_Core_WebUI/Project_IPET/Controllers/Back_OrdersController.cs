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
                fStatus = n.OrderStatus.OrderStatusName
            });

            return View(list);
        }

        public IActionResult OrderDeatil(int Id)
        {
            var result = _context.OrderDetails.Where(n => n.OrderId == Id).Select(n => new {
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
                detailwho = o.OrderName
            }).FirstOrDefault();
            return Json(result);
        }
    }
}
