using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class FriendRepository : GenericRepository<Friend>, IFriendRepository
    {
        private readonly ApplicationDbContext _context;
        public FriendRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Friend>> GetAllFriendsOfCurrentUser(string currentUserId)
        {
            return await _context.Friends
                .Where(f => f.UserId == currentUserId)
                .ToListAsync();
        }

        public async Task<Friend> GetFriend (string currentFriend)
        {
            var friend = await _context.Friends.FirstOrDefaultAsync(f =>f.UserId == currentFriend);
            return friend;
        }

        public async Task<Friend> GetFriendById(string id)
        {
            var findFriend = await _context.Friends.FirstOrDefaultAsync(f => f.FriendId == id);
            return findFriend;
        }

    }
}
