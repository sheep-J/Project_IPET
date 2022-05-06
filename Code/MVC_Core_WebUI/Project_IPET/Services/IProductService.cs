using Project_IPET.Models;
using System.Collections.Generic;

namespace Project_IPET.Services
{
    public interface IProductService
    {
        ProductListResponseModel GetProductList(ProductListRequestModel request);
        List<CategoriesModel> GetCategories();
        ProductModel GetProduct(int id);
        BrandModel GetBrands(int id);

    }
}
