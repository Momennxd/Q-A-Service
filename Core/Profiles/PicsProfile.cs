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
    public class PicsProfile : Profile
    {



        public PicsProfile() {

            CreateMap<PicsDTOs.CreatePicDTOs, Pics>();

            CreateMap<Pics, PicsDTOs.SendPicDTOs>();


        }
    }
}
