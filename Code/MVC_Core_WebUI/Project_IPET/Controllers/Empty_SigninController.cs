using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Empty_SigninController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
