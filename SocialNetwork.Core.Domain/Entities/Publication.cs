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
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserLastName { get; set; }
        public string? UserPhoto { get; set; }
        public ICollection<Comment>? Comments { get; set;} 
    }
}
