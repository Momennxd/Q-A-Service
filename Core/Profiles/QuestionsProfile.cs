using AutoMapper;
using Data.models._SP_;
using Data.models.Pictures;
using Data.models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.QuestionsDTOs;

namespace Core.Profiles
{
    public class QuestionsProfile : Profile
    {








        public QuestionsProfile()
        {
            CreateMap<Question, PatchQuestionDTO>();
            CreateMap<PatchQuestionDTO, Question>();

            CreateMap<Question, SendQuestionDTO>();
            CreateMap<SP_Question, SendQuestionDTO>();

            CreateMap<CreateQuestionDTO, Question>()
           .ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.Now));



        }









    }
}
