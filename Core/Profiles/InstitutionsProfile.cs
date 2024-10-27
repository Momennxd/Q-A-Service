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


            CreateMap<InstitutionsDTOs.SigninDTO, Institution>()
    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.userInfo));
            CreateMap<InstitutionsDTOs.SigninDTO, User>();


        }
    }
}
