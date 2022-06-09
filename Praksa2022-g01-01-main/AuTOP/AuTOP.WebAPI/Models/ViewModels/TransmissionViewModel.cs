using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.ViewModels
{
    public class TransmissionViewModel
    {
        private Guid id;
        private string name;
        private int gears;

        public string Name { get => name; set => name = value; }

        public Guid Id { get => id; set => id = value; }

        public int Gears { get => gears; set => gears = value; }
    }

}