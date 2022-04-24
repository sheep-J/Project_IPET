using Dapper;
using Project_IPET.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Project_IPET.Services
{
    public class ProductService : IProductService
    {
        private IDbConnection _dbConnection;
        public ProductService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public ProductListResponseModel GetProductList(ProductListRequestModel request)
        {
            ProductListResponseModel result =new ProductListResponseModel()
            {
                ProductList = new List<ProductModel>(),
                Pagination = request.Pagination,
            };
            try
            {              
                string column = "COUNT(1)";
                string sql = @"SELECT {0}
                                                FROM Products p 
                                                JOIN SubCategories sc ON p.SubCategoryID =sc.SubCategoryID 
                                                JOIN Categories c ON sc.CategoryID = c.CategoryID 
                                                JOIN  ProductImagePath pp ON p.ProductID =pp.ProductID
                                                WHERE 1=1";

                if (request.CategoryId != -1)
                {
                    sql += " AND c.CategoryID = @CategoryID";
                }
                var param = new
                {
                    CategoryID = request.CategoryId,
                    PageSize = request.Pagination.PageSize,
                    Page = request.Pagination.Page,
                };
                result.Pagination.TotalRecord = (int)_dbConnection.ExecuteScalar(string.Format(sql,column), param);

                column = "p.*,sc.SubCategoryName,c.CategoryName,pp.ProductImage";
                sql += " ORDER BY p.ProductID OFFSET @PageSize*(@Page-1) ROWS FETCH NEXT @PageSize ROWS ONLY;";
                
                result.ProductList =  _dbConnection.Query<ProductModel>(string.Format(sql, column), param).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
