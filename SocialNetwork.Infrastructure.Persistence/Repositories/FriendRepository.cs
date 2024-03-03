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
                .Where(f => f.UserId == currentUserId || f.FriendId == currentUserId)
                .ToListAsync();
        }

    }
}
