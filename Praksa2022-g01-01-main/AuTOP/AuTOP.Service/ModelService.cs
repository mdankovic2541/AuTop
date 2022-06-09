using AuTOP.Common;
using AuTOP.Model.DomainModels;
using AuTOP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Service
{
    class ModelService : IModelService
    {
        private IModelRepository modelRepository;
        private IModelVersionService modelVersionService;
        private IManufacturerRepository manufacturerRepository;

        public ModelService(IModelRepository modelRepository,IModelVersionService modelVersionService,IManufacturerRepository manufacturerRepository)
        {
            this.modelRepository = modelRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.modelVersionService = modelVersionService;
        }
        public async Task<List<ModelDomainModel>> GetAllModelsAsync(ModelFilter filter, Sorting sorting, Paging paging)
        {
            List<ModelDomainModel> modelDomain = await modelRepository.GetAllModelsAsync(filter, sorting, paging);
            return modelDomain;
        }

        public async Task<ModelDomainModel> GetModelAsync(Guid id)
        {
            ModelDomainModel modelDomain = await modelRepository.GetModelById(id);
            modelDomain.ModelVersions = await modelVersionService.GetAllModelVersionsAsync(new ModelVersionFilter {ModelId = id }, new Sorting("", ""), new Paging(true));
            modelDomain.Manufacturer = await manufacturerRepository.GetManufacturerByIdAsync(modelDomain.ManufacturerId);
            return modelDomain;
        }

        public async Task PostModelAsync(ModelDomainModel model)
        {
            model.Generate();
            await modelRepository.PostModelAsync(model);
        }
        public async Task PutModelAsync(ModelDomainModel model)
        {
            model.DateUpdated = DateTime.UtcNow;
            await modelRepository.PutModelAsync(model);
        }

        public async Task DeleteModelAsync(Guid id)
        {
            await modelRepository.DeleteModelAsync(id);
        }

    }
}
