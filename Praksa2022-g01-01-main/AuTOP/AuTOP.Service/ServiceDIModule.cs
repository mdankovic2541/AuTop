using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Service.Common;

namespace AuTOP.Service
{
   public class ServiceDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ManufacturerServis>().As<IManufacturerServis>();
            //builder.RegisterType<CourseRepository>().As<ICourseRepository>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ReactionService>().As<IReactionService>();
            builder.RegisterType<ModelService>().As<IModelService>();
            builder.RegisterType<ModelVersionService>().As<IModelVersionService>();
            builder.RegisterType<MotorService>().As<IMotorService>();
            builder.RegisterType<ReviewService>().As<IReviewService>();
            builder.RegisterType<BodyShapeService>().As<IBodyShapeService>();
            builder.RegisterType<TransmissionService>().As<ITransmissionService>();
        }
    }
}
