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

        [HttpGet]
        public IActionResult Create(int postId)
        {
            return View(new ReplyInputViewModel() { PostId = postId });
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

            return RedirectToAction("Details", "Post", new { id = input.PostId });
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Reply replyToEdit = _replyService.GetReplyById(id);
            if (replyToEdit == null)
                return NotFound();

            if (replyToEdit.AuthorId != currentUserId)
                return Unauthorized();

            return this.View(new ReplyInputViewModel()
            {
                Id = id,
                PostId = replyToEdit.PostId,
                Text = replyToEdit.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReplyInputViewModel replyInputModel)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Reply replyToEdit = _replyService.GetReplyById(replyInputModel.Id);
            if (replyToEdit == null)
                return NotFound();

            if (replyToEdit.AuthorId != currentUserId)
                return Unauthorized();

            await _replyService.SaveReplyAsync(new ReplyDTO
            {
                Id = replyInputModel.Id,
                Description = replyInputModel.Text
            });

            return RedirectToAction("Details", "Post", new { id = replyInputModel.PostId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var replyToDelete = _replyService.GetReplyById(id);
            if (replyToDelete == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (replyToDelete.AuthorId != currentUserId)
                return Unauthorized();

            await _replyService.DeleteReplyAsync(id);

            return RedirectToAction("Details", "Post", new { id = replyToDelete.PostId });
        }
    }
}
