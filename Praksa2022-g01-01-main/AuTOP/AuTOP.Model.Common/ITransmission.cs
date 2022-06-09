using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model.Common
{
    public interface ITransmission
    {

        string Name { get; set; }

        int Gears { get; set; }
    }
}
