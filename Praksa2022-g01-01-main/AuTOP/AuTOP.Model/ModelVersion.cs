using AuTOP.Model.Common;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model
{
    public class ModelVersion : IdDateBaseModel, IModelVersion
    {

        public Guid ModelId { get; set; }

        public Guid MotorId { get; set; }

        public Guid BodyShapeId { get; set; }

        public Guid TransmissionId { get; set; }

        public decimal FuelConsumption { get; set; }

        public int Year { get; set; }

        public decimal Acceleration { get; set; }

        public int Doors { get; set; }
        public ModelDomainModel Model { get => model; set => model = value; }
        public Motor Motor { get => motor; set => motor = value; }
        public BodyShape BodyShape { get => bodyShape; set => bodyShape = value; }
        public Transmission Transmission { get => transmission; set => transmission = value; }
        public List<Review> Reviews { get => reviews; set => reviews = value; }
        public string Name { get => name; set => name = value; }

        private ModelDomainModel model;
        private Motor motor;
        private BodyShape bodyShape;
        private Transmission transmission;
        private List<Review> reviews;
        private string name;
        public double TotalRating { get; set; }

        

    }
}
