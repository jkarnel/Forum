using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Core.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public ICollection<Reply> Replies { get; set; } = new HashSet<Reply>();
    }
}
