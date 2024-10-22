﻿using Data.DatabaseContext;
using Data.models.Questions;
using Data.Repositories;

namespace Data.Repository.Entities_Repositories.Collections_Repo.Collecs_Questions
{
    public class AnswerExplanationRepo : Repository<AnswerExplanation>, IAnswerExplanationRepo
    {

        private AppDbContext _appDbContext;
        public AnswerExplanationRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public Task<bool> AddExplaination(AnswerExplanation answerExplanation)
        {
            
        }
    }
}
