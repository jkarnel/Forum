using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Core.Entities
{
    public class Reply
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public Post ParentPost { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public User Author { get; set; }
    }
}
