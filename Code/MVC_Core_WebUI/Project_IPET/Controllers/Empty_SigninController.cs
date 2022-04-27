using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Empty_SigninController : Controller
    {
        //public IActionResult Index()
        //{
        //    if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USER))
        //        return RedirectToAction("Index", "Front_Home");
        //    return View();
        //}

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(CLoginViewModel vModel)
        {
            MyProjectContext db = new MyProjectContext();
            Member customer = db.Members.FirstOrDefault(m => m.UserId == vModel.txtAccount);
            if (customer != null && customer.Password.Equals(vModel.txtPassword))
            {
                string json = JsonSerializer.Serialize(customer);
                HttpContext.Session.SetString(CDictionary.SK_LOGINED_USER, json);
                return customer.RoleId == 1 ? RedirectToAction("Index", "Front_Home") : RedirectToAction("Index", "Back_Home");
            }
            return View();
        }
    }
}
