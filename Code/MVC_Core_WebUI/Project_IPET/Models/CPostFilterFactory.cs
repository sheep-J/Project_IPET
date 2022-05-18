using Project_IPET.Models.EF;
using Project_IPET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_IPET.Models
{
    public class CPostFilterFactory
    {
        private readonly MyProjectContext _context;

        public CPostFilterFactory(MyProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<CPostViewModel> PostOrderByYear(int year)
        {
            IEnumerable<CPostViewModel> Post = null;
                Post = _context.Posts
                       .Select(p => new CPostViewModel
                       {
                           Title = p.Title,
                           PostImage = p.PostImage,
                           ReplyToPost = p.ReplyToPost.ToString(),
                           PostContent = p.PostContent.Substring(0, 15),
                           PostId = p.PostId,
                           PostDate = p.PostDate,
                       }).Where(c => c.ReplyToPost == null);

                Post = Post.Where(p => DateTime.Parse(p.PostDate).Year.Equals(year))
                      .Select(p => p);

            return Post.OrderByDescending(p => DateTime.Parse(p.PostDate)).Take(3);
        }


        public IEnumerable<CPostViewModel> PostFilter(CPostViewModel PostFilters)
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
                PostTypeId = n.PostTypeId,
                PostType = n.PostType.PostTypeName,
                ReplyToPost = n.ReplyToPost.ToString(),

            }).Where(c => c.ReplyToPost == null);

            if (PostFilters != null)
            {

                if (PostFilters.FilterKeyword != null)
                {
                    datas = datas.Where(p => p.MemberName.Contains(PostFilters.FilterKeyword) ||
                                             p.PostType.Contains(PostFilters.FilterKeyword) ||
                                             p.PostDate.Contains(PostFilters.FilterKeyword) ||
                                             p.PostContent.Contains(PostFilters.FilterKeyword) ||
                                             p.Title.Contains(PostFilters.FilterKeyword)
                                       )
                                 .Select(p => p);

                }

                if (PostFilters.FilterPostType != null)
                {
                    datas = datas.Where(p => p.PostType.ToString() == PostFilters.FilterPostType).Select(p => p);
                }

                if (PostFilters.FilterPostFristDate != null && PostFilters.FilterPostLastDate != null)
                {

                    datas = datas.Where(p => DateTime.Parse(p.PostDate) > PostFilters.FilterPostFristDate
                                          && DateTime.Parse(p.PostDate).AddDays(-1) <= PostFilters.FilterPostLastDate)
                                 .Select(p => p);

                }

                if (PostFilters.FilterTag != null)
                {
                    datas = datas.Where(p => p.Tag == PostFilters.FilterTag).Select(p => p);
                }
            }

            return datas.ToList().OrderByDescending(p => DateTime.Parse(p.PostDate));

        }


















    }
}
