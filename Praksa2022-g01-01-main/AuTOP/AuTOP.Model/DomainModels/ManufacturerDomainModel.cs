using AuTOP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model.DomainModels
{
    public class ManufacturerDomainModel : IdDateBaseModel
    {
        private string name;
        private string imageURL;
        private List<ModelDomainModel> models;


        public string Name { get => name; set => name = value; }
        public List<ModelDomainModel> Models { get => models; set => models = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }
    }
}
