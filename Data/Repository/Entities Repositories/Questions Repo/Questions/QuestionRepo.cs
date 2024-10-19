using Data.DatabaseContext;
using Data.models._SP_;
using Data.models.Questions;
using Data.Repositories;
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
    public class QuestionRepo : Repository<Questions>, IQuestionRepo
    {



        AppDbContext _appDbContext;

        public QuestionRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<bool> IsUserRightAnswerAccess(int QuestionID, int UserID)
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

    }
}
