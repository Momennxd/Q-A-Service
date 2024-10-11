using Data.DatabaseContext;
using Data.models.Questions;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices
{
    public class QuestionsChoicesRepo : Repository<QuestionsChoices>, IQuestionsChoicesRepo
    {

        AppDbContext _appDbContext;

        public QuestionsChoicesRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<List<QuestionsChoices>> GetAllByQuestionIDAsync(int Questionid)
        {
           return await _appDbContext.Questions_Choices.Select(c => c).
                Where(c => c.QuestionID == Questionid).ToListAsync();
        }

        public async Task<List<QuestionsChoices>> GetAllRightAnswersAsync(int Questionid)
        {
            return await _appDbContext.Questions_Choices.Select(c => c).
               Where(c => c.QuestionID == Questionid && c.IsRightAnswer).ToListAsync();
        }

        public async Task<bool> IsRightAnswerAsync(int choiceid)
        {
            var choice = await FindAsync(choiceid);
            return choice?.IsRightAnswer ?? false;
        }

    }
}
