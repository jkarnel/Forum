using Forum.Web.Models.Reply;
using System.Collections.Generic;

namespace Forum.Web.Models.Post
{
    public class PostDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string CreatedOn { get; set; }

        public IEnumerable<ReplyViewModel> Replies { get; set; }
    }
}
