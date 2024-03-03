using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friend;

namespace WebAppSocialNetwork.Controllers
{
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

            await _friendService.Add(saveFriend);

            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        } 
    }
}
