using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friend;

namespace WebAppSocialNetwork.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendService _friendService;
        private readonly IPublicationService _publicationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;
        public FriendController(IFriendService friendService, IHttpContextAccessor httpContextAccessor, IPublicationService publicationService)
        {
            _friendService = friendService;
            _httpContextAccessor = httpContextAccessor;
            _publicationService = publicationService;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
    
        public async Task<IActionResult> Index()
        {
            List<FriendViewModel> friends = await _friendService.GetAllFriend();
            ViewBag.friend = friends;
            return View(await _publicationService.GetAllPublicationFriend());
        }

        public IActionResult AddFriend()
        {
            return View("AddFriend", new SaveFriendViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(SaveFriendViewModel saveFriend)
        {
            if (!ModelState.IsValid)
            {
                return View("AddFriend", saveFriend);
            }

            var user =  await _friendService.Add(saveFriend);

            if (saveFriend.HasError)
            {
                saveFriend.HasError = user.HasError;
                saveFriend.Error = user.Error;
                return View(saveFriend);
            }

            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        } 

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _friendService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _friendService.Delete(id);
            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        }

    }
}
