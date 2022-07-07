using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Services.Abstract.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
    }
}
