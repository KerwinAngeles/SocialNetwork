﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friend;
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
    public class FriendService : GenericService<SaveFriendViewModel, FriendViewModel, Friend>, IFriendService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly AuthenticationResponse _userViewModel;

        public FriendService(IFriendRepository friendRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IAccountService accountService) : base(friendRepository, mapper)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveFriendViewModel> Add(SaveFriendViewModel saveFriend)
        {
            var currentUser = await _accountService.FindByFriendName(_userViewModel.UserName);
            
            if(currentUser == null)
            {
                return null;
            }

            var friendUser = await _accountService.FindByFriendName(saveFriend.UserName);
            
            if(currentUser.Name == friendUser.Name)
            {
                saveFriend.HasError = true;
                saveFriend.Error = "You can't add yourself as a friend";
                return saveFriend;
            }
            if(friendUser.HasError == true)
            {
                saveFriend.HasError = true;
                saveFriend.Error = "This user was not found.";
                return saveFriend;
            }
            else if(friendUser.UserName == currentUser.UserName)
            {
                return saveFriend;
            }
            
            else
            {
                var friend = new Friend()
                {
                    UserId = currentUser.Id,
                    FriendId = friendUser.Id
                };
                var findFriend = await _friendRepository.GetFriendById(friend.FriendId);
                if (findFriend != null)
                {
                    saveFriend.HasError = true;
                    saveFriend.Error = "You can't add your friend again.";
                    return saveFriend;
                }
                await _friendRepository.AddAsync(friend);
                return _mapper.Map<SaveFriendViewModel>(friend);
            }   
        }

        public async Task<List<FriendViewModel>> GetAllFriend()
        {
            var currentUser = await _accountService.FindByFriendName(_userViewModel.UserName);
            if (currentUser == null)
            {
                return null;
            }

            var friends = await _friendRepository.GetAllFriendsOfCurrentUser(currentUser.Id);

            var friendViewModels = new List<FriendViewModel>();
            foreach (var friend in friends)
            {
                var friendUser = await _accountService.FindById(friend.FriendId);
                if (friendUser != null)
                {
                    friendViewModels.Add(new FriendViewModel
                    {
                        Id = friend.Id,
                        Name = friendUser.Name,
                        LastName = friendUser.LastName,
                        ImageUrl = friendUser.ImageUrl,
                        UserName = friendUser.UserName
                    });
                }
            }
            return friendViewModels;
        }

      
    }
}
