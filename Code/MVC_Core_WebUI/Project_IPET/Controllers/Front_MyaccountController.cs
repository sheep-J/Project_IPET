using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjTest.Models;
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
        private readonly MyProjectContext _context;


        public Front_MyaccountController(MyProjectContext context)
        {

            _context = context;

        }
        public IActionResult Index()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (!string.IsNullOrEmpty(json))
                return View();
            return RedirectToAction("Index", "Empty_Signin");
        }
       
        public  IActionResult MyCommentListView()
        {
            List<CCommentViewModel> mycommentview= null;
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
          

            if (!string.IsNullOrEmpty(json))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId;


                mycommentview = _context.Comments
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
            if (!string.IsNullOrEmpty(user))
            {
                Member nowuser = JsonSerializer.Deserialize<Member>(user);
                int userid = nowuser.MemberId;

                var list = _context.Orders.Where(n => n.MemberId == userid && n.TransactionTypeId == 1).Select(n => new CUserOrderViewModel
                {
                    fId = n.OrderId,
                    fDate = n.RequiredDate.Substring(0, 8),
                    fTotal = (_context.OrderDetails.Where(a => a.OrderId == n.OrderId).Sum(n => n.UnitPrice * n.Quantity) + n.Frieght).ToString(),
                    fStatus = n.OrderStatus.OrderStatusName
                }).ToList();

                if (list == null)
                    return Json("無訂單紀錄");
                return Json(list);
            }
            else
                return Json("請先登入");
        }
        public IActionResult ReadOrderDeatil(int Id)
        {
            var result = _context.OrderDetails.Where(n => n.OrderId == Id).Select(n => new
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
            var result = _context.Orders.Where(n => n.OrderId == Id).Select(o => new
            {
                frieght = o.Frieght,
                total = (_context.OrderDetails.Where(a => a.OrderId == o.OrderId).Sum(n => n.UnitPrice * n.Quantity) + o.Frieght).ToString(),
                where = o.ShippedTo,
                who = o.OrderName,
                phone = o.OrderPhone
            }).FirstOrDefault();
            return Json(result);
        }

        public IActionResult LoadDonate()
        {
            string user = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (!string.IsNullOrEmpty(user))
            {
                Member nowuser = JsonSerializer.Deserialize<Member>(user);
                int userid = nowuser.MemberId;

                var list = _context.Orders.Where(n => n.MemberId == userid && n.TransactionTypeId == 2).Select(n => new
            {
                fId = n.OrderId,
                fDate = n.RequiredDate.Substring(0, 8),
                fTotal = (_context.DonationDetails.Where(a => a.OrderId == n.OrderId).Sum(n => n.UnitPrice * n.Quantity) + n.Frieght).ToString(),
                fStatus = n.OrderStatus.OrderStatusName,
                fFound = n.DonationDetails.FirstOrDefault().Foundation.FoundationName
            }).ToList();

            if (list == null)
                return Json("無捐贈紀錄");
                return Json(list);
            }
            else
                return Json("請先登入");
        }
        public IActionResult ReadDonateDetail(int Id)
        {
            var list = _context.DonationDetails.Where(n => n.OrderId == Id).Select(n => new
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
            var result = _context.Orders.Where(n => n.OrderId == Id).Select(o => new
            {
                frieght = o.Frieght,
                total = (_context.DonationDetails.Where(a => a.OrderId == o.OrderId).Sum(n => n.UnitPrice * n.Quantity) + o.Frieght).ToString(),
                where = o.DonationDetails.FirstOrDefault().Foundation.FoundationName,
                who = o.OrderName,
                phone = o.OrderPhone
            }).FirstOrDefault();
            return Json(result);
        }

        [HttpPost]
        public IActionResult MyContentListView()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            int memberId = (new CMembersFactory(_context)).getMemberId(json);

            CFrontMembersViewModel datas = null;
            datas = _context.Members.Where(m => m.MemberId == memberId)
                .Select(m => new CFrontMembersViewModel
                {
                    Name = m.Name,
                    Email = m.Email,
                    BirthDate = m.BirthDate,
                    Phone = m.Phone,
                    City = m.Region.City.CityName,
                    Region = m.Region.RegionName,
                    Address = m.Address,
                }).FirstOrDefault();

            return PartialView(datas);
        }
        [HttpPost]
        public IActionResult MyContentEdit(CFrontMembersViewModel vModel)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            int memberId = (new CMembersFactory(_context)).getMemberId(json);

            if (vModel.NewPwd != null)
            {
                var member = _context.Members.FirstOrDefault(m => m.MemberId == memberId);
                if (member != null)
                {
                    member.Name = vModel.Name;
                    member.Email = vModel.Email;
                    member.BirthDate = vModel.BirthDate;
                    member.Password = vModel.NewPwd;
                    member.Phone = vModel.Phone;
                    member.RegionId = (new CMembersFactory(_context)).getRegionId(vModel.Region);
                    member.Address = vModel.Address;
                };
                _context.SaveChanges();
                return RedirectToAction("Index", "Front_Myaccount");
            }
            return RedirectToAction("Index", "Front_Myaccount");
        }



    }
}
