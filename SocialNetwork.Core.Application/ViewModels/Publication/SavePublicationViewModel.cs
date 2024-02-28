using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Publication
{
    public class SavePublicationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a title!!")]
        [DataType(DataType.Text)]
        public string? Title { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
