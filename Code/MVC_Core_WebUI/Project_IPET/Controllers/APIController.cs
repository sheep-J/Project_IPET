using Microsoft.AspNetCore.Mvc;
using Project_IPET.Helpers;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class APIController : Controller
    {

        private readonly MyProjectContext _context;
        int foundationid;
        string foundation_address;

        public APIController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult ReadTransaction()
        {
            var transaction = _context.TransactionTypes.Select(t => new { t.TransactionTypeId, t.TransactionTypeName }).OrderBy(t => t.TransactionTypeId);
            return Json(transaction);
        }

        public IActionResult ReadFoundation()
        {
            var foundation = _context.Foundations.Select(f => new { f.FoundationId, f.FoundationName, f.FoundationAddress }).OrderBy(f => f.FoundationId);
            return Json(foundation);
        }
        public IActionResult GetFoundationAddress()
        {
            List<CartModel> CartItems = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

            foreach (var item in CartItems)
            {
                var conn = _context.PrjConnects.Where(c => c.ProductId == item.ProductID).FirstOrDefault();

                if (conn != null)
                {
                    int prjid = Convert.ToInt32(conn.PrjId);
                    var prjdetail = _context.ProjectDetails.Where(p => p.PrjId == prjid).FirstOrDefault();
                    int foundationid = Convert.ToInt32(prjdetail.FoundationId);
                    var foundation = _context.Foundations.Where(f => f.FoundationId == foundationid).FirstOrDefault();
                    foundation_address = foundation.FoundationAddress;
                }
            }
            return Content(foundation_address);
        }
        public IActionResult GetFoundationId()
        {
            List<CartModel> CartItems = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

            foreach (var item in CartItems)
            {
                var conn = _context.PrjConnects.Where(c => c.ProductId == item.ProductID).FirstOrDefault();

                if (conn != null)
                {
                    int prjid = Convert.ToInt32(conn.PrjId);
                    var prjdetail = _context.ProjectDetails.Where(p => p.PrjId == prjid).FirstOrDefault();
                    foundationid = Convert.ToInt32(prjdetail.FoundationId);
                }
            }
            return Content(foundationid.ToString());
        }
    }
}
