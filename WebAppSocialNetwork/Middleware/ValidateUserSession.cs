using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;

namespace WebAppSocialNetwork.Middleware
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse usuarioViewModel = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("User");

            if (usuarioViewModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
