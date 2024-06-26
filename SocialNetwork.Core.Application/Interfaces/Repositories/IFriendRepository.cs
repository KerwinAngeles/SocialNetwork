﻿using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IFriendRepository : IGenericRepository<Friend>
    {
        Task<List<Friend>> GetAllFriendsOfCurrentUser(string currentUserId);
        Task<Friend> GetFriend(string currentFriend);
        Task<Friend> GetFriendById(string id);
    }
}
