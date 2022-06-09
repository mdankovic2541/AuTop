using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuTOP.WebAPI.Models.InputModel
{
    public class MotorInputModel
    {
        private Guid id = Guid.Empty;
        private int year;
        private string Type;
        private int maxHP;
        private int engineSize;
        private string name;

        public string Name { get => name; set => name = value; }
        public int EngineSize { get => engineSize; set => engineSize = value; }
        public int MaxHP { get => maxHP; set => maxHP = value; }
        public string Type1 { get => Type; set => Type = value; }
        public int Year { get => year; set => year = value; }
        public Guid Id { get => id; set => id = value; }
    }
}