using AuTOP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model
{
    public class Transmission : IdDateBaseModel, ITransmission
    {

        public string Name { get; set; }

        public int Gears { get; set; }

    }
}
