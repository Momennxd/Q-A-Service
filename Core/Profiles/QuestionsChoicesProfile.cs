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
using static Core.DTOs.Questions.QuestionsChoicesDTOs;
using Data.models._SP_;

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

            CreateMap<SendSimpleChoiceDTO, QuestionsChoices>();
            CreateMap<QuestionsChoices, SendSimpleChoiceDTO>();

            CreateMap<SP_ChoiceWithExplanation, SendChoiceWithExplanationDTO>();
            CreateMap<SendChoiceWithExplanationDTO ,  SP_ChoiceWithExplanation>();

        }

    }
}
