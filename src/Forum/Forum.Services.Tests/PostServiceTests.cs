using Forum.Core.Entities;
using Forum.Data;
using Forum.Services.Abstract.DTOs;
using Forum.Tests.Utilities;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services.Tests
{
    public class PostServiceTests
    {
        private ForumDbContext _forumDbContext;

        [SetUp]
        public void Setup()
        {
            _forumDbContext = ForumDbContextUtility.GetInMemoryDBContext();
        }

        [Test]
        public async Task TestCreateNewPost_Success()
        {
            PostService postService = new PostService(_forumDbContext);
            PostDTO post = new PostDTO()
            {
                Title = "TestPostTitle",
                Description = "TestPostDescription",
                AuthorId = "user1",
            };

            int countBeforeCall = _forumDbContext.Posts.Count();

            Post createdPost = await postService.SavePostAsync(post);

            int countAfterCall = _forumDbContext.Posts.Count();

            Assert.That(countBeforeCall, Is.EqualTo(0));
            Assert.That(countAfterCall, Is.EqualTo(1));

            Assert.That(createdPost.Id, Is.GreaterThan(0));
            Assert.That(createdPost.Title, Is.EqualTo("TestPostTitle"));
            Assert.That(createdPost.Description, Is.EqualTo("TestPostDescription"));
            Assert.That(createdPost.AuthorId, Is.EqualTo("user1"));
        }
    }
}