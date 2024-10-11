using AutoMapper;
using Data.models.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Profiles
{
    public class ChoicesPicsProfile : Profile
    {









        public ChoicesPicsProfile() {

            CreateMap<ChoicesPics, DTOs.Pictures.ChoicesPicsDTOs.SendChoicePicDTO>();

            CreateMap<DTOs.Pictures.ChoicesPicsDTOs.CreateChoicePicDTO, ChoicesPics>();
        }







    }
}
