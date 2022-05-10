using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using Project_IPET.Services;
using Project_IPET.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;


namespace Project_IPET.Controllers
{
    public class Front_MyaccountController : Controller
    {
        private readonly MyProjectContext _myProject;


        public Front_MyaccountController(MyProjectContext myProject)
        {

            _myProject = myProject;

        }
        public IActionResult Index()
        {
            return View();
        }
       
        public  IActionResult MyCommentListView()
        {
            List<CCommentViewModel> mycommentview= null;
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
          

            if (!string.IsNullOrEmpty(json))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId;


                mycommentview = _myProject.Comments
                                   .Where(u => u.OrderDetail.Order.MemberId == userid)
                                   .OrderByDescending(d => d.CommentDate)
                                   .Select(n => new CCommentViewModel
                                   {
                                       ProductName = n.Product.ProductName,
                                       Rating = n.Rating,
                                       CommentDate = n.CommentDate.Substring(0,10),
                                       CommentContent = n.CommentContent,
                                       ReplyContent = n.ReplyContent,
                                   }).ToList();

                return PartialView(mycommentview);

            }
            else
            {
                ViewBag.NOData = "尚未有任何評價";
                return PartialView();
            }
        }
        public IActionResult CreateyComment()
        {


           

            return View();
        }


        public IActionResult LoadOrder()
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            //if (!string.IsNullOrEmpty(user)) {
                //Member nowuser = JsonSerializer.Deserialize<Member>(user);
                //int userid = nowuser.MemberId;

                var list = _myProject.Orders.Where(n => n.MemberId == 3 && n.TransactionTypeId == 1).Select(n => new CUserOrderViewModel
                {
                    fId = n.OrderId,
                    fDate = n.RequiredDate.Substring(0, 8),
                    fTotal = (_myProject.OrderDetails.Where(a => a.OrderId == n.OrderId).Sum(n => n.UnitPrice * n.Quantity) + n.Frieght).ToString(),
                    fStatus = n.OrderStatus.OrderStatusName
                }).ToList();

                if (list == null)
                    return Json("無訂單紀錄");
                return Json(list);
            //}
            //else
            //    return Json("請先登入");
        }
        public IActionResult ReadOrderDeatil(int Id)
        {
            var result = _myProject.OrderDetails.Where(n => n.OrderId == Id).Select(n => new
            {
                name = n.Product.ProductName,
                price = n.UnitPrice,
                quantity = n.Quantity,
                total = (n.UnitPrice * n.Quantity)
            }).ToList();
            return Json(result);
        }
        public IActionResult ReadOrderOther(int Id)
        {
            var result = _myProject.Orders.Where(n => n.OrderId == Id).Select(o => new
            {
                frieght = o.Frieght,
                total = (_myProject.OrderDetails.Where(a => a.OrderId == o.OrderId).Sum(n => n.UnitPrice * n.Quantity) + o.Frieght).ToString(),
                where = o.ShippedTo,
                who = o.OrderName,
                phone = o.OrderPhone
            }).FirstOrDefault();
            return Json(result);
        }

        public IActionResult LoadDonate()
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            //if (!string.IsNullOrEmpty(user)) {
            //Member nowuser = JsonSerializer.Deserialize<Member>(user);
            //int userid = nowuser.MemberId;

            var list = _myProject.Orders.Where(n => n.MemberId == 3 && n.TransactionTypeId == 2).Select(n => new
            {
                fId = n.OrderId,
                fDate = n.RequiredDate.Substring(0, 8),
                fTotal = (_myProject.DonationDetails.Where(a => a.OrderId == n.OrderId).Sum(n => n.UnitPrice * n.Quantity) + n.Frieght).ToString(),
                fStatus = n.OrderStatus.OrderStatusName,
                fFound = n.DonationDetails.FirstOrDefault().Foundation.FoundationName
            }).ToList();

            if (list == null)
                return Json("無捐贈紀錄");
            return Json(list);
            //}
            //else
            //    return Json("請先登入");
        }
        public IActionResult ReadDonateDetail(int Id)
        {
            var list = _myProject.DonationDetails.Where(n => n.OrderId == Id).Select(n => new
            {
                name = n.Product.ProductName,
                price = n.UnitPrice,
                quantity = n.Quantity,
                total = (n.UnitPrice*n.Quantity)
            }).ToList();
            return Json(list);
        }
        public IActionResult ReadDonateOther(int Id)
        {
            var result = _myProject.Orders.Where(n => n.OrderId == Id).Select(o => new
            {
                frieght = o.Frieght,
                total = (_myProject.DonationDetails.Where(a => a.OrderId == o.OrderId).Sum(n => n.UnitPrice * n.Quantity) + o.Frieght).ToString(),
                where = o.DonationDetails.FirstOrDefault().Foundation.FoundationName,
                who = o.OrderName,
                phone = o.OrderPhone
            }).FirstOrDefault();
            return Json(result);
        }
    }
}
