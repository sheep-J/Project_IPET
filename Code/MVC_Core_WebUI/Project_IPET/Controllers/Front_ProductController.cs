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
            ViewBag.PrjProductCount = _productService.GetPrjProductList(new ProductListRequestModel { Pagination = new PageModel() { Page = 1, PageSize = 1 } }).Pagination.TotalRecord;
            return View(result);
        }

        [HttpPost]
        public IActionResult ProductList(ProductListRequestModel request)
        {
            var result = _productService.GetProductList(request);
            return View(result);
        }
        public IActionResult ProductDetail(int id, int prjId)
        {
            ProductListRequestModel requestMode = new ProductListRequestModel();

            var result = _productService.GetProduct(id);
            requestMode.CategoryId = result.CategoryID;
            requestMode.SubCategoryId = -1;
            requestMode.Pagination = new PageModel()
            {
                Page = 1,
                PageSize = int.MaxValue
            };

            var result2 = _productService.GetProductList(requestMode);
            result.RandomProductList = new List<ProductModel>();
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                var index = random.Next(0, result2.ProductList.Count);
                result.RandomProductList.Add(result2.ProductList[index]);
                result2.ProductList.RemoveAt(index);
            }
            result.PrjID = prjId;
            return View(result);
        }
        public IActionResult ProductSingle(int id)
        {
            var result = _productService.GetProduct(id);
            return View(result);
        }


        public IActionResult PrjProductList(ProductListRequestModel request)
        {
            var result = _productService.GetPrjProductList(request);
            return View("ProductList", result);
        }

    }
}
