using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
    public class ModelVersionFilter
    {
        private string name = "";
        private Guid? modelId = null;
        private Guid? motorId = null;
        private Guid? transmissionId = null;
        private Guid? bodyShapeId = null;
        private int powerFrom = 0;
        private int powerTo = 0;
        private int yearFrom = 0;
        private int yearTo = 0;

        public Guid? ModelId { get => modelId; set => modelId = value; }
        public Guid? MotorId { get => motorId; set => motorId = value; }
        public Guid? TransmissionId { get => transmissionId; set => transmissionId = value; }
        public Guid? BodyShapeId { get => bodyShapeId; set => bodyShapeId = value; }
        public string Name { get => name; set => name = value; }
        public int PowerFrom { get => powerFrom; set => powerFrom = value; }
        public int PowerTo { get => powerTo; set => powerTo = value; }
        public int YearFrom { get => yearFrom; set => yearFrom = value; }
        public int YearTo { get => yearTo; set => yearTo = value; }
    }
}
