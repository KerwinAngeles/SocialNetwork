using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "You must enter a name!!")]
        [DataType(DataType.Text)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "You must enter a last name!!")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a email!!")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "You must enter a phone!!")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "You must enter a user name!!")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; } 

        [Required(ErrorMessage = "You must enter a password!!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The password must be the same")]
        [Required(ErrorMessage = "You must enter a password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        public string? ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? PhotoProfile { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
