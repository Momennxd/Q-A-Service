﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.Collections;
using Data.Repositories;
using Data.models.Collections;
using static Core.DTOs.Collections.CollectionsDTOs;
using static Core.DTOs.Collections.CollectionsReviewsDTOs;

namespace Core.Profiles
{
    public class CollectionsProfile : Profile
    {


        public CollectionsProfile()
        {


            CreateMap<QCollection, SendCollectionDTO_Full>();
            CreateMap<QCollection, SendCollectionDTO_Thumb>();
            CreateMap<QCollection, SendCollectionDTO_Search>();
            CreateMap<QCollection, PatchQCollectionDTO>();
            CreateMap<PatchQCollectionDTO, QCollection>();

            CreateMap<CollectionsDTOs.CreateQCollectionDTO, QCollection>()
           .ForMember(dest => dest.AddedTime, opt => opt.MapFrom(src => DateTime.Now));




            // Map CollectionsReview to CollectionsReviewDto
            CreateMap<Collections_Reviews, CreateCollectionsReviewDTO>();

            CreateMap<CreateCollectionsReviewDTO, Collections_Reviews>();


            CreateMap<Collections_Reviews, SendCollectionsReviewsDTO>();

        }

    }
}
