using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
    public class ModelFilter
    {
        private string name = "";
        private Guid? manufacturerId = null;

        public Guid? ManufacturerId { get => manufacturerId; set => manufacturerId = value; }
        public string Name { get => name; set => name = value; }
    }
}
