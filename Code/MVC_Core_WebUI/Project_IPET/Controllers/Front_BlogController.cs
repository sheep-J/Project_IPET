using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using Project_IPET.Services;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{

    public class Front_BlogController : Controller
    {
        private readonly MyProjectContext _context;

        private IWebHostEnvironment _environment;

       
        public Front_BlogController(MyProjectContext myProject, IWebHostEnvironment p)
        {
            _environment = p;
            _context = myProject;
            

        }
        public IActionResult Index()
        {
            
            int countbypage =6;
            int totalpost = _context.Posts
                .Where(c => c.ReplyToPost == null).Count();
            CTools tools = new CTools();
          
            tools.Page(countbypage, totalpost, out int tatalpage);

            ViewBag.page = tatalpage;

            return View();
        }

        [HttpPost]
        public IActionResult ListView(int inputpage ,CPostViewModel postFilter)
        {
            
            int page = 1;
            int countbypage = 6;
            if (inputpage > 0)
                page = inputpage;

            var posts = new CBFrontPostFilterFactory(_context).PostFilter(postFilter)
                      .Where(c => c.ReplyToPost == null)
                      .Skip((page - 1) * countbypage).Take(countbypage).ToList();

            return PartialView(posts);
        }

        public IActionResult PostView(int id,CPostViewModel vModel)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            int userid = 0;
            ViewBag.Id = id;
            if (!string.IsNullOrEmpty(json))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                string userName = userobj.Name;
                userid =userobj.MemberId; ;
                ViewBag.MemberName = userName;

            }
            else
            {

                ViewBag.MemberName = "尚未登入會員";
                
            }

            if (!string.IsNullOrEmpty(json) && !string.IsNullOrEmpty(vModel.PostContent)) 
            { 
                var newReply = new Post
                {
                    MemberId = userid,
                    PostContent = vModel.PostContent,
                    PostDate = DateTime.Now.ToString(),
                    PostTypeId = 6,
                    Banned = false,
                    BannedContent = "********************",
                    LikeCount = 0,
                    ReplyToPost = id,

                };
                _context.Add(newReply);
                _context.SaveChanges();
            }
            var postdetail = _context.Posts.Where(m => m.PostId == id)
                             .Select(p => new CPostViewModel
                             {
                                 PostImage = p.PostImage,
                                 Title = p.Title,
                                 MemberName = p.Member.Name,
                                 LikeCount = p.LikeCount,
                                 PostDate = p.PostDate,
                                 PostContent = p.PostContent,
                                 PostId = id,
                                 PostType = p.PostType.PostTypeName,
                                 ReplyCount = _context.Posts.Where(r => r.ReplyToPost == id).Select(r => r.ReplyToPost).Count(),

                             }).ToList();
            ViewBag.PostID = id;

            return View(postdetail);
        }

        public IActionResult ReplyListView(int inputpostId)
        {
            var replylistview = _context.Posts.Where(p => p.ReplyToPost != null && p.ReplyToPost == inputpostId)
                                .OrderByDescending(d =>d.PostDate)
                                .Select(p => new CPostViewModel {

                                    MemberImage = p.Member.Avatar.ToString(),//ToDo: 等資料庫確定會員照片格式
                                    MemberName = p.Member.Name,
                                    PostDate=p.PostDate,
                                    PostContent=p.PostContent,

                                }).ToList();    


            return PartialView(replylistview);
        }



        public IActionResult CreatePost()
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (string.IsNullOrEmpty(json))
            {
                return RedirectToAction("Index");
            }
          

            var data = _context.PostTypes.Select(p=>p).ToList();


            List<SelectListItem> mySelectItemList = new List<SelectListItem>();
            foreach (var item in data)
            {
                mySelectItemList.Add(new SelectListItem()
                {
                    Text = item.PostTypeName,
                    Value = item.PostTypeId.ToString(),
                    Selected = false
                });
            }
            CPostViewModel model = new CPostViewModel() 
            {
                MyList = mySelectItemList
            };
           
            return View(model);
          
        }
        [HttpPost]
        public IActionResult CreatePost(CPostViewModel vModel) 
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member userobj = JsonSerializer.Deserialize<Member>(json);
            int userid = userobj.MemberId;

            if (vModel.PostPhoto != null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                vModel.PostImage = photoName;
                vModel.PostPhoto.CopyTo(             
                    new FileStream(
                        _environment.WebRootPath + "/Front/Images/blog/UploadImage/" + photoName
                        , FileMode.Create));
            }
            var newpost = new Post
            {
               MemberId = userid,
               Title = vModel.Title,
               PostContent = vModel.PostContent,
               PostDate = DateTime.Now.ToString(),
               PostTypeId = int.Parse(vModel.PostType),
               PostImage =vModel.PostImage,
               Banned = false,
               BannedContent = "********************",
               LikeCount = 0,
               
            };
            _context.Add(newpost);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // 取得商品評價留言 Html Start
        public IActionResult ProductComment(string productname)
        {
            productname = "室內成貓-貓飼料";

            var productcomment = _context.Comments
                                 .OrderByDescending(d=>d.CommentDate)
                                 .Where(p=>p.Product.ProductName == productname)
                                 .Select(n => new CCommentViewModel {

                                     MemberName = n.OrderDetail.Order.Member.Name,
                                     MemberImage = n.OrderDetail.Order.Member.Avatar,
                                     Rating = n.Rating,
                                     CommentDate = n.CommentDate,
                                     CommentContent = n.CommentContent,
                                    
                                 }).ToList();

            return View(productcomment);
        }
        // 取得商品評價留言 Html End
    }
}
