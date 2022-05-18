using System;
using System.Collections.Generic;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class PetImagePath
    {
        public int PetImagePathId { get; set; }
        public int PetId { get; set; }
        public byte[] PetImage { get; set; }
        public string PetImagePath1 { get; set; }
        public bool? IsMainImage { get; set; }

        public virtual Pet Pet { get; set; }
    }
}
