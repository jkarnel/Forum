namespace Forum.Web.Models.Post
{
    public class PostItemViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RepliesCount { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }
    }
}
