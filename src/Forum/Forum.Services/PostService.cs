using Forum.Core.Entities;
using Forum.Data;
using Forum.Services.Abstract;
using Forum.Services.Abstract.DTOs;
using Microsoft.EntityFrameworkCore;
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

                await _dbContext.Posts.AddAsync(newPost).ConfigureAwait(false);

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
            return _dbContext.Posts.Where(x => x.Id == postId)
                .Include(x => x.Author)
                .Include(x => x.Replies)
                .FirstOrDefault();
        }

        /// <summary>
        /// <see cref="IPostService.GetPosts(string, int, int)"/>
        /// </summary>
        public IEnumerable<Post> GetPosts(int pageIndex, int countOnPage)
        {
            IQueryable<Post> postsQuery = _dbContext.Posts;

            postsQuery = postsQuery.Skip((pageIndex - 1) * countOnPage)
                                   .Take(countOnPage)
                                   .Include(x => x.Author)
                                   .Include(x => x.Replies);

            return postsQuery.AsEnumerable();
        }

        /// <summary>
        /// <see cref="IPostService.DeletePostAsync(int)"/>
        /// </summary>
        public async Task DeletePostAsync(int postId)
        {
            Post postToDelete = GetPostById(postId);
            _dbContext.Posts.Remove(postToDelete);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
