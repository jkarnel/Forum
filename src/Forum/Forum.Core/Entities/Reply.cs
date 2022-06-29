using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Core.Entities
{
    public class Reply
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }
    }
}
