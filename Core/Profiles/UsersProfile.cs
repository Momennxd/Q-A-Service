using AutoMapper;
using Data.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.People.UsersDTOs;

namespace Core.Profiles
{
    internal class UsersProfile : Profile
    {


        public UsersProfile()
        {

            CreateMap<AddUserDTO, User >();
            CreateMap<User, AddUserDTO>();
            CreateMap<SendUserDTO, User>();
            CreateMap< User, SendUserDTO>();

        }




    }
}
