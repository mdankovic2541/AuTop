using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
     public class TransmissionFilter
    {
        private string name = "";
        private int gears = 0;


        public string Name { get => name; set => name = value; }
        public int Gears { get => gears; set => gears = value; }
    }
}
