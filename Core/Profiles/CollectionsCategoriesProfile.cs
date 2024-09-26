using AutoMapper;
using Core.DTOs.Collections;
using Core_Layer.models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Profiles
{
    internal class CollectionsCategoriesProfile : Profile
    {





        public CollectionsCategoriesProfile() {



            CreateMap<CollectionsCategories, CollectionCategoriesDTOs.SendCollectionCategoryDTO>();




        }


    }
}
