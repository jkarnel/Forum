using Forum.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data
{
    public class ForumDbContext : IdentityDbContext<User>
    {
    }
}
