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
    public class MotorController : ApiController
    {
        private IMapper mapper;
       
        public MotorController(IMotorService motorService, IMapper mapper)
        {
            this.MotorService = motorService;
            this.mapper = mapper;
        }
        protected IMotorService MotorService { get; set; }

        public async Task<HttpResponseMessage> GetAllAsync([FromUri]MotorFilter filter, string sortBy = "Name", string sortMethod = "", int page = 1)
        {
            if (filter == null)
            {
                filter = new MotorFilter();
            }
            Sorting sorting = new Sorting(sortBy, sortMethod);
            Paging paging = new Paging(page);           
            List<Motor> motors = await MotorService.GetAllAsync(filter, sorting, paging);
            List<MotorViewModel> motorView = mapper.Map<List<Motor>, List<MotorViewModel>>(motors);


            return Request.CreateResponse(HttpStatusCode.OK, motorView);
        }
        public async Task<HttpResponseMessage> GetByIdAsync(Guid id)
        {
            Motor motor = await MotorService.GetByIdAsync(id);
            MotorViewModel motorView = mapper.Map<Motor, MotorViewModel>(motor);
            return Request.CreateResponse(HttpStatusCode.OK,motorView);
        }

        public async Task<HttpResponseMessage> PostAsync(Motor motor)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await MotorService.PostAsync(motor));
        }

        public async Task<HttpResponseMessage> PutAsync(Motor motor)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await MotorService.PutAsync(motor));
        }

        public async Task<HttpResponseMessage> DeleteAsync(Guid Id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await MotorService.DeleteAsync(Id));
        }
    }
    
}
