using Data.DatabaseContext;
using Data.models.Collections;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Collections_Repo.Collects_Questions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo.Collecs_Questions
{
    public class CollectionsQuestionRepo : Repository<Collections_Questions>, ICollectionsQuestionRepo
    {

        private AppDbContext _appDbContext;
        public CollectionsQuestionRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<int> DeleteCollectionsQuestionsAsync(int QuestionID)
        {
            Collections_Questions? collections_Questions =
                 await GetCollectionQuestionsAsync(QuestionID);

            if (collections_Questions == null) return 0;

            await base.DeleteItemAsync(collections_Questions.Collection_QuestionID);

            return 1;
        }

        public async Task<Collections_Questions?> GetCollectionQuestionsAsync(int QuestionID)
        {
            return await _appDbContext.Collections_Questions
                .FirstOrDefaultAsync(x => x.QuestionID == QuestionID);
        }

    }
}
