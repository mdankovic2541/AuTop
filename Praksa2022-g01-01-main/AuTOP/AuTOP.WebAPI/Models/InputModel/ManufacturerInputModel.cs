using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.InputModel
{
    public class ManufacturerInputModel
    {
        private Guid id = Guid.Empty;
        private string name;

        public string Name { get => name; set => name = value; }
        public Guid Id { get => id; set => id = value; }
    }
}