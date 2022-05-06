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
          
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            if (json != null)
            {
                Member userobj = JsonSerializer.Deserialize<Member>(json);
                int userid = userobj.MemberId;




                var mycommentview = _myProject.Comments
                                    .Where(u => u.OrderDetail.Order.MemberId == userid)
                                    .Select(n => new CCommentViewModel
                                    {
                                        ProductName = n.Product.ProductName,
                                        Rating = n.Rating,
                                        CommentDate = n.CommentDate,
                                        CommentContent = n.CommentContent,
                                    }).ToList();


                return PartialView(mycommentview);

            }
            else
            {
                return null;
            }
        }

    }
}
