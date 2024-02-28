using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Reply
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int commentId { get; set; }
        public int UserId { get; set; }
        public Comment? Comment { get; set; }
        public User? User { get; set; }

    }
}
