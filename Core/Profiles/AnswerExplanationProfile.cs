using AutoMapper;
using Core.DTOs.Questions;
using Data.models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Profiles
{
    public class AnswerExplanationProfile : Profile
    {
        public AnswerExplanationProfile()
        {
            CreateMap<AnswerExplanation, AnswerExplanationDTOs.AnswerExplanationMainDTO>();
            CreateMap<AnswerExplanationDTOs.AnswerExplanationMainDTO, AnswerExplanation>();


            CreateMap<AnswerExplanationDTOs.GetAnswerExplanationDTO, AnswerExplanation>();

            CreateMap<AnswerExplanation, AnswerExplanationDTOs.GetAnswerExplanationDTO>();
            CreateMap<AnswerExplanation, AnswerExplanationDTOs.AnswerExplanationMainDTO>();
        }
    }
}
