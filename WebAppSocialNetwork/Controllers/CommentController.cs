using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;

namespace WebAppSocialNetwork.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(SaveCommentViewModel commentViewModel)
        {
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
            if (!ModelState.IsValid)
            {
                return View("Index", commentViewModel);
            }
            await _commentService.Add(commentViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
    }
}
