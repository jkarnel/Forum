using Forum.Core.Entities;
using Forum.Services.Abstract;
using Forum.Services.Abstract.DTOs;
using Forum.Web.Models.Post;
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
        public IActionResult Index(string searchStatement = null, int page = 0)
        {
            IEnumerable<Post> postsList = _postService.GetPosts(searchStatement, page, DefaultPostsCountOnPage);

            var postsViewModelList = postsList.Select(x => new PostItemViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.UserName,
                RepliesCount = x.Replies.Count()
            }).AsEnumerable();

            return View(new PostsAllViewModel()
            {
                Posts = postsViewModelList,
                PageIndex = page,
                TotalPages = postsViewModelList.Count(),
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
        public IActionResult Edit(int postId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postToEdit = _postService.GetPostById(postId);
            if (postToEdit == null)
            {
                //return error that post was not found
            }
            if(postToEdit.AuthorId != currentUserId)
            {
                //return error that user don`t have rights to edit this post
            }

            return this.View(new PostInputViewModel()
            {
                Id = postId,
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
            {
                //return error that post was not found
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (postToEdit.AuthorId != currentUserId)
            {
                //return error that user don`t have rights to edit this post
            }

            await _postService.SavePostAsync(new PostDTO
            {
                Title = postInputModel.Title,
                Description = postInputModel.Description
            });

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult View(int postId)
        {
            var postToEdit = _postService.GetPostById(postId);
            if (postToEdit == null)
            {
                //return error that post was not found
            }

            return this.View(new PostInputViewModel()
            {
                Id = postId,
                Title = postToEdit.Title,
                Description = postToEdit.Description
            });
        }
    }
}
