using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model.DomainModels
{
    public class PagedManufacturersModel
    {
        private List<ManufacturerDomainModel> manufacturers;
        private int totalItemCount;

        public List<ManufacturerDomainModel> Manufacturers { get => manufacturers; set => manufacturers = value; }
        public int TotalItemCount { get => totalItemCount; set => totalItemCount = value; }
    }
}
