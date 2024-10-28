using AutoMapper;
using Core.DTOs.Categories;
using Data.models.nsCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Profiles
{
    public class CategoriesProfile : Profile
    {

        public CategoriesProfile() {

            CreateMap<Categories, CategoriesDTOs.SendCategoryDTO>();

            CreateMap<CategoriesDTOs.CreateCategoryDTO, Categories>();
        }




    }
}
