﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_MembersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}