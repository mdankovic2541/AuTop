using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
    public class Sorting
    {
        private string sortBy;
        private string sortMethod;

        public Sorting() { }

        public Sorting(string sortBy, string sortMethod)
        {
            SortBy = sortBy;
            SortMethod = sortMethod;
        }

        public string SortBy { get => sortBy; set => sortBy = value; }
        public string SortMethod { get => sortMethod; set => sortMethod = value; }
    }
}
