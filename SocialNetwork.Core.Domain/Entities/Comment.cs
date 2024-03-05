using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? ImageUrl { get; set; }
        public int PublicationId { get; set; }
        public Publication? Publication { get; set; }
        public Comment? Parent { get; set; }
        public ICollection<Comment>? Children { get; set;}
    }
}
