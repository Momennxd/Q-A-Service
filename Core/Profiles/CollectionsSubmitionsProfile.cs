using AutoMapper;
using Core.DTOs.Collections;
using Data.models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Profiles
{
    public class CollectionsSubmitionsProfile : Profile
    {
        public CollectionsSubmitionsProfile()
        {
            CreateMap<CollectionsSubmissionsDTOs.MainDTO, Collections_Submitions>();
            CreateMap<CollectionSubmissionView, CollectionsSubmissionsDTOs.CollectionSubmissionMainDTO>();

        }
    }
}
