using AutoMapper;
using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Service.Common;
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
    public class TransmissionController : ApiController
    {
         
        private IMapper mapper;
        protected ITransmissionService TransmissionService { get; set; }

        public TransmissionController(ITransmissionService transmissionService, IMapper mapper)
        {
            this.TransmissionService = transmissionService;
            this.mapper = mapper;
        }

        
        

        public async Task<HttpResponseMessage> GetAllAsync([FromUri] TransmissionFilter filter, string sortBy = "Name", string sortMethod = "", int page = 1)
        {
            if (filter == null)
            {
                filter = new TransmissionFilter();
            }
            Sorting sorting = new Sorting(sortBy, sortMethod);
            Paging paging = new Paging(page);
            List<Transmission> bodyShapes = await TransmissionService.GetAllAsync(filter, sorting, paging);
            List<TransmissionViewModel> motorView = mapper.Map<List<Transmission>, List<TransmissionViewModel>>(bodyShapes);


            return Request.CreateResponse(HttpStatusCode.OK, motorView);
        }
        public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
        {
            Transmission bodyShape = await TransmissionService.GetByIdAsync(id);
            TransmissionViewModel bodyShapeView = mapper.Map<Transmission, TransmissionViewModel>(bodyShape);
            return Request.CreateResponse(HttpStatusCode.OK, bodyShapeView);
        }
    }
}

