using Forum.Core.Entities;
using Forum.Services.Abstract;
using Forum.Services.Abstract.DTOs;
using Forum.Web.Models.Reply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class ReplyController : Controller
    {
        private readonly IReplyService _replyService;
        private readonly ILogger<PostController> _logger;

        public ReplyController(IReplyService replyService, ILogger<PostController> logger)
        {
            _replyService = replyService ?? throw new ArgumentNullException(nameof(replyService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReplyInputViewModel input)
        {
            await _replyService.SaveReplyAsync(new ReplyDTO
            {
                Description = input.Text,
                PostId = input.PostId,
                AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });

            return RedirectToAction("Index", "Post");
        }
        
        [HttpGet]
        public IActionResult Edit(int replyId)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Reply replyToEdit = _replyService.GetReplyById(replyId);
            if(replyToEdit == null)
            {
                //return error that reply was not found
            }
            if(replyToEdit.AuthorId != currentUserId)
            {
                //return error that user don`t have rights to edit this reply
            }

            return this.View(new ReplyInputViewModel()
            {
                Id = replyId,
                PostId = replyToEdit.PostId,
                Text = replyToEdit.Description
            });
        }

        [HttpPost]
        public IActionResult Edit(ReplyInputViewModel replyInputModel)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Reply replyToEdit = _replyService.GetReplyById(replyInputModel.Id);
            if (replyToEdit == null)
            {
                //return error that reply was not found
            }
            if (replyToEdit.AuthorId != currentUserId)
            {
                //return error that user don`t have rights to edit this reply
            }

            _replyService.SaveReplyAsync(new ReplyDTO
            {
                Description = replyInputModel.Text
            });

            return RedirectToAction("View", "Post", new {postId = replyInputModel.PostId });
        }
    }
}
