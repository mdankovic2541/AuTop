using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
    public class ManufacturerFilter
    {
        private string name = "";
        private bool dontGetModels = true;



        public bool DontGetModels { get => dontGetModels; set => dontGetModels = value; }
        public string Name { get => name; set => name = value; }
    }
}
