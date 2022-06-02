using Project_IPET.Models;
using System.Collections.Generic;

namespace Project_IPET.Services
{
    public interface IProductService
    {
        //前面的Model(Class)代表要回傳(return)的型別
        ProductListResponseModel GetProductList(ProductListRequestModel request);
        List<CategoriesModel> GetCategories();
        ProductModel GetProduct(int id);
        void CreateProduct(ProductModel product);
        List<BrandModel> GetBrands();
        void EditProduct(ProductModel product);
        void DeleteProduct(int id);
        /// <summary>
        /// 拿到捐贈商品獨立顯示在ProductList頁面
        /// </summary>
        ProductListResponseModel GetPrjProductList(ProductListRequestModel request);

    }
}
