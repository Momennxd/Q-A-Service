using AutoMapper;
using Core.DTOs.Questions;
using Core.Services.Interfaces.Questions;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Collections_Repo.Collecs_Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.AnswerExplanationDTOs;

namespace Core.Services.Concrete.Questions
{
    public class AnswerExplanationService : IAnswerExplanationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IAnswerExplanationRepo, AnswerExplanation> _unitOfWork;

        public AnswerExplanationService(IMapper mapper, IUnitOfWork<IAnswerExplanationRepo, AnswerExplanation> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddNewAsync(AnswerExplanationDTOs.AnswerExplanationMainDTO answerExplanationDTO)
        {
            var entity = _mapper.Map<AnswerExplanation>(answerExplanationDTO);
            entity.AddedDate = DateTime.Now;
            await _unitOfWork.EntityRepo.AddExplainationAsync(_mapper.Map<AnswerExplanation>(answerExplanationDTO));
            await _unitOfWork.CompleteAsync();
            return true; 
        }

        public async Task<GetAnswerExplanationDTO> GetAnswerExplanationAsync(int ExplainaID)
        {
            return _mapper.Map<GetAnswerExplanationDTO>(await _unitOfWork.EntityRepo.FindAsync(ExplainaID));
        }

        public async Task<List<AnswerExplanationDTOs.GetAnswerExplanationDTO>> GetAnswerExplanationByQuestionIDAsync(int QuestionID)
        {
            var entities = await _unitOfWork.EntityRepo.GetExplainationByQuestionID(QuestionID);
            return _mapper.Map<List<AnswerExplanationDTOs.GetAnswerExplanationDTO>>(entities);
        }

    }
}
