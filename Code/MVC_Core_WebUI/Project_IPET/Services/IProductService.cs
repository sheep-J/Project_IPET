using Project_IPET.Models;
using System.Collections.Generic;

namespace Project_IPET.Services
{
    public interface IProductService
    {
        ProductListResponseModel GetProductList(ProductListRequestModel request);
        
    }
}
