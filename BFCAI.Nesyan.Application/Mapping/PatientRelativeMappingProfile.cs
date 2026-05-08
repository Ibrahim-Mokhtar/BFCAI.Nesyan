using AutoMapper;
using BFCAI.Nesyan.Application.Abstraction.Models.Patients;
using BFCAI.Nesyan.Application.Abstraction.Models.Relatives;
using BFCAI.Nesyan.Domain.Entities.Primary.Patients;
using BFCAI.Nesyan.Domain.Entities.Primary.Relatives;
using BFCAI.Nesyan.Domain.Entities.Relations.Primary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Mapping
{
    internal class PatientRelativeMappingProfile:Profile
    {
        public PatientRelativeMappingProfile()
        {

            CreateMap<PatientRelative, PatientSummaryDto>()
            .ForMember(
                dest => dest.PatientId,
                opt => opt.MapFrom(src =>
                    src.Patient.Id))

            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src =>
                    src.Patient.FName + " " +
                    src.Patient.LName))

            .ForMember(
            dest => dest.Age,
            opt => opt.MapFrom(src =>
                src.Patient.Age))

            .ForMember(
                dest => dest.Gender,
                opt => opt.MapFrom(src =>
                    src.Patient.Gender))

            .ForMember(
                dest => dest.CurrentStage,
                opt => opt.MapFrom(src =>
                    src.Patient.CurrentStage));


            CreateMap<Relative, RelativeSummaryDto>()
             .ForMember(
                 dest => dest.RelativeId,
                 opt => opt.MapFrom(src => src.Id))
             .ForMember(
                 dest => dest.FullName,
                 opt => opt.MapFrom(src =>
                     $"{src.FName} {src.LName}"));

            CreateMap<Patient, PatientSummaryDto>()
                .ForMember(
                    dest => dest.PatientId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src =>
                        $"{src.FName} {src.LName}"));
        }
    }
}
