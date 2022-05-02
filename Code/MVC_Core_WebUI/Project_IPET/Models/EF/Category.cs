using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte[] CategoryPicture { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
