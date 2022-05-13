using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_IPET.Models;
using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjTest.Models
{
    public class CMembersFactory
    {
        private readonly MyProjectContext _context;

        public CMembersFactory(MyProjectContext context)
        {
            _context = context;
        }

        private static string getGenders(int gender)
        {
            string Gender = "";
            if (gender == 1)
                Gender = "Male";
            else if (gender == 2)
                Gender = "Female";
            else if (gender == 3)
                Gender = "Unknown";
            return Gender;
        }

        public int getMemberId(string json)
        {
            Member obj = JsonSerializer.Deserialize<Member>(json);
            int MemberId = obj.MemberId;
            return MemberId;
        }

        public int getRegionId(string regionName)
        {
            int regionId = _context.Regions.Where(r => r.RegionName == regionName)
                .Select(r => r.RegionId).FirstOrDefault();
            return regionId;
        }

        public int getGender(string gender)
        {
            int Gender = 0;
            if (gender == "男")
                Gender = 1;
            else if (gender == "女")
                Gender = 2;
            else if (gender == "不願透露")
                Gender = 3;
            return Gender;
        }

        public IEnumerable<CBackMembersViewModel> memberFilter(CMembersFilter vModel)
        {
            IEnumerable<CBackMembersViewModel> datas = null;
            datas = _context.Members.Select(m => new CBackMembersViewModel
            {
                Name = m.Name,
                Email = m.Email,
                UserId = m.UserId,
                Gender = getGenders(m.Gender),
                BirthDate = m.BirthDate.Date.ToString("yyyy/MM/dd"),
                Phone = m.Phone,
                Address = m.Region.City.CityName + m.Region.RegionName + " " + m.Address,
                RegisteredDate = m.RegisteredDate.ToString("yyyy/MM/dd"),
            });
            //=================== begin filter ===================

            if (!string.IsNullOrEmpty(vModel.Keyword))
                datas = datas
                   .Where(m => m.Name.Contains(vModel.Keyword) || m.Email.Contains(vModel.Keyword) || m.UserId.Contains(vModel.Keyword));

            if (vModel.Male == true)
            {
                datas = datas
                      .Where(m => m.Gender == "Male");
                if(vModel.Female)
                vModel.Female = false;
            }

            if (vModel.Female == true)
            {
                datas = datas
                    .Where(m => m.Gender == "Female");
                if (vModel.Male)
                    vModel.Male = false;
            }

            return datas.Distinct().ToList();
        }

    }
}
