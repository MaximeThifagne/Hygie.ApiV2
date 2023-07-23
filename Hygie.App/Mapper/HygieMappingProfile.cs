using AutoMapper;
using Hygie.App.Commands.Patients;
using Hygie.App.DTOs;
using Hygie.Core.Entities;

namespace Hygie.App.Mapper
{
    public class HygieMappingProfile : Profile
    {
        public HygieMappingProfile()
        {
            CreateMap<Patient, PatientResponseDTO>().ReverseMap();
            CreateMap<Patient, CreatePatientCommand>().ReverseMap();
            CreateMap<Patient, EditPatientCommand>().ReverseMap();
        }
    }
}