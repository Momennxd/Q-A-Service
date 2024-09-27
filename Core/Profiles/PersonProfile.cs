using AutoMapper;
using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Profiles
{
    public class PersonProfile : Profile
    {


        public PersonProfile()
        {

            CreateMap<DTOs.People.PeopleDTOs.AddPersonDTO, Person>()
            .ForMember(dest => dest.JoinedDate, opt => opt.MapFrom(src => DateTime.Now));
        
            CreateMap<Person, DTOs.People.PeopleDTOs.SendPersonDTO>();


        }










    }
}
