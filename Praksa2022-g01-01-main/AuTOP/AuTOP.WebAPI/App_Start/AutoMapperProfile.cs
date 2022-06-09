using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AutoMapper;
using AuTOP.WebAPI.Models;
using AuTOP.WebAPI.Models.DetailModel;
using AuTOP.Model;
using AuTOP.Model.Common;
using AuTOP.WebAPI.Models.ViewModels;
using AuTOP.WebAPI.Models.InputModel;

namespace AuTOP.WebAPI.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DataSet,ManufacturerDomainModel>();
            CreateMap<ModelDomainModel, ModelViewModel>();

            CreateMap<ManufacturerDomainModel, ManufacturerDetailModel>();
            CreateMap<ManufacturerDomainModel, ManufacturerViewModel>();
            CreateMap<ManufacturerInputModel, ManufacturerDomainModel>();
            CreateMap<PagedManufacturersModel, PagedViewManufacturersModel>();

            CreateMap<ModelDomainModel, ModelViewModel>();

            CreateMap<Motor, MotorViewModel>();

            CreateMap<ModelVersion, ModelVersionViewModel>();
            CreateMap<ModelVersion, ModelVersionDetailModel>();
            CreateMap<IUser, UserViewModel>();
            CreateMap<Review, ReviewViewModel>();
            CreateMap<Motor, MotorViewModel>();
            CreateMap<ModelVersionInputModel, ModelVersion>();
            CreateMap<BodyShape, BodyShapeViewModel>();
            CreateMap<Transmission, TransmissionViewModel>();
            
        }
        
    }
}