using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Reply
{
    public class SaveReplyViewModel
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int CommentId { get; set; }
        public string? UserId { get; set; }
        public List<int>? Comments { get; set; }
    }
}
