using AutoMapper;
using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.DatabaseContext;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.ChosenChoices;
using Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.chosen_choicesDTOs;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace Core.Services.Concrete.Questions
{
    public class ChosenChoicesService : IChosenChoicesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IChosenChoicesRepo, Chosen_Choices> _uowChosenChoices;

        public ChosenChoicesService
            (IMapper mapper, IUnitOfWork<IChosenChoicesRepo, Chosen_Choices> uowChosenChoices)
        {
            _uowChosenChoices = uowChosenChoices;
            _mapper = mapper;
        }


        public async Task<Dictionary<int, chosen_choicesDTOs.send_chosen_choicesDTO>> GetChosenChoices(HashSet<int> QuestionIDs, int submitionID, int userID)
        {
            var res = await _uowChosenChoices.EntityRepo.GetChosenChoicesAsync(QuestionIDs, submitionID, userID);

            Dictionary<int, chosen_choicesDTOs.send_chosen_choicesDTO> ans = res.ToDictionary(
                pair => pair.Key,
                pair => new chosen_choicesDTOs.send_chosen_choicesDTO
                {
                    Chosen_ChoiceID = pair.Value.Chosen_ChoiceID,
                    ChoiceID = pair.Value.ChoiceID,
                    UserID = pair.Value.UserID,
                    ChosenDate = pair.Value.ChosenDate,
                    SubmitionID = pair.Value.SubmitionID
                });

            return ans;
        }

        public async Task<send_chosen_choicesDTO> AddChosenChoices(Add_chosen_choicesDTO add_Chosen_ChoicesDTO, int userID)
        {
            var e = _mapper.Map<Chosen_Choices>(add_Chosen_ChoicesDTO);

            e.UserID = userID;
            e.ChosenDate = DateTime.UtcNow;

            await _uowChosenChoices.EntityRepo.AddItemAsync(e);
            await _uowChosenChoices.CompleteAsync();
            return _mapper.Map<send_chosen_choicesDTO>(e);
        }
    }


}
