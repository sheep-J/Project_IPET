using Dapper;
using Project_IPET.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Services
{
    public class PetService : IPetService
    {

        private IDbConnection _dbConnection;
        public PetService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<CityModel> GetCities()
        {
            List<CityModel> result = new List<CityModel>();
            List<RegionModel> region = new List<RegionModel>();
            try
            {
                string sql = @"SELECT * FROM Cities";
                result = _dbConnection.Query<CityModel>(sql).ToList();

                string sql2 = @"SELECT * FROM Region";
                region = _dbConnection.Query<RegionModel>(sql2).ToList();
                foreach (var cities in result)
                {
                    cities.Regions = new List<RegionModel>();

                    var reg = region.Where(s => s.CityId == cities.CityID);
                    cities.Regions.AddRange(reg);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public PetListModel.Response GetPetList(PetListModel.Request request)
        {
            PetListModel.Response result = new PetListModel.Response()
            {
                PetList = new List<PetModel>(),
                Pagination = request.Pagination,
            };
            try
            {
                string sqlCount = @"SELECT COUNT(1) FROM Pets p
                                                JOIN Cities c ON p.PetCityID = c.CityID
                                                JOIN Region r ON p.PetRegionID = r.RegionID
                                                LEFT JOIN  PetImagePath pp ON p.PetID =pp.PetID
                                                WHERE pp.IsMainImage = 1 {0}";
                string sql = @"SELECT * FROM Pets p
                                                JOIN Cities c ON p.PetCityID = c.CityID
                                                JOIN Region r ON p.PetRegionID = r.RegionID
                                                LEFT JOIN  PetImagePath pp ON p.PetID =pp.PetID
                                                WHERE pp.IsMainImage = 1 {0}
                                                ORDER BY p.PetID OFFSET @PageSize*(@Page-1) ROWS FETCH NEXT @PageSize ROWS ONLY;";
                ; string where = "";
                if (request.CityID != -1)
                {
                    where += "AND p.PetCityID = @PetCityID ";
                }
                if (request.PetGender != "ALL")
                {
                    where += "AND p.PetGender = @PetGender ";
                }
                if (request.PetCategory != "ALL")
                {
                    where += "AND p.PetCategory = @PetCategory ";
                }
                var param = new
                {
                    PetCityID = request.CityID,
                    PetGender = request.PetGender,
                    PetCategory = request.PetCategory,
                    PageSize = request.Pagination.PageSize,
                    Page = request.Pagination.Page,
                };
                result.Pagination.TotalRecord = (int)_dbConnection.ExecuteScalar(string.Format(sqlCount, where), param);

                result.PetList = _dbConnection.Query<PetModel>(string.Format(sql, where), param).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public PetModel GetPet(int id)
        {
            PetModel result = new PetModel();
            try
            {
                string sql = @"SELECT * FROM Pets p
                                            JOIN Cities c ON p.PetCityID =c.CityID 
                                            JOIN Region r ON p.PetRegionID = r.RegionID 
                                            LEFT JOIN PetImagePath pp ON p.PetID =pp.PetID 
                                            WHERE p.PetID=@PetID";
                var param = new
                {
                    PetID = id,
                };

                var list = _dbConnection.Query<PetModel>(sql, param);
                result = list.FirstOrDefault();
                result.PetImages = list.Select(p => p.PetImage).ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 新增寵物認養資訊(INSERT)到DB
        /// </summary>
        /// <param name="petModel">填好資料後，要傳入一個petID給DB</param>
        public void CreatePet(PetModel petModel)
        {
            try
            {
                string sql = @"INSERT INTO  Pets 
                                            (PetName,PetVariety,PetCategory,PetGender,PetSize,PetColor,PetAge,PetFix,PetCityID,PetRegionID,PublishedDate,PetDescription,PetContact,PetContactPhone)
                                            OUTPUT INSERTED.PetID 
                                            VALUES 
                                            ( @PetName, @PetVariety, @PetCategory, @PetGender, @PetSize, @PetColor, @PetAge, @PetFix, @PetCityID, @PetRegionID, @PublishedDate, @PetDescription, @PetContact, @PetContactPhone ) ";
                string imageSql = @"INSERT INTO [dbo].[PetImagePath] ([PetID],[PetImage],[IsMainImage])
                                                        VALUES (@PetID,@PetImage,@IsMainImage)";
                //匿名類型
                var param = new
                {
                    PetName=petModel.PetName,
                    PetVariety=petModel.PetVariety,
                    PetCategory=petModel.PetCategory, 
                    PetGender=petModel.PetGender, 
                    PetSize=petModel.PetSize, 
                    PetColor=petModel.PetColor, 
                    PetAge = petModel.PetAge, 
                    PetFix = petModel.PetFix, 
                    PetCityID=petModel.PetCityID, 
                    PetRegionID=petModel.PetRegionID, 
                    PublishedDate=petModel.PublishedDate, 
                    PetDescription = petModel.PetDescription, 
                    PetContact = petModel.PetContact, 
                    PetContactPhone = petModel.PetContactPhone
                };

                petModel.PetID = _dbConnection.QuerySingle<int>(sql, param);

                for (int i = 0; i < petModel.PetImages.Count; i++)
                {
                    var paramImg = new
                    {
                        PetID = petModel.PetID,
                        PetImage = petModel.PetImages[i],
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

        public void EditPet(PetModel petModel)
        {
            PetModel result = new PetModel();
            try
            {

                string sql = @"UPDATE Pets 
                                            SET PetName=@PetName, PetVariety=@PetVariety, PetCategory=@PetCategory, PetGender=@PetGender, PetSize=@PetSize, 
                                                    PetColor=@PetColor, PetAge=@PetAge, PetFix=@PetFix, PetCityID=@PetCityID, PetRegionID=@PetRegionID, PublishedDate=@PublishedDate,
                                                    PetDescription=@PetDescription, PetContact=@PetContact, PetContactPhone=@PetContactPhone
                                            WHERE PetID=@PetID";

                string deleteImageSql = @"DELETE FROM PetImagePath
                                                                    WHERE PetID=@PetID";

                string imageSql = @"INSERT INTO [dbo].[PetImagePath] ([PetID],[PetImage],[IsMainImage])
                                                        VALUES (@PetID,@PetImage,@IsMainImage)";

                var param = new
                {
                    PetID = petModel.PetID,
                    PetName = petModel.PetName,
                    PetVariety = petModel.PetVariety,
                    PetCategory = petModel.PetCategory,
                    PetGender = petModel.PetGender,
                    PetSize = petModel.PetSize,
                    PetColor = petModel.PetColor,
                    PetAge = petModel.PetAge,
                    PetFix = petModel.PetFix,
                    PetCityID = petModel.PetCityID,
                    PetRegionID = petModel.PetRegionID,
                    PublishedDate = petModel.PublishedDate,
                    PetDescription = petModel.PetDescription,
                    PetContact = petModel.PetContact,
                    PetContactPhone = petModel.PetContactPhone
                };

                _dbConnection.Execute(sql, param);
                _dbConnection.Execute(deleteImageSql, param);

                for (int i = 0; i < petModel.PetImages.Count; i++)
                {
                    var paramImg = new
                    {
                        PetID = petModel.PetID,
                        PetImage = petModel.PetImages[i],
                        IsMainImage = i == 0
                    };
                    _dbConnection.Execute(imageSql, paramImg);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeletePet(int id)
        {
            PetModel result = new PetModel();
            try
            {
                string sql = @"DELETE FROM Pets 
                                            WHERE PetID=@PetID;
                                            DELETE FROM PetImagePath
                                            WHERE PetID=@PetID";
                //匿名類型
                var param = new
                {
                    PetID = id
                };

                _dbConnection.Execute(sql, param);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}