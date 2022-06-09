using AuTOP.Model;
using AuTOP.Model.Common;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.DetailModel
{
    public class ModelVersionDetailModel
    {
        private Guid id;
        private string name;
        private decimal fuelConsumption;
        private int year;
        private decimal acceleration;
        private int doors;
        private ModelViewModel model;
        private Motor motor;
        private Transmission transmission;
        private BodyShape bodyShape;
        private List<IReview> reviews;

        public Guid Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public decimal FuelConsumption { get => fuelConsumption; set => fuelConsumption = value; }
        public int Year { get => year; set => year = value; }
        public decimal Acceleration { get => acceleration; set => acceleration = value; }
        public int Doors { get => doors; set => doors = value; }
        public ModelViewModel Model { get => model; set => model = value; }
        public Motor Motor { get => motor; set => motor = value; }
        public Transmission Transmission { get => transmission; set => transmission = value; }
        public BodyShape BodyShape { get => bodyShape; set => bodyShape = value; }
        public List<IReview> Reviews { get => reviews; set => reviews = value; }
        public double TotalRating { get; set; }
    }
}