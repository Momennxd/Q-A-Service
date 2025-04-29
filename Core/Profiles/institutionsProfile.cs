using AutoMapper;
using Core.DTOs.Institution;
using Data.models.Institutions;
using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.People.PeopleDTOs;

namespace Core.Profiles
{
    public class institutionsProfile : Profile
    {
        public institutionsProfile()
        {





            CreateMap<InstitutionsDTOs.CreateInstitutionDTO, Person>()
           .ForMember(dest => dest.JoinedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<InstitutionsDTOs.CreateInstitutionDTO, User>();
            CreateMap<InstitutionsDTOs.CreateInstitutionDTO, Institution>();



            CreateMap<InstitutionsDTOs.PatchInstitutionDTO, InstitutionsDTOs.SendInstitutionDTO>();
            CreateMap<InstitutionsDTOs.PatchInstitutionDTO, Institution>();
            CreateMap<Institution, InstitutionsDTOs.PatchInstitutionDTO>();


            CreateMap<Person, InstitutionsDTOs.SendInstitutionDTO>();
            CreateMap<User, InstitutionsDTOs.SendInstitutionDTO>();
            CreateMap<Institution, InstitutionsDTOs.SendInstitutionDTO>();

        }
    }
}
