using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.InputModel
{
    public class ModelInputModel
    {
        private Guid guid = Guid.Empty;
        private Guid manufacturerId;
        private string name;

        public Guid ManufacturerId { get => manufacturerId; set => manufacturerId = value; }
        public string Name { get => name; set => name = value; }
        public Guid Guid { get => guid; set => guid = value; }
    }
}