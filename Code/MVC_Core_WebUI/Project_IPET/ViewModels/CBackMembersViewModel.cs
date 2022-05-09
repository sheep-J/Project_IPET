using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.ViewModels
{
    public class CBackMembersViewModel
    {
        //#region get set
        //private Member _member;
        //private City _city;
        //private Region _region;
        //public CBackMembersViewModel()
        //{
        //    _member = new Member();
        //    _city = new City();
        //    _region = new Region();
        //}
        //public Member member
        //{
        //    get { return _member; }
        //    set { _member = value; }
        //}
        //public City city
        //{
        //    get { return _city; }
        //    set { _city = value; }
        //}
        //public Region region
        //{
        //    get { return _region; }
        //    set { _region = value; }
        //}

        //public string Name 
        //{ 
        //    get { return _member.Name; }
        //    set { _member.Name = value; } 
        //}
        //public string Email {
        //    get { return _member.Email; }
        //    set { _member.Email = value; }
        //}
        //public string UserId
        //{
        //    get { return _member.UserId; }
        //    set { _member.UserId = value; }
        //}
        //public int Gender
        //{
        //    get { return _member.Gender; }
        //    set 
        //    {
        //        //string male = "Male"; string female = "Female";
        //        //_member.Gender = _member.Gender == 1 ? male = value : female = value;
        //        _member.Gender = value;
        //    }
        //}
        //public DateTime BirthDate
        //{
        //    get { return _member.BirthDate; }
        //    set { _member.BirthDate = value; }
        //}
        //public string Phone 
        //{
        //    get { return _member.Phone; }
        //    set { _member.Phone = value; }
        //}
        //public string Address 
        //{
        //    get { return _member.Address; }
        //    set { 
        //        _city.CityName = value;
        //        _region.RegionName += value;
        //        _member.Address += value;
        //    }
        //}
        //public DateTime RegisteredDate
        //{
        //    get { return _member.RegisteredDate; }
        //    set { _member.RegisteredDate = value; }
        //}
        //#endregion

        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string RegisteredDate { get; set; }

    }
}
