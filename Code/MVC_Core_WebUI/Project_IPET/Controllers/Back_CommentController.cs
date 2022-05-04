using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models.EF;
using Project_IPET.Services;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_CommentController : Controller
    {

        private readonly MyProjectContext _myProject;


        public Back_CommentController(MyProjectContext myProject)
        {

            _myProject = myProject;

        }
        public IActionResult Index()
        {
            int countbypage = 10;
            int totalpost = _myProject.Comments.Count();
            CPostTools tools = new CPostTools();
            tools.Page(countbypage, totalpost, out int tatalpage);

            ViewBag.page = tatalpage;

            return View();
        }

        [HttpPost]
        public IActionResult ListView(int inputpage)
        {
            int page = 1;
            int countbypage = 10;
            int totalpost = _myProject.Comments.Count();


            if (inputpage > 0)
                page = inputpage;

            CPostTools tools = new CPostTools();

            tools.Page(countbypage, totalpost, out int tatalpage);

            var posts = _myProject.Comments.Select(n => new CCommentViewModel
            {
                 CommentId = n.CommentId,
                 ProductName = n.Product.ProductName,
                 OrderId = n.OrderDetail.OrderId,
                 MemberName =n.OrderDetail.Order.Member.Name,
                 Rating = n.Rating,
                 CommentDate =n.CommentDate,
                 CommentContent = n.CommentContent,
                 ReplyContent = n.ReplyContent,
                 Reply = n.Reply,
                 BannedContent = n.BannedContent,
                 Banned = n.Banned,

            }).Skip((page - 1) * countbypage).Take(countbypage).ToList();



            return PartialView(posts);
        }
    }
}
