﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.Collections;
using Data.Repositories;
using Data.models.Collections;

namespace Core.Profiles
{
    public class CollectionsProfile : Profile
    {


        public CollectionsProfile()
        {

            CreateMap<QCollection, CollectionsDTOs.CreateQCollectionDTO>();


            CreateMap<QCollection, CollectionsDTOs.SendCollectionDTO>();

            CreateMap<CollectionsDTOs.CreateQCollectionDTO, QCollection>()
           .ForMember(dest => dest.AddedTime, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));



        }

    }
}
