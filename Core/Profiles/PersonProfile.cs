using AutoMapper;
using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.People.PeopleDTOs;

namespace Core.Profiles
{
    public class PersonProfile : Profile
    {


        public PersonProfile()
        {

            CreateMap<AddPersonDTO, Person>()
            .ForMember(dest => dest.JoinedDate, opt => opt.MapFrom(src => DateTime.Now));
        
            CreateMap<Person, SendPersonDTO>();

        }










    }
}
