using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Application.ViewModels.User;

namespace WebAppSocialNetwork.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPublicationService _publicationService;
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;


        public ProfileController(IUserService userService, IHttpContextAccessor httpContextAccessor, IPublicationService publicationService, ICommentService commentService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _publicationService = publicationService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(string id)
        {
            id = _userViewModel.Id;
            return View(await _userService.GetByIdEditProfile(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel saveUser)
        {

            if (!ModelState.IsValid)
            {
                return View("Index");
            }


            saveUser.Id = _userViewModel.Id;
            SaveUserViewModel userViewModel = await _userService.GetById(saveUser.Id);
            saveUser.ImageUrl = UploadFile(saveUser.PhotoProfile, saveUser.Id, true, userViewModel.ImageUrl);
            await _commentService.Update(saveUser.Id, saveUser.ImageUrl);
            await _publicationService.Update(saveUser.Id, saveUser.ImageUrl, saveUser.Name, saveUser.LastName);
            await _userService.Update(saveUser);

            return RedirectToAction("Index", "Home");
        }

        private string UploadFile(IFormFile file, string id, bool isEditMode = false, string photoUrl = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return photoUrl;
                }
            }
            string basePath = $"/Images/Publication/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            if (file != null)
            {
                FileInfo fileInfo = new(file.FileName);
                string fileName = guid + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return $"{basePath}/{fileName}";
            }
            else
            {
                return null;
            }

        }
    }
}
