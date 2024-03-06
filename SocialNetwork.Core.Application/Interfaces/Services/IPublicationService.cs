using SocialNetwork.Core.Application.ViewModels.Publication;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPublicationService : IGenericService<SavePublicationViewModel, PublicationViewModel, Publication>
    {
        //Task<List<PublicationViewModel>> GetAllViewModelWithInclude();
        Task<List<PublicationViewModel>> GetAllPublicationFriend();
        Task<List<PublicationViewModel>> GetAllpublicationById(string id);

        Task Update(string userId, string ImageUrl, string userName, string lastName);

    }
}
