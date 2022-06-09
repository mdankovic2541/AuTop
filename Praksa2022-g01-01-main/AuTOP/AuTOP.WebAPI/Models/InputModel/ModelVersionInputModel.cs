using AuTOP.Model;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.InputModel
{
    public class ModelVersionInputModel
    {
        public Guid ModelId { get; set; }

        public Guid MotorId { get; set; }

        public Guid BodyShapeId { get; set; }

        public Guid TransmissionId { get; set; }

        public decimal FuelConsumption { get; set; }

        public int Year { get; set; }

        public decimal Acceleration { get; set; }

        public int Doors { get; set; }
        public string Name { get => name; set => name = value; }

        private string name;
    }
}