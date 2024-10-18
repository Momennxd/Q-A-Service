using Data.models.Collections;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo.Collects_Questions
{
    public interface ICollectionsQuestionRepo : IRepository<Collections_Questions>
    {


        public Task<Collections_Questions?> GetCollectionQuestionsAsync(int QuestionID);










    }
}
