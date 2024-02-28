using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "You must enter a user name!!")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Must be have a token!!")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "You must enter a password!!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The password must be the same")]
        [Required(ErrorMessage = "You must enter a password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}
