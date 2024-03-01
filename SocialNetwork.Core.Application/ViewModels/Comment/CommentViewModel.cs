using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? UserName { get; set; }
        public string? UserPhoto {  get; set; }
        public string? UserId { get; set; }
        public int ParentId { get; set; }
        public int PublicationId { get; set; }
        public PublicationViewModel? Publication { get; set; }
        public CommentViewModel? Parent { get; set; }
        public List<CommentViewModel>? Children { get; set; }
    }
}
