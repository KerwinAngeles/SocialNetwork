using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class CommentService : GenericService<SaveCommentViewModel, CommentViewModel, Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;

        public CommentService(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveCommentViewModel> Add(SaveCommentViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            return await base.Add(vm);
        }

        public async Task Update(string userId, string ImageUrl)
        {
            var comment = await _commentRepository.GetAllCommentByUserId(userId);
            foreach (var item in comment)
            {
                item.ImageUrl = ImageUrl;
                await _commentRepository.UpdateAsync(item, item.Id);
            }
        }
        public List<CommentViewModel> BuildCommentViewModels(List<Comment> comments)
        {
            return comments.Select(comment => new CommentViewModel
            {
                Id = comment.Id,
                Message = comment.Message,
                UserName = comment.UserName,
                ImageUrl = comment.ImageUrl,
                Children = comment.Children != null
                    ? BuildCommentViewModels(comment.Children.ToList())
                    : new List<CommentViewModel>()
            }).ToList();
        }
    }
}
