using AuTOP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model.DomainModels
{
    public class ModelDomainModel : IdDateBaseModel
    {
        private Guid manufacturerId;
        private string name;
        private string imageURL;
        private ManufacturerDomainModel manufacturer;
        private List<ModelVersion> modelVersions;




        public Guid ManufacturerId { get => manufacturerId; set => manufacturerId = value; }
        public string Name { get => name; set => name = value; }
        public List<ModelVersion> ModelVersions { get => modelVersions; set => modelVersions = value; }
        public ManufacturerDomainModel Manufacturer { get => manufacturer; set => manufacturer = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }
    }
}
