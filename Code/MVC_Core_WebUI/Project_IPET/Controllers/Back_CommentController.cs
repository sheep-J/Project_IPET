using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class Back_CommentController : Controller
    {

        private readonly MyProjectContext _context;


        public Back_CommentController(MyProjectContext context)
        {

            _context = context;

        }
        public IActionResult Index(int FilterRating, CCommentViewModel commentFilter)
        {
            int pagesize = 10;
            int totalcomment = new CCommentFilterFactory(_context).CommentFilter(commentFilter).Count();

            commentFilter.Rating = FilterRating;
            CTools tools = new CTools();
            tools.Page(pagesize, totalcomment, out int tatalpage);
            ViewBag.totalcomment = totalcomment;
            ViewBag.page = tatalpage;
            ViewBag.pagesize = pagesize;
            
            var Comments = new CCommentFilterFactory(_context).CommentFilter(commentFilter).ToList();

            return View(Comments);
        }

        public IActionResult Shield(int? Id) 
        {
            if (Id != null ) 
            {
                Comment comment = _context.Comments
                                 .FirstOrDefault(c => c.CommentId == Id);
                if (comment != null) 
                {
                   string temp = "";
                    temp = comment.CommentContent;
                    comment.CommentContent = comment.BannedContent;
                    comment.BannedContent = temp;
                    comment.Banned = !comment.Banned;
                    _context.SaveChanges();
                }

                return RedirectToAction("Index","Back_Comment");
            }


            return RedirectToAction("Index", "Back_Comment");
        }

        public IActionResult CreateReply(CCommentViewModel vModel)
        {


            if (vModel.CommentId != 0 || vModel.SelectID != 0)
            {
                Comment comment = _context.Comments
                                 .FirstOrDefault(c => c.CommentId == vModel.CommentId ||c.CommentId == vModel.SelectID);
                if (comment != null)
                {

                    if (vModel.ReplyContent != null && vModel.ReplyContent != "親切地回覆顧客留言以拉近與顧客的距離")
                    {
                        comment.ReplyContent = vModel.ReplyContent;
                        comment.Reply = true;

                    }
                    else 
                    {
                        comment.ReplyContent = null;
                       comment.Reply = false;
                    }
                   
                    
                    _context.SaveChanges();
                }

                return RedirectToAction("Index", "Back_Comment");
            }


            return RedirectToAction("Index", "Back_Comment");
        }
        




    }
}
