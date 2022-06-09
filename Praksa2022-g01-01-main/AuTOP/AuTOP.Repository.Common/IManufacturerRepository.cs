using AuTOP.Common;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuTOP.Repository
{
    public interface IManufacturerRepository
    {
        Task<PagedManufacturersModel> GetAllManufacturers(ManufacturerFilter filter, Sorting sort, Paging paging);
        Task<ManufacturerDomainModel> GetManufacturerByIdAsync(Guid id);
        Task PostManufacturerAsync(ManufacturerDomainModel manufacturer);
        Task PutManufacturerAsync(ManufacturerDomainModel manufacturer);
        Task DeleteManufacturerAsync(Guid id);
    }
}