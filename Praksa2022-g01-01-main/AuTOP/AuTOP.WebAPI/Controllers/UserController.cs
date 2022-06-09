using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Model.Common;
using AuTOP.Service;
using AuTOP.Service.Common;
using AuTOP.WebAPI.Models.ViewModels;
using AutoMapper;

namespace AuTOP.WebAPI.Controllers
{
    //[Authorize]
    public class UserController : ApiController
    {
        public UserController(IUserService userService, IMapper mapper)
        {
            this.UserService = userService;
            this.mapper = mapper; 
        }
        protected IUserService UserService { get; set; }
        private IMapper mapper;

        [Route("users")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] UserFilter filter, [FromUri] Sorting sorting, [FromUri] Paging paging)
        {            
            var users = await UserService.GetAsync(filter, sorting, paging);
            List<UserViewModel> usersView = new List<UserViewModel>();

            if (users != null)
            {
                foreach(User u in users)
                {
                    UserViewModel user = mapper.Map<IUser, UserViewModel>(u);
                    user.Id = u.Id;
                    usersView.Add(user);
                }
                return Request.CreateResponse(HttpStatusCode.OK, usersView);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("users/{userId}")]
        public async Task<HttpResponseMessage> GetByIdAsync(Guid userId)
        {
            var user = await UserService.GetByIdAsync(userId);
            UserViewModel userView = new UserViewModel();            
            if (user != null)
            {
                userView = mapper.Map<IUser, UserViewModel>(user);

                return Request.CreateResponse(HttpStatusCode.OK, userView);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("users")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] User user)
        {
            User userPost = user;
            var status = await UserService.PostAsync(userPost);

            if (status)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("users/{id}")]
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] User user)
        {
            User userPut = user;
            var status = await UserService.PutAsync(id, userPut);

            if(status)
            {                
                return Request.CreateResponse(HttpStatusCode.OK, "User updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("users/{id}")]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {

            var status = await UserService.DeleteAsync(id);
            if (status)
            {                
                return Request.CreateResponse(HttpStatusCode.OK, "User deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"User with Id:{id} not found");
            }
        }

        [Route("username/{username}")]
        public async Task<HttpResponseMessage> GetIdByUsername(string username)
        {
            var user = await UserService.GetIdbyName(username);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"User with username:{username} not found");
            }
        }
    }
}
