using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models;
using Project_IPET.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Back_ProductsController : Controller
    {
        private IProductService _productService;

        public Back_ProductsController(IProductService productService) //建構子必須和class名稱一樣!
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

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public bool CreateProduct(ProductModel product, IList<IFormFile> files)
        {
            bool result = true;
            try
            {
                product.ProductImages = new List<byte[]>();
                foreach (IFormFile source in files)
                {
                    MemoryStream ms = new MemoryStream();
                    source.CopyTo(ms);
                    product.ProductImages.Add(ms.ToArray());
                }

                //傳進資料庫
                _productService.CreateProduct(product);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public List<CategoriesModel> GetCategories()
        {
            var result = _productService.GetCategories();
            return result;
        }
        public List<BrandModel> GetBrands()
        {
            var result = _productService.GetBrands();
            return result;
        }

        public IActionResult EditProduct(int id)
        {
            var result = _productService.GetProduct(id);
            return View(result);
        }

        [HttpPost]
        public bool EditProduct(ProductModel product, IList<IFormFile> files)  
        {
            bool result = true;
            try
            {
                product.ProductImages = new List<byte[]>();
                foreach (IFormFile source in files)
                {
                    MemoryStream ms = new MemoryStream();
                    source.CopyTo(ms);
                    product.ProductImages.Add(ms.ToArray());
                }

                //傳進資料庫
                _productService.EditProduct(product);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        [HttpPost]
        public bool DeleteProduct(int id)
        {
            bool result=true;
            try
            {
                _productService.DeleteProduct(id);
            }
            catch(Exception ex)
            {
                result=false;
            }
            return result;
        }

    }
}
