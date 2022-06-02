﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Member
    {
        public Member()
        {
            MyFavorites = new HashSet<MyFavorite>();
            Orders = new HashSet<Order>();
            PostLikeds = new HashSet<PostLiked>();
            Posts = new HashSet<Post>();
        }

        public int MemberId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? RegionId { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Avatar { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int RoleId { get; set; }
        public bool Banned { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<MyFavorite> MyFavorites { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PostLiked> PostLikeds { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
