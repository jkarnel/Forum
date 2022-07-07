using Forum.Core.Entities;
using Forum.Data;
using Forum.Services.Abstract;
using Forum.Services.Abstract.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Services
{
    /// <summary>
    /// <see cref="IPostService" />
    /// </summary>
    public class PostService : IPostService
    {
        private readonly ForumDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Database context</param>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="dbContext"/> is null</exception>
        public PostService(ForumDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// <see cref="IPostService.SavePostAsync(PostDTO)"/>
        /// </summary>
        public async Task<Post> SavePostAsync(PostDTO post)
        {
            Post result = null;

            if(post.Id == 0)
            {
                Post newPost = new Post()
                {
                    Title = post.Title,
                    Description = post.Description,
                    AuthorId = post.AuthorId,
                    CreatedOn = DateTime.UtcNow
            };

                await _dbContext.Posts.AddAsync(newPost);

                result = newPost;
            }
            else
            {
                var postToChange = this.GetPostById(post.Id);
                if(postToChange != null)
                {
                    postToChange.Title = post.Title;
                    postToChange.Description = post.Description;
                    postToChange.LastModifiedOn = DateTime.UtcNow;
                }

                result = postToChange;
            }
            
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// <see cref="IPostService.GetPostById(int)"/>
        /// </summary>
        public Post GetPostById(int postId)
        {
            return _dbContext.Posts.Where(x => x.Id == postId).FirstOrDefault();
        }

        /// <summary>
        /// <see cref="IPostService.GetPosts(string, int, int)"/>
        /// </summary>
        public IEnumerable<Post> GetPosts(string searchStatement, int pageIndex, int countOnPage)
        {
            IQueryable<Post> postsQuery = _dbContext.Posts;
            if (searchStatement != null)
            {
                //TODO: apply full text search feature  
                postsQuery = postsQuery
                    .Where(x => x.Title.Contains(searchStatement));
            }

            postsQuery = postsQuery.Skip(pageIndex * countOnPage)
                                   .Take(countOnPage);

            return postsQuery.AsEnumerable();
        }
    }
}
