using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "You must enter a email!!")]
        [DataType(DataType.Text)]
        public string Email { get; set; } = null!;
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
