using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjTest.Models;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using Project_IPET.Services;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Project_IPET.Controllers
{
    public class Front_MyaccountController : Controller
    {
        private readonly MyProjectContext _context;
        private readonly IWebHostEnvironment _host;

        public Front_MyaccountController(MyProjectContext context, IWebHostEnvironment host)
        {

            _context = context;
            _host = host;
        }

        public IActionResult Index()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (!string.IsNullOrEmpty(json))
                return View();
            return RedirectToAction("Index", "Empty_Signin");
        }

        public IActionResult MyCommentListView()
        {

            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);


            if (!string.IsNullOrEmpty(json))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId;


                var nocomment = _context.OrderDetails
                               .Where(u => u.Order.MemberId == userid && u.Commented == false)
                               .OrderByDescending(d => d.OrderDetailId)
                               .Select(n => new CCommentViewModel
                               {
                                   ProductId = n.ProductId,
                                   OrderDetailId = n.OrderDetailId,
                                   MemberID = userid,
                                   ProductName = n.Product.ProductName,
                                   Rating = null,
                                   CommentDate = null,
                                   CommentContent = null,
                                   ReplyContent = null,
                               }).ToList();

                var comment = _context.Comments
                                      .Where(u => u.OrderDetail.Order.MemberId == userid)
                                      .OrderByDescending(d => d.CommentDate)
                                      .Select(n => new CCommentViewModel
                                      {
                                          ProductId = n.ProductId,
                                          OrderDetailId = n.OrderDetailId,
                                          MemberID = userid,
                                          ProductName = n.OrderDetail.Product.ProductName,
                                          Rating = n.Rating,
                                          CommentDate = n.CommentDate.Substring(0, 10),
                                          CommentContent = n.CommentContent,
                                          ReplyContent = n.ReplyContent,
                                      }).ToList();
                nocomment.AddRange(comment);


                return PartialView(nocomment);

            }
            else
            {
                ViewBag.NOData = "尚未有任何評價";
                return PartialView();
            }
        }
        public IActionResult CreateComment(CCommentViewModel vModel)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member userobj = JsonSerializer.Deserialize<Member>(json);
            int userid = userobj.MemberId;

            if (!string.IsNullOrEmpty(vModel.CommentContent) && !string.IsNullOrEmpty(vModel.Rating.ToString()))
            {
                var newpost = new Comment
                {
                    ProductId = vModel.ProductId,
                    OrderDetailId = vModel.OrderDetailId,
                    Rating = vModel.Rating,
                    CommentDate = DateTime.Now.GetDateTimeFormats('f')[0].ToString(),
                    CommentContent = vModel.CommentContent,
                    ReplyContent = null,
                    BannedContent = "*******************",
                    Reply = false,
                    Banned = false,
                };

                OrderDetail OrderDetail = _context.OrderDetails
                                          .FirstOrDefault(p => p.OrderDetailId == vModel.OrderDetailId);
                if (OrderDetail != null)
                {
                    OrderDetail.Commented = true;
                    _context.Add(newpost);
                    _context.SaveChanges();
                }



                return RedirectToAction("Index", "Front_Myaccount");
            }

            return RedirectToAction("Index", "Front_Myaccount");
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
                total = (n.UnitPrice * n.Quantity)
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
                    Avatar = m.Avatar,
                }).FirstOrDefault();

            return PartialView(datas);
        }
        [HttpPost]
        public IActionResult MyContentEdit(CFrontMembersViewModel vModel)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            int memberId = (new CMembersFactory(_context)).getMemberId(json);

            string ImagesFolder = Path.Combine(_host.WebRootPath,
                "Front/images/register/members", vModel.Photo.FileName);
            using (var fileStream = new FileStream(ImagesFolder, FileMode.Create))
            {
                vModel.Photo.CopyTo(fileStream);
            }

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
                    member.Avatar = vModel.Photo.FileName;
                };
                _context.SaveChanges();
                return RedirectToAction("Index", "Front_Myaccount");
            }
            return RedirectToAction("Index", "Front_Myaccount");
        }

        [HttpPost]
        public IActionResult MyWishListView()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            int memberId = (new CMembersFactory(_context)).getMemberId(json);


            IEnumerable<CFrontWishListViewModel> datas = null;

            datas = from f in _context.MyFavorites 
                            join p in _context.Products
                            on f.ProductId equals p.ProductId
                            join c in _context.Comments
                            on p.ProductId equals c.ProductId
                            where (f.MemberId == memberId)
                            select new CFrontWishListViewModel 
                            {
                                ProductName = p.ProductName,
                                ProductPrice = p.UnitPrice,
                                Quantity = p.UnitsInStock,
                                FavoriteId = f.FavoriteId,
                                ProductId = f.ProductId,
                                Ranking = decimal.Round((decimal)(from c in _context.Comments
                                                    join p in _context.Products
                                                    on c.ProductId equals p.ProductId
                                                    where (c.ProductId == f.ProductId)
                                                    select c.Rating).Average(),1),
                               
                             };


            //datas = _context.MyFavorites.Where(m => m.MemberId == memberId)
            //    .OrderByDescending(m => m.FavoriteId)
            //    .Select(m => new CFrontWishListViewModel
            //    {
            //        ProductName = m.Product.ProductName,
            //        ProductPrice = m.Product.UnitPrice,
            //        Quantity = m.Product.UnitsInStock,
            //        FavoriteId = m.FavoriteId,
            //        ProductId = m.ProductId,
            //        Ranking = getProductRating(m.ProductId),

            //    }).ToList();

            //_context.MyFavorites.Where(m => m.MemberId == memberId).Select(m=>m.);

            return PartialView(datas);
        }

        public decimal getProductRating(int id)
        {
            if (id == 0)
            {
                return 0;
            }
            else 
            {
                var Comments = (from c in _context.Comments
                                               where (c.ProductId == id)
                                               select c).Count();

                var Ranking = (decimal)(from c in _context.Comments
                                        join p in _context.Products
                                        on c.ProductId equals p.ProductId
                                        where (c.ProductId == id)
                                        select c.Rating).Average();
                if (Comments != 0)
                {
                    return decimal.Round(Ranking,1);
                }
                else 
                {
                    return 0;
                }
             
            }
           
           
        }


        [HttpPost]
        public IActionResult MyWishListAddCart(int? id)
        {
            Product datas = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (datas == null)
                return RedirectToAction("Front_Myaccount");

            string json = HttpContext.Session.GetString("Cart");
            var cart = JsonSerializer.Deserialize<List<CartModel>>(json);
            if (cart == null)
            {
                cart = new List<CartModel>();
                HttpContext.Session.SetString("Cart", "");
            }
            CartModel item = new CartModel()
            {
                ProductID = id.Value,
                Product = datas,
            };
            cart.Add(item);

            string jsoncart = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", jsoncart);
            return RedirectToAction("Index", "Front_Myaccount");
        }

        [HttpPost]
        public IActionResult MyWishListDelete(int? id)
        {
            MyFavorite datas = _context.MyFavorites.FirstOrDefault(m => m.FavoriteId == id);
            if (datas != null)
            {
                _context.MyFavorites.RemoveRange(datas);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Front_Myaccount");
        }


    }
}
