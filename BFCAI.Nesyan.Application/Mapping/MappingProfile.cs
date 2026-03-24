using AutoMapper;
using BFCAI.Nesyan.Application.Abstraction.Models.Doctors;
using BFCAI.Nesyan.Domain.Entities.Primary;
using BFCAI.Nesyan.Domain.Entities.Primary.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorToReturnDto>();
            CreateMap<DoctorToCreateDto, Doctor>()
                    .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender, true)));
        }
    }
}
