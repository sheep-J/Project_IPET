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
            {
                ViewBag.commentcount = GetComment().Count;
                return View();
            }
            return RedirectToAction("Index", "Empty_Signin");
        }

        public IActionResult MyCommentListView()
        {

            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (!string.IsNullOrEmpty(json))
            {

                var comment = GetComment();

                return PartialView(comment);

            }
            else
            {
                ViewBag.NOData = "尚未有任何評價";
                return PartialView();
            }
        }

        public List<CCommentViewModel> GetComment()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (!string.IsNullOrEmpty(json))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId;

                var nocomment = (from od in _context.OrderDetails
                                 join o in _context.Orders
                                 on od.OrderId equals o.OrderId
                                 join p in _context.Products
                                 on od.ProductId equals p.ProductId
                                 where o.MemberId == userid && od.Commented == false && o.OrderStatusId == 4
                                 orderby od.OrderDetailId descending
                                 select new CCommentViewModel
                                 {
                                     ProductId = od.ProductId,
                                     OrderDetailId = od.OrderDetailId,
                                     MemberID = userid,
                                     ProductName = p.ProductName,
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


                return nocomment;

            }
            else
            {
                return null;

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

        public IActionResult chkFlag(CEmptySignupViewModel vModel)
        {
            if (vModel.FlagEmail && vModel.FlagPhone && vModel.FlagOldPwd && vModel.FlagPassword)
            {
                return Content("true");
            }
            return Content("false");
        }

        [HttpPost]
        public IActionResult MyWishListView()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            int memberId = (new CMembersFactory(_context)).getMemberId(json);

            IEnumerable<CFrontWishListViewModel> datas = null;

            #region WIP 導覽
            //datas = from f in _context.MyFavorites
            //        select new CFrontWishListViewModel()
            //        {
            //            ProductName = f.Product.ProductName,
            //            ProductPrice = f.Product.UnitPrice,
            //            Quantity = f.Product.UnitsInStock,
            //            //Ranking = decimal.Round((decimal)f.Product.Comments.Average(c => c.Rating), 1),
            //            FavoriteId = f.FavoriteId,
            //            ProductId = f.ProductId,
            //        };
            #endregion

            datas = from f in _context.MyFavorites
                            join p in _context.Products
                            on f.ProductId equals p.ProductId
                            join c in _context.Comments
                            on p.ProductId equals c.ProductId into js
                            from c1 in js.DefaultIfEmpty()
                            select new CFrontWishListViewModel()
                            {
                                ProductName = f.Product.ProductName,
                                ProductPrice = f.Product.UnitPrice,
                                Quantity = f.Product.UnitsInStock,
                                //Ranking = decimal.Round((decimal)c1.Rating,1),
                                FavoriteId = f.FavoriteId,
                                ProductId = f.ProductId,
                                CommentId = c1 == null ? "Null" : c1.CommentId.ToString()
                            };

            //ViewBag.Rating = from x in datas
            //                 group x by x.ProductId into g
            //                 select new { Ranking = g.Average(c => c.Ranking) };

            return PartialView(datas);
        }

        [HttpPost]
        public IActionResult MyWishListDelete(int? id)
        {
            MyFavorite datas = _context.MyFavorites.FirstOrDefault(m => m.FavoriteId == id);
            if (datas != null)
            {
                _context.MyFavorites.Remove(datas);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Front_Myaccount");
        }

        public IActionResult GetProductToWishtList(int id)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (json == null)
            {
                return RedirectToAction("Index", "Empty_Signin");
            }
            else
            {
                int memberId = (new CMembersFactory(_context)).getMemberId(json);
                var added = _context.MyFavorites.Where(f => f.MemberId == memberId).Any(f => f.ProductId == id);

                if (added)
                    return RedirectToAction("Index", "Front_Myaccount");
                else
                {
                    var wishList = new MyFavorite
                    {
                        MemberId = memberId,
                        ProductId = id,
                    };
                    _context.MyFavorites.Add(wishList);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Front_Myaccount");
                }

            }
        }


    }
}
