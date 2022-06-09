using AuTOP.Common;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuTOP.Service
{
    public interface IManufacturerServis
    {
        Task<PagedManufacturersModel> GetAllManufacturersAsync(ManufacturerFilter courseFilter, Sorting sort, Paging paging);
        Task<ManufacturerDomainModel> GetManufacturerByIdAsync(Guid id,string modelSortMethod,string modelFilter);
        Task PostManufacturerAsync(ManufacturerDomainModel manufacturer);
        Task PutManufacturerAsync(ManufacturerDomainModel manufacturer);
        Task DeleteManufacturerAsync(Guid id);

    }
}