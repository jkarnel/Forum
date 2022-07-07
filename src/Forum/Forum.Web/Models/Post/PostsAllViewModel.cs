using System.Collections.Generic;

namespace Forum.Web.Models.Post
{
    public class PostsAllViewModel
    {
        public IEnumerable<PostItemViewModel> Posts { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int NextPage
        {
            get
            {
                if (PageIndex >= TotalPages)
                    return 1;

                return PageIndex + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (PageIndex <= 1)
                    return TotalPages;

                return PageIndex - 1;
            }
        }
    }
}
