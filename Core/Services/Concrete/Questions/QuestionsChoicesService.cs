using AutoMapper;
using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete.Questions
{
    public class QuestionsChoicesService : IQuestionsChoicesService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> _unitOfWork;


        public QuestionsChoicesService
            (IMapper mapper, IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> uowChoices)
        {
            _unitOfWork = uowChoices;
            _mapper = mapper;
        }



        public Task<QuestionsChoicesDTOs.SendChoiceDTO> AddChoiceAsync
            (QuestionsChoicesDTOs.CreateChoiceDTO createChoiceDto)
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteChoiceAsync(int choiceID)
        {
            throw new NotImplementedException();
        }


        public Task<QuestionsChoicesDTOs.SendChoiceDTO> FindAsync(int choice)
        {
            throw new NotImplementedException();
        }


        public Task<List<QuestionsChoicesDTOs.SendChoiceDTO>> GetAllQuestionChoiceAsync(int QuestionID)
        {
            throw new NotImplementedException();
        }


        public Task<QuestionsChoicesDTOs.SendChoiceDTO> UpdateChoiceAsync
            (QuestionsChoicesDTOs.CreateChoiceDTO updateChoiceDto, int choiceID)
        {
            throw new NotImplementedException();
        }
    }
}
