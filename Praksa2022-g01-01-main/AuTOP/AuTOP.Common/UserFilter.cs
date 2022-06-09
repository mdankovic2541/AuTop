using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
    public class UserFilter
    {
        //private string searchBy;
        private string searchQuery;

        public UserFilter() { }
        public UserFilter(string searchQuery)
        {
            SearchQuery = searchQuery;
        }
        //public string SearchBy { get => searchBy; set => searchBy = value; }
        public string SearchQuery { get => searchQuery; set => searchQuery = value; }
    }
}
