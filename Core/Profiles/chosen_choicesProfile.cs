using Core.DTOs.Questions;
using Data.models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.chosen_choicesDTOs;
using static Core.DTOs.Questions.QuestionsDTOs;
using AutoMapper;

namespace Core.Profiles
{
    public class chosen_choicesProfile : Profile
    {

        public chosen_choicesProfile() {

            CreateMap<Chosen_Choices, send_chosen_choicesDTO>();
            CreateMap<Add_chosen_choicesDTO, Chosen_Choices>();


        }
    }
}
