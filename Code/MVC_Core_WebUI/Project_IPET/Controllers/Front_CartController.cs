using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_IPET.Helpers;
using Project_IPET.Models.Cart;
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
            List<CartItem> CartItems = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");

            if (CartItems != null)
            {
                ViewBag.Count = CartItems.Count();     // 計算購物車商品數 (種類)
                ViewBag.Total = CartItems.Sum(item => item.SubTotal);       // 計算購物車總金額
            }
            else
            {
                ViewBag.Count = 0;
                ViewBag.Total = 0;
            }

            return View(CartItems);
        }

        public IActionResult AddToCart(int id)
        {

            var product = _context.Products.Single(m => m.ProductId.Equals(id));

            // TODO 優化計算項目
            CartItem item = new CartItem()
            {
                Id = product.ProductId,
                Product = product,
                Name = product.ProductName,
                Category = GetCategoryName(product.SubCategoryId),
                UnitPrice = product.UnitPrice,
                Quantity = 1,
                SubTotal = product.UnitPrice,
                imageSrc = ConvertImage(GetProductImage(product.ProductId))
            };

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart") == null)
            {
                // 如果沒有已存在購物車, 則建立新的購物車
                List<CartItem> cart = new List<CartItem>();
                cart.Add(item);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            }
            else
            {
                // 如果已存在購物車, 檢查有無相同的商品 , 有的話只調整數量
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");

                int index = cart.FindIndex(m => m.Product.ProductId.Equals(id));

                if (index != -1)
                {
                    cart[index].Quantity += 1;
                    cart[index].SubTotal += product.UnitPrice;
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
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");
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
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "Cart");
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