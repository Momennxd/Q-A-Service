using AutoMapper;
using Core.DTOs.Pictures;
using Data.models.Pictures;

namespace Core.Profiles
{
    public class PicsProfile : Profile
    {


        public PicsProfile() {


            CreateMap<Pics, PicsDTOs.SendPicDTO>();

        }
    }
}
