using AutoMapper;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Friend;
using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            #region "User"
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();
            #endregion

            #region "Publication"
            CreateMap<Publication, PublicationViewModel>()
                .ForMember(x => x.User, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Publication, SavePublicationViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                
                .ReverseMap()
                .ForMember(x => x.Comments, opt => opt.Ignore());
            #endregion

            #region "Comment"
            CreateMap<Comment, CommentViewModel>()
                .ReverseMap();

            CreateMap<Comment, SaveCommentViewModel>()
                .ReverseMap()
                .ForMember(x => x.Children, opt => opt.Ignore())
                .ForMember(x => x.Publication, opt => opt.Ignore());
            #endregion

            #region "Friend"
            CreateMap<Friend, FriendViewModel>()
                .ReverseMap();
            CreateMap<Friend, SaveFriendViewModel>()
                .ReverseMap();
            #endregion

        }
    }
}
