using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_IPET.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_HomeController : Controller
    {
        private readonly ILogger<Front_HomeController> _logger;

        public Front_HomeController(ILogger<Front_HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
