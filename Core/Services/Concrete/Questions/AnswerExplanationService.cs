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

        public async Task<bool> AddNewAsync(AnswerExplanationDTOs.AddAnswerExplanationDTO answerExplanationDTO)
        {
            await _unitOfWork.EntityRepo.AddExplaination(_mapper.Map<AnswerExplanation>(answerExplanationDTO));
            await _unitOfWork.CompleteAsync();
            return true; 
        }
    }
}
