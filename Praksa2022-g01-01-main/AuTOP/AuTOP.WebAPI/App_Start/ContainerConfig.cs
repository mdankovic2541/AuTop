using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using AuTOP.Repository;
using AuTOP.Service;
using AuTOP.WebAPI.App_Start;
using AuTOP.WebAPI.Provider;
using Microsoft.Owin.Security.OAuth;

namespace AuTOP.WebAPI
{
    public class ContainerConfig
    {


        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            builder.RegisterType<OauthProvider>().As<IOAuthAuthorizationServerProvider>().SingleInstance();

            builder.RegisterModule(new RepositoryDIModule());
            builder.RegisterModule(new ServiceDIModule());

            builder.RegisterAutoMapper(typeof(AutoMapperProfile).Assembly);

            Container = builder.Build();

            return Container;
        }

    }
}