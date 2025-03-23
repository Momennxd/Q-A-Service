using Data.DatabaseContext;
using Data.models._SP_;
using Data.models.Collections;
using Data.models.People;
using Data.models.Questions;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Collections_Repo.Collecs_Questions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo
{
    public class QuestionRepo : Repository<Question>, IQuestionRepo
    {



        AppDbContext _appDbContext;

        public QuestionRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<int> DeleteQuestionAsync(int QuestionID)
        {

            var RowsCount = new SqlParameter
            {
                ParameterName = "@RowsCount",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure
            await _appDbContext.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC SP_DeleteQuestion  @QuestionID = {QuestionID}, @RowsCount = {RowsCount} OUTPUT");

            // Get the output value
            return (int)RowsCount.Value;
        }

        public async Task<List<SP_Question>> GetAllQuestionsAsync(int CollectionID)
        {

            // Execute the stored procedure and map results to the SP_Question
            return await _appDbContext.Set<SP_Question>()
                .FromSqlRaw("EXEC SP_GetCollectionQuestions @CollectionID",
                             new SqlParameter("@CollectionID", CollectionID))
                .ToListAsync();


        }

        public async Task<List<Question>> GetAllQuestionsAsync(HashSet<int> QuestionIDs)
        {

            return await _appDbContext.Set<Question>().Select(q => q).
                Where(q => QuestionIDs.Contains(q.QuestionID)).ToListAsync();

        }

        public async Task<Question?> GetQuestionAsync(int QuestionID)
        {
            var res = await _appDbContext.Questions.FirstOrDefaultAsync(q => q.QuestionID == QuestionID);
            return res;
        }

        public async Task<bool> IsUserRightAnswerAccessAsync(int QuestionID, int UserID)
        {
            var hasAccessParam = new SqlParameter
            {
                ParameterName = "@HasAccess",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure
            await _appDbContext.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC SP_HasRightAnswersAccess @QuestionID = {QuestionID}, @UserID = {UserID}, @HasAccess = {hasAccessParam} OUTPUT");

            // Get the output value
            bool hasAccess = (bool)hasAccessParam.Value;

            return hasAccess;
        }

        public async Task<int> PatchQuestionPointsAsync(int QuestionID, int NewPointsVal)
        {

            if (QuestionID <= 0 || NewPointsVal < 0) return -1;

            var repo = new CollectionsQuestionRepo(_appDbContext);

            var CollecQues = await repo.GetCollectionQuestionsAsync(QuestionID);

            if (CollecQues == null)
                throw new ArgumentNullException("Question Does not belong to any Collection");

            CollecQues.QuestionPoints = NewPointsVal;

            return NewPointsVal;

        }
    }
}
