using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddToCart(int? id)
        {
            //找到prjId為參數的專案
            //找到prjId為參數的connect再去找商品
            //找到已付款的訂單, 找出是誰, 消費甚麼, 數量多少
            //上述所有物件包裝成vModel回傳至View()
            return View();
        }
    }
}
