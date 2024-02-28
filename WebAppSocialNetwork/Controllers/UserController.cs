using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;

namespace WebAppSocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            AuthenticationResponse userAuthenticate = await _userService.LoginAsync(loginViewModel);

            if(userAuthenticate != null && userAuthenticate.HasError != true) 
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userAuthenticate);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                loginViewModel.HasError = userAuthenticate.HasError;
                loginViewModel.Error = userAuthenticate.Error;
                return View(loginViewModel);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveUser)
        {
            if (!ModelState.IsValid)
            {
                return View(saveUser);
            }

            var origin = Request.Headers["origin"];

            RegisterResponse registerResponse = await _userService.RegisterAsync(saveUser, origin);

            if (registerResponse.HasError)
            {
                saveUser.HasError = registerResponse.HasError;
                saveUser.Error = registerResponse.Error;
                return View(saveUser);
            }
        
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotViewModel);
            }
            var origin = Request.Headers["origin"];

            ForgotPasswordResponse forgotResponse = await _userService.ForgotPasswordAsync(forgotViewModel, origin);
            if (forgotResponse.HasError)
            {
                forgotViewModel.HasError = forgotViewModel.HasError;
                forgotViewModel.Error = forgotViewModel.Error;
                return View(forgotViewModel);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });

        }

        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token});
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var origin = Request.Headers["origin"];

            ResetPasswordResponse resetPasswordResponse = await _userService.ResetPasswordAsync(resetPasswordViewModel, origin);

            if (resetPasswordResponse.HasError)
            {
                resetPasswordViewModel.HasError = resetPasswordResponse.HasError;
                resetPasswordViewModel.Error = resetPasswordResponse.Error;
                return View(resetPasswordViewModel);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
