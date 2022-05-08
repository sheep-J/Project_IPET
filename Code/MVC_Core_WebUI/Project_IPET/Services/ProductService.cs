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
            ProductListResponseModel result = new ProductListResponseModel()
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
                                                LEFT JOIN  ProductImagePath pp ON p.ProductID =pp.ProductID
                                                JOIN Brand b ON p.BrandID = b.BrandID
                                                WHERE pp.IsMainImage = 1 ";

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
                result.Pagination.TotalRecord = (int)_dbConnection.ExecuteScalar(string.Format(sql, column), param); //拿第一個cell

                column = "p.*,sc.SubCategoryName,c.CategoryName,pp.ProductImage,b.BrandName";
                sql += " ORDER BY p.ProductID OFFSET @PageSize*(@Page-1) ROWS FETCH NEXT @PageSize ROWS ONLY;";

                result.ProductList = _dbConnection.Query<ProductModel>(string.Format(sql, column), param).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public List<CategoriesModel> GetCategories()
        {
            List<CategoriesModel> result = new List<CategoriesModel>();
            List<SubCategoriesModel> subCat = new List<SubCategoriesModel>();
            try
            {
                string sql = @"SELECT * FROM Categories";
                result = _dbConnection.Query<CategoriesModel>(sql).ToList();

                string sql2 = @"SELECT * FROM SubCategories";
                //List<SubCategoriesModel> subCat =new List<SubCategoriesModel>();
                subCat = _dbConnection.Query<SubCategoriesModel>(sql2).ToList();
                foreach (var categories in result)
                {
                    categories.SubCategories = new List<SubCategoriesModel>();
                    //寫法1
                    //把主分類用迴圈方式長出來讓子分類抓到CategoryId
                    //categories.SubCategories.AddRange(subCat.Where(s => s.CategoryId == categories.CategoryId));
                    //寫法2
                    //Step1. 取得subCat中所有CategoryId = categories(主分類)的CategoryId
                    var sub = subCat.Where(s => s.CategoryId == categories.CategoryId);
                    //Step2. 把子分類加入主分類下的List
                    categories.SubCategories.AddRange(sub);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public ProductModel GetProduct(int id)
        {
            ProductModel result = new ProductModel();
            try
            {
                string sql = @"SELECT p.*,pp.ProductImage,b.BrandName FROM Products p 
                                            JOIN Brand b ON p.BrandID=b.BrandID
                                            LEFT JOIN ProductImagePath pp ON p.ProductID =pp.ProductID 
                                            WHERE p.ProductID=@ProductID";
                //匿名類型
                var param = new
                {
                    ProductID = id,
                };
                var list = _dbConnection.Query<ProductModel>(sql, param);

                result = list.FirstOrDefault();
                result.ProductImages = list.Select(p => p.ProductImage).ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 新增產品(INSERT)到DB
        /// </summary>
        /// <param name="product">填好資料後，要傳入一個product給DB</param>
        public void CreateProduct(ProductModel product)
        {
            try
            {
                string sql = @"INSERT INTO  Products 
                                            (ProductName, SubCategoryID, BrandID, CostPrice, UnitPrice, UnitsInStock, Description, HotProduct, ProductAvailable)
                                            OUTPUT INSERTED.ProductID
                                            VALUES 
                                            (@ProductName, @SubCategoryID, @BrandID, @CostPrice, @UnitPrice, @UnitsInStock, @Description, @HotProduct, @ProductAvailable)";
                string imageSql = @"INSERT INTO [dbo].[ProductImagePath] ([ProductID],[ProductImage],[IsMainImage])
                                    VALUES (@ProductID,@ProductImage,@IsMainImage)";
                //匿名類型
                var param = new
                {
                    ProductName = product.ProductName,
                    SubCategoryID = product.SubCategoryID,
                    BrandID = product.BrandID,
                    CostPrice = product.CostPrice,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    Description = product.Description,
                    HotProduct = product.HotProduct,
                    ProductAvailable = product.ProductAvailable
                };
                //
                product.ProductID = _dbConnection.QuerySingle<int>(sql, param);

                for(int i = 0;i<product.ProductImages.Count; i++)
                {
                    var paramImg = new
                    {
                        ProductID = product.ProductID,
                        ProductImage = product.ProductImages[i],
                        IsMainImage = i == 0 // i == 0 ? true : false
                    };
                    _dbConnection.Execute(imageSql, paramImg);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<BrandModel> GetBrands()
        {
            List<BrandModel> result = new List<BrandModel>();
            try
            {
                string sql = @"SELECT * FROM Brand";
                result = _dbConnection.Query<BrandModel>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
