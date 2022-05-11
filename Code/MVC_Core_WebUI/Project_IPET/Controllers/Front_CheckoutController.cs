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
        int login_memberid;

        private readonly MyProjectContext _context;

        public Front_CheckoutController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartModel> CartItems = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

            if (CartItems == null)
            {
                return RedirectToAction("Index", "Front_Cart");
            }

            var Order = new OrderModel()
            {
                OrderItem = CartItems,
                Frieght = 60,
            };
            Order.CartTotal = CartItems.Sum(item => item.SubTotal);

            return View(Order);
        }


        [HttpPost]
        public IActionResult CreateOrder(IFormCollection collection)
        {

            string loginuser = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (!string.IsNullOrEmpty(loginuser))
            {
                Member member = JsonSerializer.Deserialize<Member>(loginuser);
                login_memberid = member.MemberId;
            }

            List<CartModel> CartItems = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

            if (CartItems == null)
            {
                return RedirectToAction("Index", "Front_Cart");
            }

            string time = DateTime.Now.ToString("yyyyMMddHHmmss");

            //string formatString = "yyyyMMddHHmmss";
            //string sample = "20100611221912";
            //DateTime dt = DateTime.ParseExact(sample, formatString, null);

            string SelectTransaction = collection["TransactionOption"];
            string SelectFoundation = collection["FoundationOption"];

            Order order = new Order();
            order.MemberId = login_memberid;
            order.DeliveryTypeId = 1;
            order.PaymentTypeId = 1;
            order.TransactionTypeId = Convert.ToInt32(SelectTransaction);
            order.OrderStatusId = 1;
            order.RequiredDate = time;
            order.ShippedTo = collection["OrderAddress"];
            order.Frieght = 60;
            order.OrderName = collection["OrderName"];
            order.OrderPhone = collection["OrderPhone"];

            _context.Orders.Add(order);
            _context.SaveChanges();


            int OrderID = _context.Orders.Single(o => o.RequiredDate == time && o.MemberId == login_memberid).OrderId;

            if (SelectTransaction == "1")
            {
                List<OrderDetail> list = new List<OrderDetail>();
                foreach (var item in CartItems)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = OrderID;
                    orderDetail.ProductId = item.ProductID;
                    orderDetail.UnitPrice = item.Product.UnitPrice;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Commented = false;
                    list.Add(orderDetail);
                }
                _context.OrderDetails.AddRange(list);
                _context.SaveChanges();
                SessionHelper.Remove(HttpContext.Session, "Cart");
            }
            else
            {
                List<DonationDetail> list = new List<DonationDetail>();
                foreach (var item in CartItems)
                {
                    DonationDetail donationDetail = new DonationDetail();
                    donationDetail.OrderId = OrderID;
                    donationDetail.ProductId = item.ProductID;
                    donationDetail.UnitPrice = item.Product.UnitPrice;
                    donationDetail.Quantity = item.Quantity;
                    donationDetail.FoundationId = Convert.ToInt32(SelectFoundation);
                    list.Add(donationDetail);
                }
                _context.DonationDetails.AddRange(list);
                _context.SaveChanges();
                SessionHelper.Remove(HttpContext.Session, "Cart");
            }
            return RedirectToAction("Index");
        }

    }
}