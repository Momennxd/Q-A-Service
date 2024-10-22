using Data.models.Collections;
using Data.models.Questions;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo.Collecs_Questions
{
    public interface IAnswerExplanationRepo: IRepository<AnswerExplanation>
    {
        Task<bool> AddExplainationAsync(AnswerExplanation answerExplanation);
        Task<List<AnswerExplanation>> GetExplainationByQuestionID(int QuestionID);
    }
}
