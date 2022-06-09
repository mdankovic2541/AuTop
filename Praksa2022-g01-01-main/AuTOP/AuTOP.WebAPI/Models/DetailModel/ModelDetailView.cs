using AuTOP.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.DetailModel
{
    public class ModelDetailView
    {
        private Guid id;
        private string name;
        private string imageURL;
        private List<ModelVersionViewModel> modelVersions;

        public Guid Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }
        public List<ModelVersionViewModel> ModelVersions { get => modelVersions; set => modelVersions = value; }
    }
}