﻿using Data.models.Questions;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices
{
    public interface IQuestionsChoicesRepo : IRepository<QuestionsChoices>
    {

        public Task<List<QuestionsChoices>> GetAllByQuestionIDAsync(int Questionid);

        public Task<List<QuestionsChoices>> GetAllRightAnswersAsync(int Questionid);

        public Task<bool> IsRightAnswerAsync(int choiceid);


    }
}
