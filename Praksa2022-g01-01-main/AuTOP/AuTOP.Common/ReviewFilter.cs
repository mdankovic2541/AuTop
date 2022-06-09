using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
    public class ReviewFilter
    {
        //private string searchBy;
        private string searchQuery;
        private int searchByRating;
        private Guid? modelVersionId = null;
        public ReviewFilter() { }

        public ReviewFilter(string searchQuery, int searchByRating)
        {
            SearchQuery = searchQuery;

            SearchByRating = searchByRating;
        }

        //public string SearchBy { get => searchBy; set => searchBy = value; }
        public string SearchQuery { get => searchQuery; set => searchQuery = value; }
        public int SearchByRating { get => searchByRating; set => searchByRating = value; }
        public Guid? ModelVersionId { get => modelVersionId; set => modelVersionId = value; } 
    }
}
