using AuTOP.Common;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuTOP.Service
{
    public interface IModelService
    {
        Task<List<ModelDomainModel>> GetAllModelsAsync(ModelFilter filter, Sorting sorting, Paging paging);
        Task<ModelDomainModel> GetModelAsync(Guid id);
        Task PostModelAsync(ModelDomainModel model);
        Task PutModelAsync(ModelDomainModel model);
        Task DeleteModelAsync(Guid id);


    }
}