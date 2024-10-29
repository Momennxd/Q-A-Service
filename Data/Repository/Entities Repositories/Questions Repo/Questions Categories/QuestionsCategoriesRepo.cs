using Data.DatabaseContext;
using Data.models._SP_;
using Data.models.nsCategories;
using Data.models.Questions;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo.nsQuestions_Categories
{
    public class QuestionsCategoriesRepo : Repository<Questions_Categories>, IQuestionsCategoriesRepo
    {

        private readonly AppDbContext _appDbContext;
        public QuestionsCategoriesRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<int> DeleteQuestionCategoriesAsync(int QuestionID)
        {
            var lst = await _appDbContext.Questions_Categories.Select(c => c).
                Where(c => c.QuestionID == QuestionID).ToListAsync();


            foreach(var item in lst) { await base.DeleteItemAsync(item.Question_CategoryID); }

            return lst.Count;
        }

      

        public async Task<List<SP_QuestionCategories>> GetQuestionCategoriesAsync(int QuestionID)
        {
            var QustID = new SqlParameter("@QuestionID", QuestionID);

            var categories = await _appDbContext.SP_QuestionCategories
                .FromSqlRaw("EXEC SP_GetQuestionsCategories @QuestionID", QustID)
                .ToListAsync();

            return categories;
        }
    }
}
