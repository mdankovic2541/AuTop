using AuTOP.Model;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.ViewModels
{
    public class ModelVersionViewModel
    {
        private Guid id;
        private string name;
        private ModelViewModel model;

        public Guid Id { get => id; set => id = value; }
        public ModelViewModel Model { get => model; set => model = value; }
        public string Name { get => name; set => name = value; }
        public double TotalRating { get; set; }
    }
}