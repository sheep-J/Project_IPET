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

        public List<CityModel> GetCityList()
        {
            string sql = "SELECT * FROM Cities";
            var cityList = _dbConnection.Query<CityModel>(sql);
            return cityList.ToList();
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
                                                LEFT JOIN  PetImagePath pp ON p.PetID =pp.PetID
                                                WHERE pp.IsMainImage = 1 {0}";
                string sql = @"SELECT * FROM Pets p
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

    }
}