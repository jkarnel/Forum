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
    /// <see cref="IReplyService"/>
    /// </summary>
    public class ReplyService : IReplyService
    {
        private readonly ForumDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Database context</param>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="dbContext"/> is null</exception>
        public ReplyService(ForumDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// <see cref="IReplyService.SaveReplyAsync(ReplyDTO)"/>
        /// </summary>
        public async Task<Reply> SaveReplyAsync(ReplyDTO reply)
        {
            Reply result = null;

            if(reply.Id == 0)
            {
                Reply newReply = new Reply
                {
                    Description = reply.Description,
                    AuthorId = reply.AuthorId,
                    PostId = reply.PostId,
                    CreatedOn = DateTime.UtcNow
                };

                await _dbContext.Replies.AddAsync(newReply);

                result = newReply;
            }
            else
            {
                Reply replyToChange = this.GetReplyById(reply.Id);
                replyToChange.Description = reply.Description;
                replyToChange.LastModifiedOn = DateTime.UtcNow;

                result = replyToChange;
            }
            
            await _dbContext.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// <see cref="IReplyService.GetReplyById(int)"/>
        /// </summary>
        public Reply GetReplyById(int replyId)
        {
            return _dbContext.Replies.Where(x => x.Id == replyId).FirstOrDefault();
        }
    }
}
