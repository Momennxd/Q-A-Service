using Core.DTOs.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces.Questions
{
    public interface IAnswerExplanationService
    {

        Task<bool> AddNewAsync(AnswerExplanationDTOs.AnswerExplanationMainDTO answerExplanationDTO);
        Task<List<AnswerExplanationDTOs.GetAnswerExplanationDTO>> GetAnswerExplanationAsync(int QuestionID);

    }
}
