using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
    public class MotorFilter
    {
        private string name = "";
        private int year = 0;
        private int maxhp = 0;
        private string type = "";
        private int enginesize = 0;

        public string Name { get => name; set => name = value; }
        public int Year { get => year; set => year = value; }
        public int MaxHP { get => maxhp; set => maxhp = value; }
        public string Type { get => type; set => type = value; }
        public int EngineSize { get => enginesize; set => enginesize = value; }
    }
}
