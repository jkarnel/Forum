using System;

namespace Forum.Web.Models.Reply
{
    public class ReplyViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatedOn { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
