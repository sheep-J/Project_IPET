using System;

namespace Project_IPET.Models
{
    public class PageModel
    {
        /// <summary>
        /// 現在是第幾頁
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 一頁有幾個
        /// </summary>
        public int PageSize { get; set; }
        /*
        public int TotalPage
        {
            get
            {
                return Page / PageSize + Page % PageSize > 0 ? 1 : 0;
            }
        }
        */
        /// <summary>
        /// 無條件進位換算總頁數
        /// </summary>
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling(TotalRecord / (double)PageSize);
            }
        }
        /// <summary>
        /// 總共有幾筆資料
        /// </summary>
        public int TotalRecord { get; set; }
    }
}
