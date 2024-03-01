using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region "Services"
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPublicationService, PublicationService>();
            services.AddTransient<IUploadImageService, UploadImagesService>();
            services.AddTransient<ICommentService, CommentService>();
            
            #endregion
        }

    }
}
