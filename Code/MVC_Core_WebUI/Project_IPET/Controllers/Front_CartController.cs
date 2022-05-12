using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_IPET.Helpers;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Controllers
{
    public class Front_CartController : Controller
    {
        private readonly MyProjectContext _context;

        public Front_CartController(MyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartModel> CartItems = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

            if (CartItems != null)
            {
                ViewBag.Count = CartItems.Count();     // 計算購物車商品數 (種類)
                ViewBag.Total = CartItems.Sum(item => item.SubTotal);       // 計算購物車總金額
                return View(CartItems);
            }
            else
            {
                ViewBag.Count = 0;
                ViewBag.Total = 0;
                return RedirectToAction("Index", "Front_Home");
            }

        }

        [HttpPost]
        public IActionResult AddToCart(int id, int? qty)
        {

            var product = _context.Products.Single(m => m.ProductId.Equals(id));

            // TODO 優化計算項目
            CartModel item = new CartModel()
            {
                ProductID = product.ProductId,
                Product = product,
                Category = GetCategoryName(product.SubCategoryId),
                Quantity = (int)qty,
                SubTotal = product.UnitPrice * (int)qty,
                imageSrc = ConvertImage(GetProductImage(product.ProductId))
            };

            if (SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart") == null)
            {
                // 如果沒有已存在購物車, 則建立新的購物車
                List<CartModel> cart = new List<CartModel>();
                cart.Add(item);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            }
            else
            {
                // 如果已存在購物車, 檢查有無相同的商品 , 有的話只調整數量
                List<CartModel> cart = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");

                int index = cart.FindIndex(m => m.Product.ProductId.Equals(id));

                if (index != -1)
                {
                    cart[index].Quantity += (int)qty;
                    cart[index].SubTotal += product.UnitPrice * (int)qty;
                }
                else
                {
                    cart.Add(item);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);

            }

            //return NoContent(); // TODO 請求成功但不更新畫面 & Toastr effect

            return RedirectToAction("Index");
        }

        public IActionResult RemoveItem(int id)
        {
            List<CartModel> cart = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");
            int index = cart.FindIndex(m => m.Product.ProductId.Equals(id));
            cart.RemoveAt(index);

            if (cart.Count < 1)
            {
                SessionHelper.Remove(HttpContext.Session, "Cart");
            }
            else
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            }

            return RedirectToAction("Index");
        }


        public IActionResult RemoveAll()
        {
            List<CartModel> cart = SessionHelper.GetObjectFromJson<List<CartModel>>(HttpContext.Session, "Cart");
            cart.RemoveRange(0, cart.Count);

            if (cart.Count < 1)
            {
                SessionHelper.Remove(HttpContext.Session, "Cart");
            }
            else
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            }

            return RedirectToAction("Index");
        }

        private string ConvertImage(byte[] arrayImage)
        {
            string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
            return "data:image/png;base64," + base64String;
        }

        public string GetCategoryName(int id)
        {
            var CategoryName = (from p in _context.Products
                                join sc in _context.SubCategories on id equals (sc.SubCategoryId)
                                join c in _context.Categories on sc.CategoryId equals (c.CategoryId)
                                select c.CategoryName).FirstOrDefault().ToString();

            return CategoryName;
        }

        public byte[] GetProductImage(int id)
        {
            var ProductImage = (from p in _context.Products
                                join pip in _context.ProductImagePaths on id equals (pip.ProductId)
                                select pip.ProductImage).FirstOrDefault();


            return ProductImage;
        }

    }
}