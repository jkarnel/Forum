using System.ComponentModel.DataAnnotations;

namespace Forum.Web.Models.Post
{
    public class PostInputViewModel
    {
        private const int PostTitleMaxLength = 128;
        private const int PostTitleMinLength = 4;

        private const int PostDescriptionMaxLength = 32768;

        private const string PostTitleLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public int Id { get; set; }

        [Required]
        [StringLength(PostTitleMaxLength, ErrorMessage = PostTitleLengthErrorMessage, MinimumLength = PostTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(PostDescriptionMaxLength)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
