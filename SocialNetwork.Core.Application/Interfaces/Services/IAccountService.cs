using SocialNetwork.Core.Application.Dtos.Account;
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
        Task<AuthenticationResponse> FindByName(string name);

        Task<AuthenticationResponse> FindById(string Id);

    }
}
