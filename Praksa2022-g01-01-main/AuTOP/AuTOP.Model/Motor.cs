using AuTOP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model
{
    public class Motor : IdDateBaseModel, IMotor
    {

        public int Year { get; set; }

        public string Type { get; set; }

        public int MaxHP { get; set; }

        public int EngineSize { get; set; }

        public string Name { get; set; }
    }
}
