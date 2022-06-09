using AuTOP.Model;
using AuTOP.Model.Common;
using AuTOP.Repository;
using AuTOP.Repository.Common;
using AuTOP.Service.Common;
using AuTOP.Service;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AuTOP.Common;

namespace AuTOP.WebAPI.Provider
{
    public class OauthProvider : OAuthAuthorizationServerProvider
    {
        public OauthProvider()
        {
        }
        //public OauthProvider(IUserService userService)
        //{
        //    this.UserService = userService;
        //}
        //protected IUserService UserService { get; set; }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IUserRepository userRepository = new UserRepository();
            IUserService userService = new UserService(userRepository);

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var searcQuery = "";
            //var searchBy = "Username";
            string sortBy = "Username";
            string sortMethod = "ASC";
            int page = 1;

            UserFilter filter = new UserFilter(searcQuery);
            Sorting sorting = new Sorting(sortBy, sortMethod);
            Paging paging = new Paging(page);

            var users = await userService.GetAsync(filter,sorting,paging);



            if (users != null)
            {
                var user = users.Where(o => o.Username == context.UserName && o.Password == context.Password).FirstOrDefault();
                if (user != null)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                    identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                    await Task.Run(() => context.Validated(identity));
                }
                else
                {
                    context.SetError("Wrong Credentials", "Provided username and password is incorrect");
                }
            }
            else
            {
                context.SetError("Wrong Credentials", "Provided username and password is incorrect");
            }
            return;

        }
    }
}