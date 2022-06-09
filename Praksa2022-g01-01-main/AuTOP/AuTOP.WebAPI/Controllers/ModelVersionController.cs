using AutoMapper;
using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Model.DomainModels;
using AuTOP.Service;
using AuTOP.WebAPI.Models;
using AuTOP.WebAPI.Models.DetailModel;
using AuTOP.WebAPI.Models.InputModel;
using AuTOP.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuTOP.WebAPI.Controllers
{
    public class ModelVersionController : ApiController
    {
        private IMapper mapper;
        private IModelVersionService modelVersionService;

        public ModelVersionController(IMapper mapper,IModelVersionService modelVersionService)
        {
            this.mapper = mapper;
            this.modelVersionService = modelVersionService;
        }
        public async Task<HttpResponseMessage> GetAllModelVersions([FromUri] ModelVersionFilter filter,string sortBy = "Year", string sortMethod = "ASC", int page = 1)
        {   if(filter == null)
            {
                filter = new ModelVersionFilter();
            }
            Sorting sorting = new Sorting(sortBy, sortMethod);
            Paging paging = new Paging(page);
            List<ModelVersion> modelVersionsDomain = await modelVersionService.GetAllModelVersionsAsync(filter, sorting, paging);
            List<ModelVersionViewModel> modelVersionsView = mapper.Map<List<ModelVersion>, List<ModelVersionViewModel>>(modelVersionsDomain);
            return Request.CreateResponse(HttpStatusCode.OK, modelVersionsView);

        }
        public async Task<HttpResponseMessage> GetModelVersionById(Guid id)
        {
            ModelVersion modelVersionDomain = await modelVersionService.GetModelVersionByIdAsync(id,User.Identity.Name);
            ModelVersionDetailModel detailModel = mapper.Map<ModelVersion, ModelVersionDetailModel>(modelVersionDomain);
            return Request.CreateResponse(HttpStatusCode.OK, detailModel);

       }

        public async Task<HttpResponseMessage> PostModelVersion(ModelVersionInputModel modelVersion)
        {
            ModelVersion domainModelVersion = mapper.Map<ModelVersionInputModel, ModelVersion>(modelVersion);
            await modelVersionService.PostModelVersionAsync(domainModelVersion);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> PutModelVersion(ModelVersionInputModel modelVersion)
        {
            ModelVersion domainModelVersion = mapper.Map<ModelVersionInputModel, ModelVersion>(modelVersion);
            await modelVersionService.PutModelVersionAsync(domainModelVersion);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteModelVersion(Guid id)
        {
            await modelVersionService.DeleteModelVersionAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}