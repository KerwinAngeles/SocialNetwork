using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.ViewModels.Friend;
using SocialNetwork.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task SingOutAsync();
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, string origin);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request, string origin);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<SaveUserViewModel> FindByName(string name);
        Task Update(EditUserViewModel saveUser);
        Task<EditUserViewModel> FindByIdEditProfile(string Id);
        Task<SaveUserViewModel> FindById(string Id);
        Task<SaveFriendViewModel> FindByFriendName(string name);
        

    }
}
