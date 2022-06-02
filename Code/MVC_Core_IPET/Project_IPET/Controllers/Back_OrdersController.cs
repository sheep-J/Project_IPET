﻿using Microsoft.AspNetCore.Mvc;
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
                fTotal = n.TransactionTypeId ==1?(_context.OrderDetails.Where(a => a.OrderId == n.OrderId).Sum(n => n.UnitPrice * n.Quantity) + n.Frieght).ToString(): (_context.DonationDetails.Where(a => a.OrderId == n.OrderId).Sum(n => n.UnitPrice * n.Quantity) + n.Frieght).ToString(),
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
            var type = _context.Orders.Where(n => n.OrderId == Id).Select(n => n.TransactionTypeId).FirstOrDefault();
            if(type == 1)
            {
                var Comment = _context.Comments.Where(n => n.OrderDetail.OrderId == Id).ToList();
                _context.Comments.RemoveRange(Comment);
                var Detail = _context.OrderDetails.Where(n => n.OrderId == Id).ToList();
                _context.OrderDetails.RemoveRange(Detail);
                var Order = _context.Orders.FirstOrDefault(n => n.OrderId == Id);
                _context.Remove(Order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var Comment = _context.Comments.Where(n => n.OrderDetail.OrderId == Id).ToList();
                _context.Comments.RemoveRange(Comment);
                var Detail = _context.DonationDetails.Where(n => n.OrderId == Id).ToList();
                _context.DonationDetails.RemoveRange(Detail);
                var Order = _context.Orders.FirstOrDefault(n => n.OrderId == Id);
                _context.Remove(Order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult OrderDeatil(int Id)
        {
            var type = _context.Orders.Where(n => n.OrderId == Id).Select(n => n.TransactionTypeId).FirstOrDefault();
            if(type == 1)
            {
                var result = _context.OrderDetails.Where(n => n.OrderId == Id).Select(n => new
                {
                    name = n.Product.ProductName,
                    price = n.UnitPrice,
                    quantity = n.Quantity,
                    total = (n.UnitPrice * n.Quantity)
                });
                return Json(result);
            }
            else
            {
                var result = _context.DonationDetails.Where(n => n.OrderId == Id).Select(n => new
                {
                    name = n.Product.ProductName,
                    price = n.UnitPrice,
                    quantity = n.Quantity,
                    total = (n.UnitPrice * n.Quantity)
                });
                return Json(result);
            }
        }
        public IActionResult OrderOther(int Id)
        {
            var type = _context.Orders.Where(n => n.OrderId == Id).Select(n => n.TransactionTypeId).FirstOrDefault();
            if(type == 1)
            {
                var result = _context.Orders.Where(n => n.OrderId == Id).Select(o => new
                {
                    detailfrieght = o.Frieght,
                    detaliprice = (_context.OrderDetails.Where(a => a.OrderId == o.OrderId).Sum(n => n.UnitPrice * n.Quantity) + o.Frieght).ToString(),
                    detailwhere = o.TransactionTypeId == 1 ? o.ShippedTo : o.DonationDetails.FirstOrDefault().Foundation.FoundationName,
                    detailwho = o.OrderName,
                    detailtype = o.TransactionType.TransactionTypeName
                }).FirstOrDefault();
                return Json(result);
            }
            else
            {
                var result = _context.Orders.Where(n => n.OrderId == Id).Select(o => new
                {
                    detailfrieght = o.Frieght,
                    detaliprice = (_context.DonationDetails.Where(a => a.OrderId == o.OrderId).Sum(n => n.UnitPrice * n.Quantity) + o.Frieght).ToString(),
                    detailwhere = o.TransactionTypeId == 1 ? o.ShippedTo : (o.DonationDetails.FirstOrDefault().Foundation.FoundationName + "(" + o.DonationDetails.FirstOrDefault().Foundation.FoundationAddress + ")"),
                    detailwho = o.OrderName,
                    detailtype = o.TransactionType.TransactionTypeName
                }).FirstOrDefault();
                return Json(result);
            }
        }
    }
}
