using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_ContactController : Controller
    {

        private readonly MyProjectContext _context;

        public Front_ContactController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateContact(IFormCollection collection)
        {
            CustomerContact contact = new CustomerContact();
            contact.ContactName = collection["ContactName"];
            contact.ContactMail = collection["ContactMail"];
            contact.ContactSubject = collection["ContactSubject"];
            contact.ContactMessage = collection["ContactMessage"];
            contact.ReplyStatus = false;

            _context.CustomerContacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Front_Home");
        }
    }
}
