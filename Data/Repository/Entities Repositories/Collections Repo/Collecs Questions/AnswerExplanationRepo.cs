using Data.DatabaseContext;
using Data.models.Questions;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Entities_Repositories.Collections_Repo.Collecs_Questions
{
    public class AnswerExplanationRepo : Repository<AnswerExplanation>, IAnswerExplanationRepo
    {

        private AppDbContext _appDbContext;
        public AnswerExplanationRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<bool> AddExplainationAsync(AnswerExplanation answerExplanation)
        {
            await _appDbContext.Questions_Answer_Explanation.AddAsync(answerExplanation);
            return true;
        }

        public async Task<List<AnswerExplanation>> GetExplainationByQuestionID(int questionID)
        {
            return await _appDbContext.Questions_Answer_Explanation
                       .Where(x => x.QuestionID == questionID)
                       .ToListAsync();
        }

    }
}
