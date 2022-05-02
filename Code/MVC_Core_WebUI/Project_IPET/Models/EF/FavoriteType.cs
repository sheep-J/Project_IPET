using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class FavoriteType
    {
        public FavoriteType()
        {
            MyFavorites = new HashSet<MyFavorite>();
        }

        public int FavoriteTypeId { get; set; }
        public string FavoriteTypeName { get; set; }

        public virtual ICollection<MyFavorite> MyFavorites { get; set; }
    }
}
