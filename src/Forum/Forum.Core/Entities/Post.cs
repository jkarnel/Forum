using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<Reply> Replies { get; set; }

    }
}
