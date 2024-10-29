using AutoMapper;
using Core.DTOs.Categories;
using Data.models._SP_;
using Data.models.nsCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.nsCategories.QuestionsCategoriesDTOs;

namespace Core.Profiles
{
    public class QuestionsCategoriesProfile : Profile
    {









        public QuestionsCategoriesProfile() {


            CreateMap<Questions_Categories, SendQuestionsCategoryDTO>();
            CreateMap<Questions_Categories, BaseSendQuestionsCategoryDTO>();


            CreateMap<CreateQuestionsCategoryDTO, Questions_Categories>();


            CreateMap<SP_QuestionCategories, SendQuestionsCategoryDTO>();

            

        }
    }
}
