using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Publication
{
    public class PublicationViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime DateCreate { get; set; }
        public string? UserName { get; set; }
        public string? User {  get; set; }
        public string? UserLastName { get; set; }
        public string? UserPhoto {  get; set; }
        public string? UserId { get; set; }
        public ICollection<CommentViewModel>? Comments { get; set; }
    }
}
