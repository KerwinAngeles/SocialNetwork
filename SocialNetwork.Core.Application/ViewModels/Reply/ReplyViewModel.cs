using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Reply
{
    public class ReplyViewModel
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int commentId { get; set; }
        public string? UserId { get; set; }
        public CommentViewModel? Comment { get; set; }
    }
}
