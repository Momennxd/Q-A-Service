using AutoMapper;
using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Profiles
{
    internal class UsersProfile : Profile
    {


        public UsersProfile()
        {

            CreateMap<User, Core.DTOs.People.UsersDTOs.AddUserDTO>();

        }



    
    }
}
