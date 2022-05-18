using Project_IPET.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Models
{
    public class PetModel
    {
        /// <summary>
        /// 寵物編號
        /// </summary>
        public int PetID { get; set; }
        /// <summary>
        /// 寵物名稱
        /// </summary>
        public string PetName { get; set; }
        /// <summary>
        /// 寵物品種
        /// </summary>
        public string PetVariety { get; set; }
        /// <summary>
        /// 寵物種類
        /// </summary>
        public string PetCategory { get; set; }
        /// <summary>
        /// 寵物性別
        /// </summary>
        public string PetGender { get; set; }
        /// <summary>
        /// 寵物體型
        /// </summary>
        public string PetSize { get; set; }
        /// <summary>
        /// 寵物毛色
        /// </summary>
        public string PetColor { get; set; }
        /// <summary>
        /// 寵物年紀
        /// </summary>
        public string PetAge { get; set; }
        /// <summary>
        /// 寵物結紮
        /// </summary>
        public string PetFix { get; set; }
        /// <summary>
        /// 刊登日期
        /// </summary>
        public string PublishedDate { get; set; }
        /// <summary>
        /// 寵物資訊
        /// </summary>
        public string PetDescription { get; set; }
        /// <summary>
        /// 聯絡人
        /// </summary>
        public string PetContact { get; set; }
        /// <summary>
        /// 聯絡電話
        /// </summary>
        public string PetContactPhone { get; set; }
        /// <summary>
        /// 縣市名稱
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 鄉鎮名稱
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 年紀,體型,品種
        /// </summary>
        public string Detail
        {
            get
            {
                return this.PetAge + this.PetSize + this.PetVariety;
            }
        }

        public string PublishedDateFormat
        {
            get
            {
                return this.PublishedDate.Substring(0, 4) + "/" + this.PublishedDate.Substring(4, 2) + "/" + this.PublishedDate.Substring(6, 2);
            }
        }

        public string CityRegion
        {
            get
            {
                return this.CityName + " " + this.RegionName;
            }
        }

        /// <summary>
        /// 寵物照片
        /// </summary>
        public byte[] PetImage { get; set; }

        public string PetImageBase64String
        {
            get
            {
                return PetImage == null ? "" : "data:image/jpg;base64," + Convert.ToBase64String(PetImage, 0, PetImage.Length);
            }
        }

        public List<byte[]> PetImages { get; set; }

        public List<string> PetImageBase64Strings
        {
            get
            {
                return PetImages == null ? new List<string>() : PetImages.Select(PetImage => "data:image/jpg;base64," + Convert.ToBase64String(PetImage, 0, PetImage.Length)).ToList();
            }
        }
    }
}