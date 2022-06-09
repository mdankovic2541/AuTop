using Autofac;
using AuTOP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Repository.Common;

namespace AuTOP.Repository
{
   public class RepositoryDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ManufacturerRepository>().As<IManufacturerRepository>();
            builder.RegisterType<ModelRepository>().As<IModelRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ModelVersionRepository>().As<IModelVersionRepository>();
            builder.RegisterType<BodyShapeRepository>().As<IBodyShapeRepository>();
            builder.RegisterType<MotorRepository>().As<IMotorRepository>();
            builder.RegisterType<TransmissionRepository>().As<ITransmissionRepository>();
            builder.RegisterType<ReactionRepository>().As<IReactionRepository>();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            
        }
    }
}
