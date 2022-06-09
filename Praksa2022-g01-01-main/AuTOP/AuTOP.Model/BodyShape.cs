using AuTOP.Model.Common;
using AuTOP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model
{
    public class BodyShape : IdDateBaseModel, IBodyShape
    {

        public string Name { get; set; }

    }
}
