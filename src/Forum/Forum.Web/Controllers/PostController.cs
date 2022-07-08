using Forum.Core.Entities;
using Forum.Services.Abstract;
using Forum.Services.Abstract.DTOs;
using Forum.Web.Models.Post;
using Forum.Web.Models.Reply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private const int DefaultPostsCountOnPage = 10;

        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
             IEnumerable<Post> postsList = _postService.GetPosts(page, DefaultPostsCountOnPage);

            var postsViewModelList = postsList.Select(x => new PostItemViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.UserName,
                RepliesCount = x.Replies.Count(),
                LastModifiedOn = x.LastModifiedOn.ToString()
            }).AsEnumerable();

            return View(new PostsAllViewModel()
            {
                Posts = postsViewModelList,
                PageIndex = page,
                TotalPages = (int)Math.Ceiling(postsViewModelList.Count() / (decimal)DefaultPostsCountOnPage),
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View(new PostInputViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostInputViewModel postInputModel)
        {
            if (!ModelState.IsValid)
                return this.View(postInputModel);

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postService.SavePostAsync(new PostDTO
            {
                Title = postInputModel.Title,
                Description = postInputModel.Description,
                AuthorId = currentUserId,
            });

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postToEdit = _postService.GetPostById(id);
            
            if (postToEdit == null)
                return NotFound();

            if(postToEdit.AuthorId != currentUserId)
                return Unauthorized();       

            return this.View(new PostInputViewModel()
            {
                Id = id,
                Title = postToEdit.Title,
                Description = postToEdit.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostInputViewModel postInputModel)
        {
            if (!ModelState.IsValid)
                return this.View(postInputModel);

            var postToEdit = _postService.GetPostById(postInputModel.Id);
            if (postToEdit == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (postToEdit.AuthorId != currentUserId)
                return Unauthorized();

            await _postService.SavePostAsync(new PostDTO
            {
                Id = postInputModel.Id,
                Title = postInputModel.Title,
                Description = postInputModel.Description
            });

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var postToView = _postService.GetPostById(id);
            if (postToView == null)
                return NotFound();

            return this.View(new PostDetailsViewModel()
            {
                Id = id,
                Title = postToView.Title,
                Description = postToView.Description,
                Replies = postToView.Replies?.Select(x => new ReplyViewModel()
                {
                    Id = x.Id,
                    Text = x.Description,
                    AuthorId = x.AuthorId,
                    AuthorName = x.Author.UserName,
                    CreatedOn = x.CreatedOn.ToLocalTime().ToString()
                }),
                AuthorName = postToView.Author.UserName,
                AuthorId = postToView.AuthorId,
                CreatedOn = postToView.CreatedOn.LocalDateTime.ToString()
            });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var postToDelete = _postService.GetPostById(id);
            if (postToDelete == null)
                return NotFound();
            
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (postToDelete.AuthorId != currentUserId)
                return Unauthorized();
            

            await _postService.DeletePostAsync(id);

            return RedirectToAction("Index");
        }
    }
}
