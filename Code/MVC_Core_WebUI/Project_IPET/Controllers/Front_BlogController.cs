using Microsoft.AspNetCore.Mvc;
using Project_IPET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult PostView()
        {
            return View();
        }

        public ActionResult CreatePost()
        {
            return View();
        }

        public IActionResult TestProductComment() {

            return View();
        }

        public IActionResult SendGmail()
        {

            CSendGmailService sendmail = new CSendGmailService();
            sendmail.sendGmail();

            return View();
        }
    }
}
