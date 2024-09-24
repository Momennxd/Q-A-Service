using AutoMapper;
using Core_Layer.models.People;
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

            CreateMap<Person, DTOs.People.PeopleDTOs.AddPersonDTO>();

        }










    }
}
