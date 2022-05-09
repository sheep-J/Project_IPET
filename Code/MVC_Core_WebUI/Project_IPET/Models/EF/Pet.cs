using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class Pet
    {
        public Pet()
        {
            PetImagePaths = new HashSet<PetImagePath>();
        }

        public int PetId { get; set; }
        public string PetName { get; set; }
        public string PetVariety { get; set; }
        public string PetCategory { get; set; }
        public string PetGender { get; set; }
        public string PetSize { get; set; }
        public string PetColor { get; set; }
        public string PetAge { get; set; }
        public string PetFix { get; set; }
        public int PetCityId { get; set; }
        public int PetRegionId { get; set; }
        public string PublishedDate { get; set; }
        public string PetDescription { get; set; }
        public string PetContact { get; set; }
        public string PetContactPhone { get; set; }

        public virtual City PetCity { get; set; }
        public virtual Region PetRegion { get; set; }
        public virtual ICollection<PetImagePath> PetImagePaths { get; set; }
    }
}
