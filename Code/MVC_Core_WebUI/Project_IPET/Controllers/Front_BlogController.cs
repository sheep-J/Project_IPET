using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using Project_IPET.Services;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{

    public class Front_BlogController : Controller
    {
        private readonly MyProjectContext _myProject;


        public Front_BlogController(MyProjectContext myProject)
        {

            _myProject = myProject;

        }
        public IActionResult Index( )
        {
            int countbypage =6;
            int totalpost = _myProject.Posts.Where(c => c.ReplyToPost == null).Count();
            CTools tools = new CTools();
            tools.Page(countbypage, totalpost, out int tatalpage);

            ViewBag.page = tatalpage;

            return View();
        }

        [HttpPost]
        public IActionResult ListView(int inputpage)
        {
            int page = 1;
            int countbypage = 6;
           
            if (inputpage > 0)
                page = inputpage;

            var posts = _myProject.Posts.Where(c => c.ReplyToPost == null).Select(n => new CPostViewModel
            {

                PostId = n.PostId,
                Title = n.Title,
                PostContent = n.PostContent,
                PostDate = n.PostDate,
                LikeCount = n.LikeCount,
                PostImage = n.PostImage,
                Tag = n.Tag,
                MemberName = n.Member.Name,
                MemberId = n.MemberId,

            }).Skip((page - 1) * countbypage).Take(countbypage).ToList();

            return PartialView(posts);
        }

        public IActionResult PostView(int id)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (!string.IsNullOrEmpty(json))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                string userid = userobj.Name;
                ViewBag.MemberName = userid;

            }
            else
            {

                ViewBag.MemberName = "尚未登入會員";
                
            }


            var postdetail = _myProject.Posts.Where(m => m.PostId == id)
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
                                 ReplyConut = _myProject.Posts.Where(r => r.ReplyToPost == id).Select(r => r.ReplyToPost).Count(),

                             }).ToList();
            ViewBag.PostID = id;

            return View(postdetail);
        }

        public IActionResult ReplyListView(int inputpostId)
        {
            var replylistview = _myProject.Posts.Where(p => p.ReplyToPost != null && p.ReplyToPost == inputpostId)
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
          

            var data = _myProject.PostTypes.Select(p=>p).ToList();


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
            
            //if (vModel.photo != null)
            //{
            //    string photoName = Guid.NewGuid().ToString() + ".jpg";
            //    vModel.FImagePath = photoName;
            //    vModel.photo.CopyTo(                //CopyTo要Stream非字串 所以要new FileStream
            //        new FileStream(
            //            _environment.WebRootPath + "/Images/" + photoName
            //            , FileMode.Create));
            //}
            var newpost = _myProject.Posts.Select(p =>new Post
            {
               MemberId = userid,
               Title = vModel.Title,
               PostContent = vModel.PostContent,
               PostDate = DateTime.Now.ToShortDateString(),
               PostTypeId = int.Parse(vModel.PostType),
               Banned = false,
               BannedContent = "********************",
               LikeCount = 0,
               
        });
            _myProject.Add(newpost);
            _myProject.SaveChanges();
            return RedirectToAction("Index");
        }


        // 取得商品評價留言 Html Start
        public IActionResult ProductComment(string productname)
        {
            productname = "室內成貓-貓飼料";

            var productcomment = _myProject.Comments
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
