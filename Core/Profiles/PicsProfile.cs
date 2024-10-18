using AutoMapper;
using Core.DTOs.Pictures;
using Data.models.Pictures;
using Services.Interfaces;
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


            CreateMap<Pics, PicsDTOs.SendPicDTO>();

        }
    }
}
