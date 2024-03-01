using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;

        public PublicationService(IPublicationRepository publicationRepository, IHttpContextAccessor httpContextAccessor ,IMapper mapper) : base(publicationRepository, mapper)
        {
            _publicationRepository = publicationRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
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
            var publicationList = await _publicationRepository.GetAllWithInclude(new List<string> { "Comments" });

            return publicationList.Where(publication => publication.UserId == userViewModel.Id).Select(publication => new PublicationViewModel
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

                Comments = publication.Comments.Select(comment => new CommentViewModel
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    UserName = userViewModel.Name,
                    UserPhoto = userViewModel.ImageUrl
                    
                }).ToList()
            }).OrderByDescending(publication => publication.DateCreate).ToList();


        }
    }
}
