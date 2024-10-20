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

namespace Core.Services.Concrete.Questions
{
    public class QuestionsChoicesService : IQuestionsChoicesService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> _unitOfWork;


        public QuestionsChoicesService
            (IMapper mapper, IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> uowChoices)
        {
            _unitOfWork = uowChoices;
            _mapper = mapper;
        }


        public async Task<List<SendChoiceDTO>> AddChoiceAsync 
            (List<CreateChoiceDTO>  lstcreateChoiceDto)
        {
            List<QuestionsChoices> CreateChoicesEnities = new List<QuestionsChoices>();

            foreach (var choice in lstcreateChoiceDto) {
                CreateChoicesEnities.Add(_mapper.Map<QuestionsChoices>(choice));
            }

            //entity.ChoiceText = createChoiceDto.ChoiceText;

            await _unitOfWork.EntityRepo.AddItemsAsync(CreateChoicesEnities);

            await _unitOfWork.CompleteAsync();


            List<SendChoiceDTO> QSendchoicesDTOs = new List<SendChoiceDTO>();

            foreach (var e in CreateChoicesEnities) {
                QSendchoicesDTOs.Add(_mapper.Map<SendChoiceDTO>(e));
            }

            return QSendchoicesDTOs;



        }

        public async Task<bool> DeleteChoice(int choiceid)
        {
            await _unitOfWork.EntityRepo.DeleteItemAsync(choiceid);

            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<List<SendChoiceDTO>> GetAllRightAnswersAsync(int Questionid)
        {

            var entities = await _unitOfWork.EntityRepo.GetAllRightAnswersAsync(Questionid);

            List<SendChoiceDTO> sendChoiceDTOs = new List<SendChoiceDTO>();

            foreach (var entity in entities) {
                sendChoiceDTOs.Add(_mapper.Map<SendChoiceDTO>(entity));
            }

            return sendChoiceDTOs;
        }

        public async Task<List<SendChoiceDTO>> GetChoicesAsync(int QuestionID)
        {
            
            var entities = await _unitOfWork.EntityRepo.GetAllByQuestionIDAsync(QuestionID);

            List<SendChoiceDTO> sendChoiceDTOs = new List<SendChoiceDTO>();

            foreach (var entity in entities) {
                sendChoiceDTOs.Add(_mapper.Map<SendChoiceDTO>(entity));
            }

            return sendChoiceDTOs;

        }

        public async Task<Dictionary<int, List<SendChoiceDTO>>> GetChoicesAsync(HashSet<int> QuestionsIDs)
        {
            Dictionary<int, List<SendChoiceDTO>> sendChoiceDTOs = new(QuestionsIDs.Count);

            var QuestionsChoicesMap = await _unitOfWork.EntityRepo.GetAllByQuestionIDsAsync(QuestionsIDs);

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
           return await _unitOfWork.EntityRepo.IsRightAnswerAsync(choiceid);
        }





        public async Task<SendChoiceDTO> PatchChoice(JsonPatchDocument<PatchChoiceDTO> patchDoc, int ChoiceID)
        {
            var entity = await _unitOfWork.EntityRepo.FindAsync(ChoiceID);

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
            await _unitOfWork.CompleteAsync();

            // Return the updated collection as a DTO
            return _mapper.Map<SendChoiceDTO>(entity);
        }
    }
}
