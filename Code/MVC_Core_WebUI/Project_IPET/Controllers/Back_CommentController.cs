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
        public IActionResult Index(CCommentViewModel commentFilter)
        {
            int pagesize = 10;
            int totalpost = _context.Comments.Count();

            CTools tools = new CTools();
            tools.Page(pagesize, totalpost, out int tatalpage);
            ViewBag.totalpost = totalpost;
            ViewBag.page = tatalpage;
            ViewBag.pagesize = pagesize;
            
            var Comments = new CCommentFilterFactory(_context).CommentFilter(commentFilter).ToList();

            return View(Comments);
        }

       

        



        [HttpPost]
        public decimal[] GetProductRating(string productname)
        {

            if (productname == null)
            {
                return null;
            }
            else
            {
                var averagerating = _context.Comments
                    .Where(p => p.Product.ProductName == productname)
                    .Select(p => p.Rating).Average();

            
                var totalcommentcount = _context.Comments
                    .Where(p => p.Product.ProductName == productname)
                    .Count();

                decimal[] Rating = { decimal.Round((decimal)averagerating, 1), totalcommentcount };

               return Rating;
            }

        }


    }
}
