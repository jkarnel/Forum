using System;

namespace Forum.Web.Models.Reply
{
    public class ReplyViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorName { get; set; }
    }
}
