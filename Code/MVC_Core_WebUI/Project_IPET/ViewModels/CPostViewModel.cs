namespace Project_IPET.ViewModels
{
    public class CPostViewModel
    {
        
        public int PostId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public string PostDate { get; set; }
        public int LikeCount { get; set; }
        public string PostImage { get; set; }
        public string Tag { get; set; }
        public string MemberName { get; set; }
        public int MemberId { get; set; }
        public int ReplyConut { get; set; }
        public string PostType { get; set; }
    }
}
