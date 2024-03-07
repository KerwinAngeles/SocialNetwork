using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Friend
{
    public class SaveFriendViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "You must enter a user name!!")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; } 
        public string? ImageUrl { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
