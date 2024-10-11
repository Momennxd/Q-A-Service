using Data.DatabaseContext;
using Data.models.Questions;
using Data.Repositories;
using System;
using System.Collections.Generic;
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









    }
}
