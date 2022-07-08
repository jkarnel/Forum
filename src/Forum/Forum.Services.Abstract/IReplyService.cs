using Forum.Core.Entities;
using Forum.Services.Abstract.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Services.Abstract
{
    /// <summary>
    /// Service that is performing actions under <see cref="Reply" /> entity
    /// </summary>
    public interface IReplyService
    {
        /// <summary>
        /// Get reply entity by id
        /// </summary>
        /// <param name="replyId">Reply id</param>
        /// <returns>Reply entity</returns>
        Reply GetReplyById(int replyId);

        /// <summary>
        /// Creates new reply (if Id = 0) or saves changed one
        /// </summary>
        /// <param name="reply">Reply DTO</param>
        /// <returns>Created or changed reply</returns>
        Task<Reply> SaveReplyAsync(ReplyDTO reply);

        /// <summary>
        /// Deletes reply by id
        /// </summary>
        /// <param name="postId">Reply id</param>
        /// <returns></returns>
        Task DeleteReplyAsync(int replyId);
    }
}
