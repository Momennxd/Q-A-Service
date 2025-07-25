﻿using Data.DatabaseContext;
using Data.models.Questions;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Pics.Choices_Pics_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.ChosenChoices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Questions_Repo.ChosenChoices
{
    public class ChosenChoicesRepo : Repository<Chosen_Choices>, IChosenChoicesRepo
    {
        private readonly AppDbContext _appDbContext;
        public ChosenChoicesRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

       

        public async Task<int> DeleteChosenChoicesAsync(int ChoiceID)
        {

            var chosenChoices = await _appDbContext.Chosen_Choices
                                      .Where(c => c.ChoiceID == ChoiceID).ToListAsync();

            _appDbContext.Chosen_Choices.RemoveRange(chosenChoices);

            return chosenChoices.Count;

        }

        public async Task<int> DeleteChosenChoicesAsync(HashSet<int> ChoiceIDs)
        {
            var chosenChoices = await _appDbContext.Chosen_Choices
                                     .Where(c => ChoiceIDs.Contains(c.ChoiceID)).ToListAsync();

            _appDbContext.Chosen_Choices.RemoveRange(chosenChoices);

            return chosenChoices.Count;
        }

        public async Task<Dictionary<int, Chosen_Choices>> GetChosenChoicesAsync(HashSet<int> questionIDs, int submitionID, int userID)
        {
            var result = await _appDbContext.Chosen_Choices
                .Where(cc => cc.SubmitionID == submitionID && cc.UserID == userID)
                .Join(_appDbContext.Questions_Choices,
                      cc => cc.ChoiceID,
                      qc => qc.ChoiceID,
                      (cc, qc) => new { ChosenChoice = cc, QuestionID = qc.QuestionID, ChosenDate = cc.ChosenDate })
                .Where(x => questionIDs.Contains(x.QuestionID))
                .GroupBy(x => x.QuestionID)
                .Select(g => g
                    .OrderByDescending(x => x.ChosenDate)
                    .First())
                .ToDictionaryAsync(x => x.QuestionID, x => x.ChosenChoice);

            return result;
        }



    }
}
