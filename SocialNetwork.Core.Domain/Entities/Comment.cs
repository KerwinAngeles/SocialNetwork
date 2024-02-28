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
        public string? Message { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int publicationId { get; set; }
        public Publication? Publication { get; set; }
        public ICollection<Reply>? replies { get; set; }
    }
}
