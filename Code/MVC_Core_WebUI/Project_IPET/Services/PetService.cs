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

        public PetListModel.Response GetPetList(PetListModel.Request request)
        {
            PetListModel.Response result = new PetListModel.Response()
            {
                PetList = new List<PetModel>(),
                Pagination = request.Pagination,
            };
            try
            {
                string column = "COUNT(1)";
                string sql = @"SELECT {0}
                                                FROM Pets p
                                                JOIN Cities c ON p.PetCityID =c.CityID 
                                                JOIN Region r ON p.PetRegionID = r.RegionID 
                                                JOIN (SELECT PetID, MIN(PetImage) PetImage FROM PetImagePath GROUP BY PetID) pip ON p.PetID =pip.PetID
                                                WHERE 1=1";
                var param = new
                {
                    PageSize = request.Pagination.PageSize,
                    Page = request.Pagination.Page,
                };
                result.Pagination.TotalRecord = (int)_dbConnection.ExecuteScalar(string.Format(sql, column), param);

                column = "p.*,c.CityName,r.RegionName,pip.PetImage";
                sql += " ORDER BY p.PetID OFFSET @PageSize*(@Page-1) ROWS FETCH NEXT @PageSize ROWS ONLY;";

                result.PetList = _dbConnection.Query<PetModel>(string.Format(sql, column), param).ToList();
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
                                            JOIN PetImagePath pip ON p.PetID =pip.PetID
                                            WHERE p.PetID=@PetID ";
                var param = new
                {
                    PetID = id,
                };
                result = _dbConnection.QueryFirst<PetModel>(sql, param);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

    }
}