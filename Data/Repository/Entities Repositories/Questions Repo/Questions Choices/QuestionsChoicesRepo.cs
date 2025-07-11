using Data.DatabaseContext;
using Data.models._SP_;
using Data.models.Questions;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices
{
    public class QuestionsChoicesRepo : Repository<QuestionsChoices>, IQuestionsChoicesRepo
    {

        AppDbContext _appDbContext;

        public QuestionsChoicesRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }


        public async Task<int> DeleteQuestionChoicesAsync(int QuestionID)
        {
            List<QuestionsChoices> questionsChoices = await GetAllByQuestionIDAsync(QuestionID);

            foreach (QuestionsChoices question in questionsChoices)
            {
                await base.DeleteItemAsync(question.ChoiceID);
            }



            return questionsChoices.Count;

        }



        public async Task<List<QuestionsChoices>> GetAllByQuestionIDAsync(int Questionid)
        {
            return await _appDbContext.Questions_Choices.Select(c => c).
                 Where(c => c.QuestionID == Questionid).OrderBy(c => c.Rank).ToListAsync();
        }

        public async Task<Dictionary<int, List<QuestionsChoices>>> GetAllByQuestionIDsAsync(HashSet<int> QuestionsIDs)
        {
            Dictionary<int, List<QuestionsChoices>> QuestionsMap = new(QuestionsIDs.Count);
            if (QuestionsIDs == null || QuestionsIDs.Count == 0) return QuestionsMap;

            var choices = await _appDbContext.Questions_Choices
                .Where(c => QuestionsIDs.Contains(c.QuestionID))
                .ToListAsync();



            QuestionsMap = choices
                .GroupBy(c => c.QuestionID)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(c => c.Rank).ToList() // Sorting by Rank in ascending order
                );


            return QuestionsMap;
        }


        public async Task<List<QuestionsChoices>> GetAllRightAnswersAsync(int Questionid)
        {
            return await _appDbContext.Questions_Choices.Select(c => c).
               Where(c => c.QuestionID == Questionid && c.IsRightAnswer).ToListAsync();
        }


        public async Task<List<QuestionsChoices>> GetCollectionChoices(int CollectionID)
        {
            // Execute the stored procedure and map results to the QuestionsChoices
            return await _appDbContext.Set<QuestionsChoices>()
                .FromSqlRaw("EXEC SP_GetCollectionChoices @CollectionID",
                             new SqlParameter("@CollectionID", CollectionID))
                .ToListAsync();
        }

        public async Task<bool> IsRightAnswerAsync(int choiceid)
        {
            var choice = await FindAsync(choiceid);
            return choice?.IsRightAnswer ?? false;
        }



    }
}
