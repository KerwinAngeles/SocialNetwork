using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class EditUserViewModel
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
        [RegularExpression(@"^(809|829|849)(-)?\d{3}(-)?\d{4}$", ErrorMessage = "Must be a dominican phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The password must be the same")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? PhotoProfile { get; set; }
        public string? ImageUrl { get; set; }

    }
}
