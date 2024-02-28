using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Publication
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime DateCreate { get; set; }

        //Property Navigation
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Comment>? Comments { get; set;} 
    }
}
