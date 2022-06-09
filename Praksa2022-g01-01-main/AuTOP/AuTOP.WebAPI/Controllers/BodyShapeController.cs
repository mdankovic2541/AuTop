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
    public class BodyShapeController : ApiController
    {
        private IMapper mapper;
        protected IBodyShapeService BodyShapeService { get; set; }
        public BodyShapeController(IBodyShapeService bodyShapeService, IMapper mapper)
        {
            this.BodyShapeService = bodyShapeService;
            this.mapper = mapper;
        }        


        public async Task<HttpResponseMessage> GetAllAsync([FromUri] BodyShapeFilter filter, string sortBy = "Name", string sortMethod = "", int page = 1)
        {
            if (filter == null)
            {
                filter = new BodyShapeFilter();
            }
            Sorting sorting = new Sorting(sortBy, sortMethod);
            Paging paging = new Paging(page);
            List<BodyShape> bodyShapes = await BodyShapeService.GetAllAsync(filter, sorting, paging);
            List<BodyShapeViewModel> motorView = mapper.Map<List<BodyShape>, List<BodyShapeViewModel>>(bodyShapes);


            return Request.CreateResponse(HttpStatusCode.OK, motorView);
        }
        public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
        {
            BodyShape bodyShape = await BodyShapeService.GetByIdAsync(id);
            BodyShapeViewModel bodyShapeView = mapper.Map<BodyShape, BodyShapeViewModel>(bodyShape);
            return Request.CreateResponse(HttpStatusCode.OK, bodyShapeView);
        }
    }
}
