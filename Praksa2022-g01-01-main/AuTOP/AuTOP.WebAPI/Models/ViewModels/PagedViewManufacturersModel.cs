using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.ViewModels
{
    public class PagedViewManufacturersModel
    {
        List<ManufacturerViewModel> manufacturers;
        int totalItemCount;

        public List<ManufacturerViewModel> Manufacturers { get => manufacturers; set => manufacturers = value; }
        public int TotalItemCount { get => totalItemCount; set => totalItemCount = value; }
    }
}