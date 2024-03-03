using SocialNetwork.Core.Application.Dtos.Account;
using SocialNetwork.Core.Application.ViewModels.Friend;
using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IFriendService : IGenericService<SaveFriendViewModel, FriendViewModel, Friend>
    {
        Task<List<FriendViewModel>> GetAllFriend();
    }
}
