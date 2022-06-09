using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model.Common
{
    public interface IModelVersion
    {

        Guid ModelId { get; set; }

        Guid MotorId { get; set; }

        Guid BodyShapeId { get; set; }

        Guid TransmissionId { get; set; }

        decimal FuelConsumption { get; set; }

        int Year { get; set; }

        decimal Acceleration { get; set; }

        int Doors { get; set; }

    }
}
