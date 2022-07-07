using Forum.Core.Entities;
using Forum.Services.Abstract.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Services.Abstract
{
    /// <summary>
    /// Service that is performing actions under <see cref="Post" /> entity
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Gets all posts available posts from repository according to search and paging parameters
        /// </summary>
        /// <param name="searchStatement">Search phrase to seach it in Post.Title property</param>
        /// <param name="pageIndex">Index of current page</param>
        /// <param name="count">Posts count on the page</param>
        /// <returns>List of posts</returns>
        /// <remarks>Search feature is implemented in a very simple way, instead full text search should be used</remarks>
        IEnumerable<Post> GetPosts(string searchStatement, int pageIndex, int count);

        /// <summary>
        /// Get post entity by id
        /// </summary>
        /// <param name="postId">Post id</param>
        /// <returns>Post entity</returns>
        Post GetPostById(int postId);

        /// <summary>
        /// Creates new post (if Id = 0) or saves changed one
        /// </summary>
        /// <param name="post">Post DTO</param>
        /// <returns>Created or changed post</returns>
        Task<Post> SavePostAsync(PostDTO post);
    }
}
