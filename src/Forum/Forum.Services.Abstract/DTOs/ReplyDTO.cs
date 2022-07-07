using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Services.Abstract.DTOs
{
    public class ReplyDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public int PostId { get; set; }
    }
}
