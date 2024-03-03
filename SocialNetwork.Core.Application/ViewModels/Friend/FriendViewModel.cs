using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Friend
{
    public class FriendViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserId { get; set; }
        public string? FriendId { get; set; }
    }
}
