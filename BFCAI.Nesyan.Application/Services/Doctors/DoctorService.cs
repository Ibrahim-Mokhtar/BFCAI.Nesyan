using AutoMapper;
using BFCAI.Nesyan.Application.Abstraction.Models.Doctors;
using BFCAI.Nesyan.Application.Abstraction.Services.Doctors;
using BFCAI.Nesyan.Domain.Contracts;
using BFCAI.Nesyan.Domain.Entities.Primary.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Services.Doctors
{
    public class DoctorService(IUnitOfWork UnitOfWork,IMapper Mapper) : IDoctorService
    {
        public async Task<IEnumerable<DoctorToReturnDto>> GetDoctorsAsync()
        {
            var doctors =await UnitOfWork.GetRepository<Doctor, int>().GetAllAsync();
            var doctorsToReturn = Mapper.Map<IEnumerable<DoctorToReturnDto>>(doctors);
            return doctorsToReturn;
        }
        public async Task<DoctorToReturnDto> GetDoctorAsync(int id)
        {
            var doctor =await UnitOfWork.GetRepository<Doctor, int>().Get(id);
            var doctorToReturn = Mapper.Map<DoctorToReturnDto>(doctor);
            return doctorToReturn;
        }
        public async Task<DoctorToReturnDto> CreateDoctorAsync(DoctorToCreateDto doctorToCreate)
        {
            var doctor = Mapper.Map<Doctor>(doctorToCreate);
            var repo = UnitOfWork.GetRepository<Doctor, int>();
            await repo.AddAsync(doctor);
            await UnitOfWork.CompleteAsync();
            return Mapper.Map<DoctorToReturnDto>(doctor);
        }
        public async Task UpdateDoctorAsync(DoctorToReturnDto doctorToUpdate)
        {
            var repo = UnitOfWork.GetRepository<Doctor, int>();
            var doctor = await repo.Get(doctorToUpdate.Id);
            if (doctor is null)
                throw new Exception("Doctor not found");
            Mapper.Map(doctorToUpdate, doctor);
            repo.Update(doctor);
            await UnitOfWork.CompleteAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var repo = UnitOfWork.GetRepository<Doctor, int>();
            var doctor = await repo.Get(id);
            if (doctor is null)
                throw new Exception("Doctor not found");
            repo.Delete(doctor);
            await UnitOfWork.CompleteAsync();
        }

    }
}
