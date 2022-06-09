using AuTOP.Common;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuTOP.Repository
{
    public interface IModelRepository
    {
        Task<List<ModelDomainModel>> GetAllModelsAsync(ModelFilter filter, Sorting sort, Paging paging);
        Task<ModelDomainModel> GetModelById(Guid id);
        Task PostModelAsync(ModelDomainModel model);
        Task PutModelAsync(ModelDomainModel model);
        Task DeleteModelAsync(Guid id);

    }
}