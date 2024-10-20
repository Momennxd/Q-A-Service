using Core.DTOs.Collections;
using Data.models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.models.Questions;
using Core.DTOs.Questions;

namespace Core.Profiles
{
    public class QuestionsChoicesProfile : Profile
    {



        public QuestionsChoicesProfile()
        {

            CreateMap<QuestionsChoices, QuestionsChoicesDTOs.SendChoiceDTO>();

            CreateMap<QuestionsChoicesDTOs.CreateChoiceDTO, QuestionsChoices>();


            CreateMap<QuestionsChoices, QuestionsChoicesDTOs.PatchChoiceDTO>();
            CreateMap<QuestionsChoicesDTOs.PatchChoiceDTO, QuestionsChoices>();


        }

    }
}
