using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;

namespace WebAppSocialNetwork.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;

        public CommentController(ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(SaveCommentViewModel commentViewModel)
        {
            commentViewModel.UserName = _userViewModel.UserName;
            commentViewModel.ImageUrl = _userViewModel.ImageUrl;

            if (!ModelState.IsValid)
            {
                return View("Index", commentViewModel);
            }

            await _commentService.Add(commentViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateReplyComment(SaveCommentViewModel commentViewModel)
        {
            commentViewModel.UserName = _userViewModel.UserName;
            commentViewModel.ImageUrl = _userViewModel.ImageUrl;

            if (!ModelState.IsValid)
            {
                return View("Index", commentViewModel);
            }
            await _commentService.Add(commentViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
    }
}
