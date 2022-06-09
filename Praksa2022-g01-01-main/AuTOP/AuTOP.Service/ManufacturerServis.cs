using AuTOP.Common;
using AuTOP.Model.DomainModels;
using AuTOP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Service.Common;

namespace AuTOP.Service
{
    class ManufacturerServis : IManufacturerServis
    {
        private IManufacturerRepository manufacturerRepository;
        private IModelRepository modelRepository;

        public ManufacturerServis(IManufacturerRepository manufacturerRepository,IModelRepository modelRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.modelRepository = modelRepository;
        }
        public async Task<PagedManufacturersModel> GetAllManufacturersAsync(ManufacturerFilter courseFilter, Sorting sort, Paging paging)
        {
            PagedManufacturersModel manufacturers = await manufacturerRepository.GetAllManufacturers(courseFilter, sort, paging);
            
            return manufacturers; ;
        }
        public async Task<ManufacturerDomainModel> GetManufacturerByIdAsync(Guid id,string modelSortMethod,string modelFilter)
        {
            ManufacturerDomainModel domainManufacturer = await manufacturerRepository.GetManufacturerByIdAsync(id);
            domainManufacturer.Models = await modelRepository.GetAllModelsAsync(new ModelFilter {ManufacturerId = domainManufacturer.Id, Name=modelFilter }, new Sorting("Name", modelSortMethod), new Paging(true));
            //domainManufacturer.Models = await modelRepository.GetModelsByManufacturer(domainManufacturer.Id);
            return domainManufacturer;
        }

        public async Task PostManufacturerAsync(ManufacturerDomainModel manufacturer)
        {
            manufacturer.Generate();
            await manufacturerRepository.PostManufacturerAsync(manufacturer);
        }

        public async Task PutManufacturerAsync(ManufacturerDomainModel manufacturer)
        {
            manufacturer.DateUpdated = DateTime.UtcNow;
            await manufacturerRepository.PutManufacturerAsync(manufacturer);
        }

        public async Task DeleteManufacturerAsync(Guid id)
        {
            await manufacturerRepository.DeleteManufacturerAsync(id);
        }
    }
}
