using AutoMapper;
using Core.DTOs.Collections;
using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models._SP_;
using Data.models.Collections;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Collections_Repo.Collects_Questions;
using Data.Repository.Entities_Repositories.Questions_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Questions.QuestionsDTOs;

namespace Core.Services.Concrete.Questions
{
    public class QuestionsService : IQuestionsService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IQuestionRepo, Question> _uowQuestions;
        private readonly IUnitOfWork<ICollectionsQuestionRepo, Collections_Questions> _uowCollecQues;
        private readonly IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> _uowQuesChoices;

        private readonly IQuestionsChoicesService _QuestionsChoicesService;


        public QuestionsService(IMapper mapper, IUnitOfWork<IQuestionRepo, Question> uowQuestions,
            IUnitOfWork<ICollectionsQuestionRepo, Collections_Questions> uowCollecQues,
            IQuestionsChoicesService questionsChoicesService,
            IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices> uowQuesChoices)
        {
            _uowCollecQues = uowCollecQues;
            _uowQuestions = uowQuestions;
            _mapper = mapper;
            _QuestionsChoicesService = questionsChoicesService;
            _uowQuesChoices = uowQuesChoices;
        }


        public async Task<List<SendQuestionDTO>> CreateQuestionsAsync(
            List<CreateQuestionDTO> createQuestionsDTO, int CollectionID, int UserID)
        {
            List<SendQuestionDTO> output = new(createQuestionsDTO.Count);

            foreach (var createDto in createQuestionsDTO)
            {

                var QuesEnitity = _mapper.Map<Question>(createDto);
                QuesEnitity.UserID = UserID;

                await _uowQuestions.EntityRepo.AddItemAsync(QuesEnitity);
                //saving the question itself
                await _uowQuestions.CompleteAsync();




                //var sendChoicesDtos = await _QuestionsChoicesService.AddChoiceAsync
                //    (createDto.Choices, QuesEnitity.QuestionID);

                //var sendQuesDto = _mapper.Map<SendQuestionDTO>(QuesEnitity);

                ////mapping the other properties
                //sendQuesDto.Choices = sendChoicesDtos;
                //sendQuesDto.QuestionPoints = createDto.QuestionPoints;


             

                ////mapping the question to a collection by adding the question info to Collections_Questions table
                //await _uowCollecQues.EntityRepo.AddItemAsync(new Collections_Questions()
                //{
                //    CollectionID = CollectionID,
                //    QuestionID = QuesEnitity.QuestionID,
                //    QuestionPoints = createDto.QuestionPoints,
                //    AddedTime = DateTime.Now
                // });


                ////saving the Collections_Questions row that maps the question to the collection
                //await _uowCollecQues.CompleteAsync();



                //output.Add(sendQuesDto);

            }


            return output;
        }

        public async Task<int> DeleteQuestionAsync(int QuestionID)
        {
            return  await _uowQuestions.EntityRepo.DeleteQuestionAsync(QuestionID);
        }


        public async Task<List<SendQuestionDTO>> GetAllQuestionsAsync(int CollectionID)
        {
            List<SP_Question> CollectQuestions =
                await _uowQuestions.EntityRepo.GetAllQuestionsAsync(CollectionID);

            List<QuestionsChoices> CollectChoices =
                await _uowQuesChoices.EntityRepo.GetCollectionChoices(CollectionID);

            Dictionary<int, SendQuestionDTO> sendQuesDtoMap = new(CollectQuestions.Count);

            foreach (var question in CollectQuestions)
            {
                if (!sendQuesDtoMap.ContainsKey(question.QuestionID))
                {
                    sendQuesDtoMap.Add(question.QuestionID, _mapper.Map<SendQuestionDTO>(question));
                    sendQuesDtoMap[question.QuestionID].Choices = new();
                }      
            }


            foreach(var choice in CollectChoices)
            {
                if (sendQuesDtoMap.ContainsKey(choice.QuestionID))
                {
                    sendQuesDtoMap[choice.QuestionID].Choices.Add
                        (_mapper.Map<QuestionsChoicesDTOs.SendChoiceDTO>(choice));
                }
            }

            return [.. sendQuesDtoMap.Values];

        }

        public async Task<SendQuestionDTO> GetQuestionAsync(int QuestionID)
        {
            var Question = await _uowQuestions.EntityRepo.GetQuestionAsync(QuestionID);
            var result = _mapper.Map<SendQuestionDTO>(Question);
            return result;

        }

        public async Task<List<QuestionWithChoicesDto>> GetRandomQuestionsWithChoicesAsync(int CollectionID)
        {
            var result = await _uowQuestions.EntityRepo.GetRandomQuestionsWithChoicesAsync(CollectionID);
            var output = _mapper.Map<List<QuestionWithChoicesDto>>(result);
            return output;
        }

        public async Task<SendQuestionDTO> PatchQuestionAsync(JsonPatchDocument<PatchQuestionDTO> patchDoc, int QuestionID)
        {

            // Await the result of FindAsync to retrieve the actual entity
            var entity = await _uowQuestions.EntityRepo.FindAsync(QuestionID);

            if (entity == null)
            {
                // Handle the case where the collection is not found
                throw new KeyNotFoundException($"Question with ID {QuestionID} not found.");
            }

            // Map the entity to a DTO to apply the patch
            var QuestionToPatch = _mapper.Map<PatchQuestionDTO>(entity);

            // Apply the patch to the DTO
            patchDoc.ApplyTo(QuestionToPatch);

            // Map the patched DTO back to the original entity
            _mapper.Map(QuestionToPatch, entity);

            // Save changes
            await _uowQuestions.CompleteAsync();

            // Return the updated collection as a DTO
            return _mapper.Map<SendQuestionDTO>(entity);


        }

        public async Task<int> PatchQuestionPointsAsync(int QuestionID, int NewPointsVal)
        {
            
            if (await _uowQuestions.EntityRepo.PatchQuestionPointsAsync(QuestionID, NewPointsVal) == -1)
                return -1;

            if (await _uowQuestions.CompleteAsync() < 1) return -1;

            return NewPointsVal;
        }
    }
}
