﻿using SocialNetwork.Core.Application.ViewModels.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        Task<SaveViewModel> Add(SaveViewModel createProduct);
        Task Update(SaveViewModel updateProduct, int id);
        Task Delete(int id);
        Task<List<ViewModel>> GetAll();
        Task<SaveViewModel> GetById(int id);

    }
}
