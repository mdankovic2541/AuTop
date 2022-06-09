using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model.Common
{
    public abstract class IdDateBaseModel
    {
       private Guid id;
       private DateTime dateCreated;
       private DateTime dateUpdated;

        public DateTime DateCreated { get => dateCreated; set => dateCreated = value; }
        public DateTime DateUpdated { get => dateUpdated; set => dateUpdated = value; }
        public Guid Id { get => id; set => id = value; }

        public void Generate()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
            DateUpdated = DateTime.UtcNow;
        }

        public void GenerateUpdateDate()
        {
            DateUpdated = DateTime.UtcNow;
        }
    }
}
