using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class PublicationService : GenericService<SavePublicationViewModel, PublicationViewModel, Publication>, IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly ICommentService _commentService;
        private readonly IFriendRepository _friendRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly AuthenticationResponse userViewModel;
        public PublicationService(IPublicationRepository publicationRepository, IHttpContextAccessor httpContextAccessor ,IMapper mapper, IAccountService accountService, IFriendRepository friendRepository, ICommentService comment) : base(publicationRepository, mapper)
        {
            _publicationRepository = publicationRepository;
            _commentService = comment;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _accountService = accountService;
            _friendRepository = friendRepository;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SavePublicationViewModel> Add(SavePublicationViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            return await base.Add(vm);
        }

        public override async Task Update(SavePublicationViewModel vm, int id)
        {
            vm.UserId = userViewModel.Id;
            await base.Update(vm, id);
        }

     
        public async Task<List<PublicationViewModel>> GetAllViewModelWithInclude()
        {
          
            var publication = await _publicationRepository.GetAllpublicationById(userViewModel.Id);

            return publication.Select(publication => new PublicationViewModel
            {
                Id = publication.Id,
                Title = publication.Title,
                ImageUrl = publication.ImageUrl,
                VideoUrl = publication.VideoUrl,
                DateCreate = publication.DateCreate,
                UserName = userViewModel.UserName,
                UserPhoto = userViewModel.ImageUrl,
                User = userViewModel.Name,
                UserLastName = userViewModel.LastName,
                Comments = _commentService.BuildCommentViewModels(publication.Comments.Where(comment => comment.ParentId == null).ToList())

                //Comments = publication.Comments.Where(comment => comment.ParentId == null)

                //.Select(comment => new CommentViewModel
                //{
                //    Id = comment.Id,
                //    Message = comment.Message,
                //    UserName = comment.UserName,
                //    ImageUrl = comment.ImageUrl,
                //    Children = comment.Children != null

                //    ? comment.Children.Select(reply => new CommentViewModel
                //    {
                //        Id = reply.Id,
                //        Message = reply.Message,
                //        UserName = userViewModel.UserName,
                //        ImageUrl = userViewModel.ImageUrl

                //    }).ToList()

                //    : new List<CommentViewModel>()
                //}).ToList(),
            })
            .OrderByDescending(publication => publication.DateCreate)
            .ToList();
        }

        public async Task<List<PublicationViewModel>> GetAllPublicationFriend()
        {
            var friendById = await _friendRepository.GetFriend(userViewModel.Id);

            if(friendById != null)
            {
                var publicationFriend = await _accountService.FindById(friendById.FriendId);
                var publication = await _publicationRepository.GetAllpublicationById(publicationFriend.Id);

                return publication.Where(p => p.UserId == publicationFriend.Id).Select(publication => new PublicationViewModel
                {
                    Id = publication.Id,
                    Title = publication.Title,
                    ImageUrl = publication.ImageUrl,
                    VideoUrl = publication.VideoUrl,
                    DateCreate = publication.DateCreate,
                    UserName = publicationFriend.UserName,
                    UserPhoto = publicationFriend.ImageUrl,
                    User = publicationFriend.Name,
                    UserLastName = publicationFriend.LastName,
                    Comments = _commentService.BuildCommentViewModels(publication.Comments.Where(comment => comment.ParentId == null).ToList())

                    //  Comments = publication.Comments.Where(comment => comment.ParentId == null)

                    //.Select(comment => new CommentViewModel
                    //{
                    //    Id = comment.Id,
                    //    Message = comment.Message,
                    //    UserName = comment.UserName,
                    //    ImageUrl = comment.ImageUrl,
                    //    Children = comment.Children != null

                    //    ? comment.Children.Select(reply => new CommentViewModel
                    //    {
                    //        Id = reply.Id,
                    //        Message = reply.Message,
                    //        UserName = comment.UserName,
                    //        ImageUrl = comment.ImageUrl

                    //    }).ToList()

                    //    : new List<CommentViewModel>()
                    //}).ToList(),
                })
                  .OrderByDescending(publication => publication.DateCreate)
                  .ToList();
            }
            else
            {
                return new List<PublicationViewModel>();
            }
        }
    }
}
