using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Domain.Entities;
using System.Diagnostics;
using WebAppSocialNetwork.Models;

namespace WebAppSocialNetwork.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;


        public HomeController(IPublicationService publicationService, IHttpContextAccessor httpContextAccessor, ICommentService commentService)
        {
            _publicationService = publicationService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            List<PublicationViewModel> publication = await _publicationService.GetAllpublicationById(_userViewModel.Id);
            return View(publication);
        }

       
        public IActionResult Create()
        {
            return View("Index", new SavePublicationViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePublicationViewModel savePublication)
        {

            if (!ModelState.IsValid)
            {
                return View("Create", savePublication);
            }

            SavePublicationViewModel publicationViewModel = await _publicationService.Add(savePublication);

            if (publicationViewModel.Id != 0 && publicationViewModel != null)
            {
                publicationViewModel.ImageUrl = UploadFile(savePublication.File, publicationViewModel.Id);
                await _publicationService.Update(publicationViewModel, publicationViewModel.Id);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _publicationService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePublicationViewModel savePublication)
        {
           
            if (!ModelState.IsValid)
            {
                return View("Edit", savePublication);
            }

            SavePublicationViewModel publicationViewModel = await _publicationService.GetById(savePublication.Id);
            savePublication.ImageUrl = UploadFile(savePublication.File, savePublication.Id, true, publicationViewModel.ImageUrl);

            await _publicationService.Update(savePublication, savePublication.Id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _publicationService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            string basePath = $"/Images/Publication/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }
                Directory.Delete(path);
            }
            await _publicationService.Delete(id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
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

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string photoUrl = "")
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
            if(file != null)
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
