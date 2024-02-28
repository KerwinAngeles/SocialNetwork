using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel loginVm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel userVm, string origin);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordVm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordVm, string origin);
        Task SignOutAsync();
    }
}
