using AllPay.Payment.Integration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Helpers;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Project_IPET.Controllers
{
    public class Front_CheckoutController : Controller
    {
        int login_memberid;
        private static string TestEnvironmentUrl = "https://payment-stage.opay.tw/Cashier/AioCheckOut/V5";

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

            string SelectTransaction = collection["TransactionOption"];
            string SelectFoundation = collection["FoundationOption"];
            string SelectPayment = collection["RadioOption"];

            Order order = new Order();
            order.MemberId = login_memberid;
            order.DeliveryTypeId = 1;
            order.PaymentTypeId = Convert.ToInt32(SelectPayment);
            order.TransactionTypeId = Convert.ToInt32(SelectTransaction);
            order.OrderStatusId = 4;
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
                    var product = _context.Products.Find(item.ProductID);
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = OrderID;
                    orderDetail.ProductId = item.ProductID;
                    orderDetail.UnitPrice = item.Product.UnitPrice;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Commented = false;
                    list.Add(orderDetail);
                    product.UnitsInStock -= item.Quantity;
                    _context.Products.Update(product);
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
                    var product = _context.Products.Find(item.ProductID);
                    DonationDetail donationDetail = new DonationDetail();
                    donationDetail.OrderId = OrderID;
                    donationDetail.ProductId = item.ProductID;
                    donationDetail.UnitPrice = item.Product.UnitPrice;
                    donationDetail.Quantity = item.Quantity;
                    donationDetail.FoundationId = Convert.ToInt32(SelectFoundation);
                    list.Add(donationDetail);
                    product.UnitsInStock -= item.Quantity;
                    _context.Products.Update(product);
                }
                _context.DonationDetails.AddRange(list);
                _context.SaveChanges();
                SessionHelper.Remove(HttpContext.Session, "Cart");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateOrderForAPI(IFormCollection collection)
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

            string SelectTransaction = collection["TransactionOption"];
            string SelectFoundation = collection["FoundationOption"];
            string SelectPayment = collection["RadioOption"];

            Order order = new Order();
            order.MemberId = login_memberid;
            order.DeliveryTypeId = 1;
            order.PaymentTypeId = Convert.ToInt32(SelectPayment);
            order.TransactionTypeId = Convert.ToInt32(SelectTransaction);
            order.OrderStatusId = 4;
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
                    var product = _context.Products.Find(item.ProductID);
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = OrderID;
                    orderDetail.ProductId = item.ProductID;
                    orderDetail.UnitPrice = item.Product.UnitPrice;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Commented = false;
                    list.Add(orderDetail);
                    product.UnitsInStock -= item.Quantity;
                    _context.Products.Update(product);
                }
                _context.OrderDetails.AddRange(list);
                _context.SaveChanges();
            }
            else
            {
                List<DonationDetail> list = new List<DonationDetail>();
                foreach (var item in CartItems)
                {
                    var product = _context.Products.Find(item.ProductID);
                    DonationDetail donationDetail = new DonationDetail();
                    donationDetail.OrderId = OrderID;
                    donationDetail.ProductId = item.ProductID;
                    donationDetail.UnitPrice = item.Product.UnitPrice;
                    donationDetail.Quantity = item.Quantity;
                    donationDetail.FoundationId = Convert.ToInt32(SelectFoundation);
                    list.Add(donationDetail);
                    product.UnitsInStock -= item.Quantity;
                    _context.Products.Update(product);
                }
                _context.DonationDetails.AddRange(list);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public void OPayAPI()
        {
            var UrlStr = GetCompleteUrl();
            SortedDictionary<string, string> testStr = new SortedDictionary<string, string>();
            testStr.Add("MerchantID", "2000132");
            testStr.Add("MerchantTradeNo", "IPET" + new Random().Next(0, 99999).ToString());
            testStr.Add("MerchantTradeDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            testStr.Add("PaymentType", "aio");
            testStr.Add("TotalAmount", GetOrderTotalAmount());
            testStr.Add("TradeDesc", "TEST");
            testStr.Add("ItemName", GetOrderInfo());
            testStr.Add("ReturnURL", "https://developers.opay.tw/AioMock/MerchantReturnUrl");
            testStr.Add("ChoosePayment", "Credit");
            testStr.Add("ClientBackURL", UrlStr+"/Front_Home/Index");
            SessionHelper.Remove(HttpContext.Session, "Cart");

            string str = string.Empty;
            string str_pre = string.Empty;

            foreach (var test in testStr)
            {
                str += string.Format("&{0}={1}", test.Key, test.Value);
            }
            str_pre += "HashKey=5294y06JbISpM5x9" + str + "&HashIV=v77hoKGq4kWxNNIS";

            string urlEncodeStrPost = HttpUtility.UrlEncode(str_pre);
            string ToLower = urlEncodeStrPost.ToLower();

            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(ToLower));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            string sCheckMacValue = sBuilder.ToString();
            testStr.Add("CheckMacValue", sCheckMacValue);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("<!DOCTYPE html><html><head><meta charset = 'utf-8' /></ head >").AppendLine();
            sb.Append("<form name='CreaditOrder'  id='CreaditOrder' action='" + TestEnvironmentUrl + "' method='POST'>").AppendLine();
            foreach (var aa in testStr)
            {
                sb.Append("<input type='hidden' name='" + aa.Key + "' value='" + aa.Value + "'>").AppendLine();
            }

            sb.Append("</form>").AppendLine();
            sb.Append("<script> var theForm = document.forms['CreaditOrder'];  if (!theForm) { theForm = document.CreaditOrder; } theForm.submit(); </script>").AppendLine();
            sb.Append("</html>").AppendLine();
            Response.WriteAsync(sb.ToString());
        }

        public string GetOrderInfo()
        {
            List<CartModel> cart = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

            string OrderInfo = "";
            foreach (var item in cart)
            {
                OrderInfo += item.Product.ProductName + " x " + item.Quantity + ", NT$" + item.SubTotal + "#";
            }
            return OrderInfo;
        }
        public string GetOrderTotalAmount()
        {
            List<CartModel> CartItems = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

            if (CartItems == null)
                return "100";

            var Order = new OrderModel()
            {
                OrderItem = CartItems,
                Frieght = 60,
            };
            Order.CartTotal = CartItems.Sum(item => item.SubTotal);
            return Decimal.ToInt32(Order.OrderTotal).ToString();
        }
        private string GetCompleteUrl()
        {
            return new StringBuilder()
                 .Append(HttpContext.Request.Scheme)
                 .Append("://")
                 .Append(HttpContext.Request.Host)
                 .Append(HttpContext.Request.PathBase)
                 .ToString();
        }
    }
}