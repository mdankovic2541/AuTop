using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using AuTOP.Common;
using AuTOP.Model.DomainModels;
using AuTOP.Service;
using AuTOP.Service.Common;
using AuTOP.WebAPI.Models;
using AuTOP.WebAPI.Models.DetailModel;
using AuTOP.WebAPI.Models.InputModel;
using AuTOP.WebAPI.Models.ViewModels;

namespace AuTOP.WebAPI.Controllers
{
    public class ManufacturerController :ApiController
    {
        private IManufacturerServis manufacturerServis;
        private IMapper mapper;

        public ManufacturerController(IManufacturerServis manufacturerServis,IMapper mapper)
        {
            this.manufacturerServis = manufacturerServis;
            this.mapper = mapper;
        }
        public async Task<HttpResponseMessage> GetAllManufacturersAsync(string name = "", string sortBy = "Name", string sortMethod = "", int page = 1)
        {
            ManufacturerFilter filter = new ManufacturerFilter
            {
                Name = name
            };
            Sorting sorting = new Sorting(sortBy, sortMethod);
            Paging paging = new Paging(page);
            PagedManufacturersModel domainManufacturers = await manufacturerServis.GetAllManufacturersAsync(filter,sorting,paging);
            PagedViewManufacturersModel viewManufacturers = mapper.Map<PagedManufacturersModel,PagedViewManufacturersModel>(domainManufacturers);
            return Request.CreateResponse(HttpStatusCode.OK,viewManufacturers);
  
        }
        public async Task<HttpResponseMessage> GetManufacturerByIdAsync(Guid id,string modelSortMethod = "",string modelFilter = "")
        {
            ManufacturerDomainModel domainManufacturer = await manufacturerServis.GetManufacturerByIdAsync(id,modelSortMethod,modelFilter);
            ManufacturerDetailModel detailManufacturer = mapper.Map<ManufacturerDomainModel, ManufacturerDetailModel>(domainManufacturer);
            return Request.CreateResponse(HttpStatusCode.OK,domainManufacturer);
        }

        public async Task<HttpResponseMessage> PostManufacturerAsync(ManufacturerInputModel manufacturer)
        {
            ManufacturerDomainModel domainManufacturer = mapper.Map<ManufacturerInputModel, ManufacturerDomainModel>(manufacturer);
            await manufacturerServis.PostManufacturerAsync(domainManufacturer);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        public async Task<HttpResponseMessage> PutManufacturerAsync(ManufacturerInputModel manufacturer)
        {
            ManufacturerDomainModel domainManufacturer = mapper.Map<ManufacturerInputModel, ManufacturerDomainModel>(manufacturer);
            await manufacturerServis.PutManufacturerAsync(domainManufacturer);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        public async Task<HttpResponseMessage> DeleteManufacturerAsync(Guid id)
        {
            await manufacturerServis.DeleteManufacturerAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }



    }
}