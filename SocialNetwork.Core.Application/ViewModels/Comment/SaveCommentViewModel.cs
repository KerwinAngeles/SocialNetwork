using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Comment
{
    public class SaveCommentViewModel
    {  
        public string Message { get; set; } = null!;
        public string? UserId { get; set; }
        public int PublicationId { get; set; }
        public int? ParentId { get; set; }
        public List<int>? Publication { get; set; }
        public List<int>? Children { get; set; }

    }
}
