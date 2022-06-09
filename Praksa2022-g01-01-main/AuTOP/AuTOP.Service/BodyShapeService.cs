using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Repository.Common;
using AuTOP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Service
{
    public  class BodyShapeService : IBodyShapeService
    {
        protected IBodyShapeRepository BodyShapeRepository { get; set; }
        public BodyShapeService(IBodyShapeRepository bodyShape)
            {
                this.BodyShapeRepository = bodyShape;
            }

       


        public async Task<List<BodyShape>> GetAllAsync(BodyShapeFilter filter, Sorting sort, Paging paging)
            {
                return await BodyShapeRepository.GetAllAsync(filter,sort,paging);
            }


            public async Task<BodyShape> GetByIdAsync(Guid id)
            {
                return await BodyShapeRepository.GetByIdAsync(id);
            }


         
        }
}
