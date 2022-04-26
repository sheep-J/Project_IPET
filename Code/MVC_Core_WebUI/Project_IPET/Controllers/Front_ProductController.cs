using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models;
using Project_IPET.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_ProductController : Controller
    {
        private IProductService _productService;
        public Front_ProductController(IProductService productService) //建構子必須和class名稱一樣!
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductList(ProductListRequestModel request)
        {
            var result = _productService.GetProductList(request);
            return View(result);
        }
    }
}
