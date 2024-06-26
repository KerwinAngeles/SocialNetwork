﻿using Microsoft.EntityFrameworkCore;
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
    public class PublicationRepository : GenericRepository<Publication>, IPublicationRepository
    {
        private readonly ApplicationDbContext _context;
        public PublicationRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<Publication>> GetAllpublicationById(string userId)
        {
            var publications = await _context.Publications.Where(p => p.UserId == userId).Include(c => c.Comments)
                .ToListAsync();
            return publications;
        }

       
    }
}
