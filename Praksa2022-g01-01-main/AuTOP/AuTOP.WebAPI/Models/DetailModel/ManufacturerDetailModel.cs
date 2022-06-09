using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.DetailModel
{
    public class ManufacturerDetailModel
    {
        private string name;
        private string imageURL;
        private List<ModelViewModel> models;
        private Guid id;
        public string Name { get => name; set => name = value; }
        public List<ModelViewModel> Models { get => models; set => models = value; }
        public Guid Id { get => id; set => id = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }
    }
}