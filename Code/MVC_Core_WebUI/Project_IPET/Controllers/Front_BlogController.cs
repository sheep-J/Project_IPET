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


        public IActionResult Index(string PostType, CPostViewModel postFilter)
        {

            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (!string.IsNullOrEmpty(json))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId;
                postFilter.userId = userid;
            }

            int pagesize = 6;
            postFilter.FilterPostType = PostType;
            int totalpost = new CPostFilterFactory(_context).PostFilter(postFilter)
                                                            .Where(c => c.ReplyToPost == null).Count();


            var PostTypeName = _context.PostTypes
                              .OrderBy(p => p.PostTypeId)
                              .Select(n => new CPostViewModel
                              {
                                  PostType = n.PostTypeName.ToString(),
                                  PostTypeId = n.Posts.Count,
                              })
                              .ToList();
            ViewBag.PostType = PostTypeName;


            CTools tools = new CTools();
            tools.Page(pagesize, totalpost, out int tatalpage);
            ViewBag.totalpost = totalpost;
            ViewBag.tatalpage = tatalpage;
            ViewBag.pagesize = pagesize;


            ViewBag.PostDate2021 = new CPostFilterFactory(_context).PostOrderByYear(2021);
            ViewBag.PostDate2020 = new CPostFilterFactory(_context).PostOrderByYear(2020);
            ViewBag.PostDateNTimeNow = new CPostFilterFactory(_context).PostOrderByYear(DateTime.Now.Year);

            var posts = new CPostFilterFactory(_context).PostFilter(postFilter)
                      .Where(c => c.ReplyToPost == null).ToList();


            return View(posts);
        }


        public IActionResult PostView(int id, CPostViewModel vModel)
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            int userid = 0;
            ViewBag.Id = id;
            if (!string.IsNullOrEmpty(json))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                string userName = userobj.Name;
                userid = userobj.MemberId; ;
                ViewBag.MemberName = userName;

            }
            else
            {

                ViewBag.MemberName = "尚未登入會員";

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

        public IActionResult CreateReply(CPostViewModel vModel) 
        {
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
          
            if (!string.IsNullOrEmpty(json) && !string.IsNullOrEmpty(vModel.PostContent))
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId; ;
                var newReply = new Post
                {
                    MemberId = userid,
                    PostContent = vModel.PostContent,
                    PostDate = DateTime.Now.ToString(),
                    PostTypeId = 6,
                    Banned = false,
                    BannedContent = "********************",
                    LikeCount = 0,
                    ReplyToPost =int.Parse(vModel.ReplyToPost),

                };
                _context.Add(newReply);
                _context.SaveChanges();
            }

            return RedirectToAction("PostView");
        }

        public IActionResult ReplyListView(int inputpostId)
        {
            var replylistview = _context.Posts.Where(p => p.ReplyToPost != null && p.ReplyToPost == inputpostId)
                                .OrderByDescending(d => d.PostDate)
                                .Select(p => new CPostViewModel
                                {

                                    MemberImage = p.Member.Avatar,
                                    MemberName = p.Member.Name,
                                    PostDate = p.PostDate,
                                    PostContent = p.PostContent,

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


            var data = _context.PostTypes.Select(p => p).ToList();


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

            if (!string.IsNullOrEmpty(vModel.PostContent) && !string.IsNullOrEmpty(vModel.Title))
            {
                var newpost = new Post
                {
                    MemberId = userid,
                    Title = vModel.Title,
                    PostContent = vModel.PostContent,
                    PostDate = DateTime.Now.GetDateTimeFormats('f')[0].ToString(),
                    PostTypeId = int.Parse(vModel.PostType),
                    PostImage = vModel.PostImage,
                    Banned = false,
                    BannedContent = "********************",
                    LikeCount = 0,

                };
                _context.Add(newpost);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("CreatePost");
        }


        public IActionResult HomeBlogView(CPostViewModel postFilter)
        {
            var posts = new CPostFilterFactory(_context).PostFilter(postFilter)
              .Where(c => c.ReplyToPost == null)
              .Select(n => new CPostViewModel
              {
                  PostId = n.PostId,
                  Title = n.Title,
                  PostContent = n.PostContent,
                  PostDate = n.PostDate,
                  LikeCount = n.LikeCount,
                  PostImage = n.PostImage,
                  MemberName = n.MemberName,
                  MemberId = n.MemberId,

              }).Take(3).ToList();

            return PartialView(posts);

        }



        public IActionResult CreateLike(CPostViewModel vModel)
        {

            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (string.IsNullOrEmpty(json))
            {
                return View();
            }
            else
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId;
                if (vModel.PostId != 0 && userid != 0)
                {

                    var newlike = new PostLiked
                    {
                        MemberId = userid,
                        PostId = vModel.PostId,

                    };
                    var likecount = _context.Posts
                        .FirstOrDefault(p => p.PostId == vModel.PostId && p.MemberId == vModel.MemberId);
                    if (likecount != null)
                    {
                        likecount.LikeCount++;
                    }

                    _context.Add(newlike);
                    _context.SaveChanges();

                    return View();
                }
                return View();
            }
        }

        public IActionResult DeleteLike(CPostViewModel vModel)
        {

            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);

            if (string.IsNullOrEmpty(json))
            {
                return View();
            }
            else
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId;


                if (vModel.PostId != 0 && userid != 0)
                {

                    var deletelike = (from l in _context.PostLikeds
                                      join m in _context.Members
                                      on l.MemberId equals m.MemberId
                                      join p in _context.Posts
                                      on l.PostId equals p.PostId
                                      where l.MemberId == userid && l.PostId == vModel.PostId
                                      select l).FirstOrDefault();

                    var likecount = _context.Posts
                       .FirstOrDefault(p => p.PostId == vModel.PostId && p.MemberId == vModel.MemberId);

                    if (likecount != null)
                    {
                        likecount.LikeCount--;
                    }

                    _context.Remove(deletelike);
                    _context.SaveChanges();

                    return View();
                }
                return View();
            }
        }

    }
}
