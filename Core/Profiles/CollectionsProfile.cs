using Core_Layer.models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core_Layer.models.Collections;

namespace Core.Profiles
{
    public class CollectionsProfile : Profile
    {


        public CollectionsProfile()
        {

            CreateMap<Qcollection, DTOs.Collections.QCollectionsDTOs.SendQCollectionDTO>();



            CreateMap<Qcollection, DTOs.Collections.QCollectionsDTOs.CreateQCollectionDTO>();


        }

    }
}
