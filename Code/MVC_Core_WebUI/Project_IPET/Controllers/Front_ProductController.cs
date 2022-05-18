using Microsoft.AspNetCore.Mvc;
using Project_IPET.Enum;
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
            var result = _productService.GetCategories();

            ViewBag.Brands = _productService.GetBrands();
            return View(result);
        }

        [HttpPost]
        public IActionResult ProductList(ProductListRequestModel request)
        {
            var result = _productService.GetProductList(request);
            return View(result);
        }
        public IActionResult ProductDetail(int id)
        {
            var result = _productService.GetProduct(id);
            return View(result);
        }
        public IActionResult ProductSingle(int id)
        {
            var result = _productService.GetProduct(id);
            return View(result);
        }
    }
}
