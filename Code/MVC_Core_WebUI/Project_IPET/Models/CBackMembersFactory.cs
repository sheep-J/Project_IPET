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
    public class CBackMembersFactory
    {
        private readonly MyProjectContext _context;

        public CBackMembersFactory(MyProjectContext context)
        {
            _context = context;
        }

        public int getMemberId(string json)
        {
            Member obj = JsonSerializer.Deserialize<Member>(json);
            int MemberId = obj.MemberId;
            return MemberId;
        }

        public IEnumerable<CBackMembersViewModel> memberFilter(string keyword,bool gender)
        {
            IEnumerable<CBackMembersViewModel> datas = null;
            datas = _context.Members.Select(m => new CBackMembersViewModel
            {
                Name = m.Name,
                Email = m.Email,
                UserId = m.UserId,
                Gender = m.Gender == 1 ? "Male" : "Female",
                BirthDate = m.BirthDate.Date.ToString("yyyy/MM/dd"),
                Phone = m.Phone,
                Address = m.Region.City.CityName + m.Region.RegionName + " " + m.Address,
                RegisteredDate = m.RegisteredDate.ToString("yyyy/MM/dd"),
            });
            //=================== begin filter ===================

            if (string.IsNullOrEmpty(keyword))
                datas = datas.Distinct().ToList();

            if(!string.IsNullOrEmpty(keyword))
                datas = datas
                   .Where(m => m.Name.Contains(keyword) || m.Email.Contains(keyword)).Distinct().ToList();

            if (gender == true)
                datas = datas
                    .Where(m => m.Gender == "Male").Distinct().ToList();

            if (gender == false)
                datas = datas
                    .Where(m => m.Gender == "Female").Distinct().ToList();

            return datas;
        }

    }
}
