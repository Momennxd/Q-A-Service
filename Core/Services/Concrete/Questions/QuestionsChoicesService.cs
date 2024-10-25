using AutoMapper;
using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging.Abstractions;
using Core.DTOs.Pictures;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;
using Microsoft.AspNetCore.JsonPatch;
using Core.DTOs.Collections;
using Data.Repository.Entities_Repositories.Questions_Repo.ChosenChoices;

namespace Core.Services.Concrete.Questions
{
    public class QuestionsChoicesService : IQuestionsChoicesService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> _uowChoices;
        private readonly IUnitOfWork<IChosenChoicesRepo, Chosen_Choices> _uowChosenChoices;


        public QuestionsChoicesService
            (IMapper mapper, IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> uowChoices,
            IUnitOfWork<IChosenChoicesRepo, Chosen_Choices> uowChosenChoices)
        {
            _uowChosenChoices = uowChosenChoices;
            _uowChoices = uowChoices;
            _mapper = mapper;
        }


        public async Task<List<SendChoiceDTO>> AddChoiceAsync 
            (List<CreateChoiceDTO>  lstcreateChoiceDto, int QuestionID)
        {
            List<QuestionsChoices> CreateChoicesEnities = new List<QuestionsChoices>();

            foreach (var choice in lstcreateChoiceDto) {

                var e = _mapper.Map<QuestionsChoices>(choice);
                e.QuestionID = QuestionID;
                CreateChoicesEnities.Add(e);

            }

            await _uowChoices.EntityRepo.AddItemsAsync(CreateChoicesEnities);

            await _uowChoices.CompleteAsync();


            List<SendChoiceDTO> QSendchoicesDTOs = new List<SendChoiceDTO>();

            foreach (var e in CreateChoicesEnities) {
                QSendchoicesDTOs.Add(_mapper.Map<SendChoiceDTO>(e));
            }

            return QSendchoicesDTOs;



        }

        public async Task<int> DeleteChoiceAsync(int choiceid)
        {
            int rowsEffected = 0;

            await _uowChosenChoices.EntityRepo.DeleteChosenChoicesAsync(choiceid);
            rowsEffected += await _uowChosenChoices.CompleteAsync();

            await _uowChoices.EntityRepo.DeleteItemAsync(choiceid);
            rowsEffected += await _uowChoices.CompleteAsync();

            return rowsEffected;
        }
        


        public async Task<int> DeleteQuestionChoicesAsync(int QuestionID)
        {

            var choices = await _uowChoices.EntityRepo.GetAllByQuestionIDAsync(QuestionID);
            HashSet<int> choicesIDs = new(choices.Count);

            foreach (var choice in choices) choicesIDs.Add(choice.ChoiceID);


            int rowsEffected = 0;

            //delete chosen_choices
            await _uowChosenChoices.EntityRepo.DeleteChosenChoicesAsync(choicesIDs);
            rowsEffected += await _uowChosenChoices.CompleteAsync();

            //delete choices
            await _uowChoices.EntityRepo.DeleteQuestionChoicesAsync(QuestionID);
            rowsEffected += await _uowChoices.CompleteAsync();

            return rowsEffected;

        }



        public async Task<List<SendChoiceDTO>> GetAllRightAnswersAsync(int Questionid)
        {

            var entities = await _uowChoices.EntityRepo.GetAllRightAnswersAsync(Questionid);

            List<SendChoiceDTO> sendChoiceDTOs = new List<SendChoiceDTO>();

            foreach (var entity in entities) {
                sendChoiceDTOs.Add(_mapper.Map<SendChoiceDTO>(entity));
            }

            return sendChoiceDTOs;
        }

        public async Task<List<SendChoiceDTO>> GetChoicesAsync(int QuestionID)
        {
            
            var entities = await _uowChoices.EntityRepo.GetAllByQuestionIDAsync(QuestionID);

            List<SendChoiceDTO> sendChoiceDTOs = new List<SendChoiceDTO>();

            foreach (var entity in entities) {
                sendChoiceDTOs.Add(_mapper.Map<SendChoiceDTO>(entity));
            }

            return sendChoiceDTOs;

        }

        public async Task<Dictionary<int, List<SendChoiceDTO>>> GetChoicesAsync(HashSet<int> QuestionsIDs)
        {
            Dictionary<int, List<SendChoiceDTO>> sendChoiceDTOs = new(QuestionsIDs.Count);

            var QuestionsChoicesMap = await _uowChoices.EntityRepo.GetAllByQuestionIDsAsync(QuestionsIDs);

            foreach (var QuestionID in QuestionsChoicesMap.Keys) {

                sendChoiceDTOs.Add(QuestionID, new(QuestionsChoicesMap[QuestionID].Count));
                foreach (var choiceEntity in QuestionsChoicesMap[QuestionID]) 
                {
                    sendChoiceDTOs[QuestionID].Add(_mapper.Map<SendChoiceDTO>(choiceEntity));
                }

            }

            return sendChoiceDTOs;
        }

        public async Task<bool> IsRightAnswerAsync(int choiceid)
        {
           return await _uowChoices.EntityRepo.IsRightAnswerAsync(choiceid);
        }


        public async Task<SendChoiceDTO> PatchChoiceAsync(JsonPatchDocument<PatchChoiceDTO> patchDoc, int ChoiceID)
        {
            var entity = await _uowChoices.EntityRepo.FindAsync(ChoiceID);

            if (entity == null)
            {
                // Handle the case where the collection is not found
                throw new KeyNotFoundException($"Choice with ID {ChoiceID} not found.");
            }

            // Map the entity to a DTO to apply the patch
            var ChoiceToPatch = _mapper.Map<PatchChoiceDTO>(entity);

            // Apply the patch to the DTO
            //here an exception might be thrown if the user tried to patch a property that is not 
            //included within the patch doc itself
            patchDoc.ApplyTo(ChoiceToPatch);

            // Map the patched DTO back to the original entity
            _mapper.Map(ChoiceToPatch, entity);

            // Save changes
            await _uowChoices.CompleteAsync();

            // Return the updated collection as a DTO
            return _mapper.Map<SendChoiceDTO>(entity);
        }
    }
}
