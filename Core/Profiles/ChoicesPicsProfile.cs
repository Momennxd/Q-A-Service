using AutoMapper;
using Core.DTOs.Pictures;
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

            CreateMap<Choices_Pics, DTOs.Pictures.ChoicesPicsDTOs.SendChoicePicDTO>();

            CreateMap<DTOs.Pictures.ChoicesPicsDTOs.CreateChoicePicDTO, Choices_Pics>();


            
        }







    }
}
