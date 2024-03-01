using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Application.ViewModels.Reply;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<PublicationViewModel>? Publications { get; set; }
        public ICollection<CommentViewModel>? Comments { get; set; }
        public ICollection<ReplyViewModel>? replies { get; set; }
    }
}
