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
   public  class MotorService : IMotorService
    {
        public MotorService(IMotorRepository motorRepository)
        {
            this.MotorRepository = motorRepository;
        }
        protected IMotorRepository MotorRepository { get; set; }

        public async Task<List<Motor>> GetAllAsync(MotorFilter filter, Sorting sort, Paging paging)
        {
        return await MotorRepository.GetAllAsync( filter,  sort,  paging);
        }


        public async Task<Motor> GetByIdAsync(Guid id)
        {
        return await MotorRepository.GetByIdAsync(id);
        }


        public async Task<bool> PostAsync(Motor motor)
        {

        return await MotorRepository.PostAsync(motor);

        }

        public async Task<bool> PutAsync(Motor motor)
        {
        motor.Generate();
        return await MotorRepository.PutAsync(motor);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
        return await MotorRepository.DeleteAsync(Id);
        }
        
    }
}
