using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Helpers;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_CheckoutController : Controller
    {
        public IActionResult Index()
        {
            if (SessionHelper.GetObjectFromJson<List<OrderDetailModel>>(HttpContext.Session, "Cart") == null)
            {
                return RedirectToAction("Index", "Front_Cart");
            }

            //string loginuser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            //if (!string.IsNullOrEmpty(loginuser))
            //{
            //    Member member = JsonSerializer.Deserialize<Member>(loginuser);
            //    memberid = member.MemberId;
            //}

            var Order = new OrderModel()
            {
                OrderItem = SessionHelper.GetObjectFromJson<List<OrderDetailModel>>(HttpContext.Session, "Cart"),
                Frieght = 60,
            };
            Order.CartTotal = Order.OrderItem.Sum(m => m.SubTotal);
            ViewBag.CartItems = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

            return View(Order);
        }
    }
}
