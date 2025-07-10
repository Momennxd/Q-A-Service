using AutoMapper;
using Core.DTOs.Collections;
using Data.models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Collections.CollectionsSubmissionsDTOs;

namespace Core.Profiles
{
    public class CollectionsSubmitionsProfile : Profile
    {
        public CollectionsSubmitionsProfile()
        {
            CreateMap<Collections_Submitions, SendCollectionSubmissionThumbDTO>();

        }
    }
}
