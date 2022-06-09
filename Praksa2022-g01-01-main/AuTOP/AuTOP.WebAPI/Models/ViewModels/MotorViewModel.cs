using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.ViewModels
{
    public class MotorViewModel
    {
        private Guid id;
        private string name;

        public string Name { get => name; set => name = value; }

        public Guid Id { get => id ; set => id = value; }
    }
}