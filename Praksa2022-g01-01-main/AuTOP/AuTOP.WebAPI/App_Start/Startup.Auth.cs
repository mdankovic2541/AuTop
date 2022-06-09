using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AuTOP.WebAPI.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using AuTOP.Service.Common;

[assembly: OwinStartup(typeof(AuTOP.WebAPI.App_Start.Startup))]

namespace AuTOP.WebAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            //this is very important line cross orgin source(CORS)it is used to enable cross-site HTTP requests  
            //For security reasons, browsers restrict cross-origin HTTP requests  
            app.UseCors(CorsOptions.AllowAll);

            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),//token expiration time  
                Provider = new OauthProvider()
            };

            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);//register the request  
        }
    }

}