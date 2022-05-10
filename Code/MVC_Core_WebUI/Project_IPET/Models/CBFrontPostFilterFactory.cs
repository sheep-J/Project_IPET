using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_IPET.Models
{
    public class CBFrontPostFilterFactory
    {
        private readonly MyProjectContext _context;

       

        public CBFrontPostFilterFactory(MyProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<CPostViewModel> PostFilter(PostFilterModel PostFilters)
        {
            IEnumerable<CPostViewModel> datas = null;
            datas = _context.Posts.Select(n => new CPostViewModel
            {
                PostId = n.PostId,
                Title = n.Title,
                PostContent = n.PostContent,
                PostDate = n.PostDate,
                LikeCount = n.LikeCount,
                PostImage = n.PostImage,
                MemberName = n.Member.Name,
                MemberId = n.MemberId,
                PostType = n.PostType.PostTypeName,
                ReplyToPost =n.ReplyToPost.ToString(),
             
            }).ToList();



            if (!string.IsNullOrEmpty(PostFilters.Keyword)) 
            {
                datas = datas.Where(p => p.MemberName == PostFilters.Keyword)
                             .Where(p => p.PostType == PostFilters.Keyword)
                             .Where(p => p.PostDate == PostFilters.Keyword)
                             .Where(p => p.PostContent == PostFilters.Keyword)
                             .Where(p => p.Tag == PostFilters.Keyword)
                             .Where(p => p.Title == PostFilters.Keyword)
                             .Select(p => p);
            
            }

            if (!string.IsNullOrEmpty(PostFilters.PostType))
            {
                datas = datas.Where(p => p.PostType.ToString() == PostFilters.PostType).Select(p => p);
            }

            if (!string.IsNullOrEmpty(PostFilters.PostFristDate) && !string.IsNullOrEmpty(PostFilters.PostLastDate))
            {

                datas = datas.Where(p => DateTime.ParseExact(p.PostDate, "yyyy/mm/dd :G(zh-TW)", null) >= DateTime.ParseExact(PostFilters.PostFristDate, "yyyy/mm/dd :G(zh-TW)", null)
                                      && DateTime.ParseExact(p.PostDate, "yyyy/mm/dd :G(zh-TW)", null) <= DateTime.ParseExact(PostFilters.PostLastDate, "yyyy/mm/dd :G(zh-TW)", null))
                             .Select(p => p);
            
            }

            if (!string.IsNullOrEmpty(PostFilters.Tag))
            {
                datas = datas.Where(p => p.Tag == PostFilters.Tag).Select(p => p);
            }
            
            return datas.OrderByDescending(d=>d.PostDate);
        }


















    }
}
