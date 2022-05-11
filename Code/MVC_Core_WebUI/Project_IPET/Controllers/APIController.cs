using Microsoft.AspNetCore.Mvc;
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
    }
}
