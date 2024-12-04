using AutoMapper;
using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Data.models.Collections;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Microsoft.AspNetCore.JsonPatch;
using Core.Unit_Of_Work;
using Core.DTOs.Questions;
using Data.Repository.Entities_Repositories.Questions_Repo;
using Data.models.Questions;
using Core.Services.Concrete.Questions;
using static Core.DTOs.Collections.CollectionsDTOs;
namespace Core.Services.Concrete.Collections
{
    public class CollectionService : ICollectionService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ICollectionRepo, QCollection> _uowCollections;
        private readonly IQuestionsService _QuestionsService;

        public CollectionService(IMapper mapper,
            IUnitOfWork<ICollectionRepo, QCollection> uowCollections, IQuestionsService QuestionsService)
        {
            _QuestionsService = QuestionsService;
            _uowCollections = uowCollections;
            _mapper = mapper;
        }

        public async Task<int> CreateCollectionAsync
            (CreateQCollectionDTO createQCollectionDTO, int UserID)
        {
            var collEntity = _mapper.Map<QCollection>(createQCollectionDTO);
            collEntity.CreatedByUserId = UserID;

            await _uowCollections.EntityRepo.AddItemAsync(collEntity);
            if (await _uowCollections.CompleteAsync() == 0) return 0;

            //just to speed things up by skipping the proccess of validating an empty array of questions in ef core
            if (createQCollectionDTO.CollecQuestions.Count == 0) return 1;

            int rowscount = (await _QuestionsService.CreateQuestionsAsync(
                createQCollectionDTO.CollecQuestions, collEntity.CollectionId, UserID)).Count;


            return rowscount + 1;
        }


        public async Task<SendCollectionDTO_Full?> GetFullCollectionAsync(int CollecID)
        {
            var eCollection = await _uowCollections.EntityRepo.FindAsync(CollecID);
            if (eCollection == null) return null;

            var sendCollectionFull = _mapper.Map<SendCollectionDTO_Full>(eCollection);

            // Await tasks sequentially to avoid DbContext threading issues :(
            sendCollectionFull.CollecQuestions = await _QuestionsService.GetAllQuestionsAsync(CollecID);
            var Likes_disLikes = await _uowCollections.EntityRepo.GetLikesDislikes(CollecID);

            sendCollectionFull.Likes = Likes_disLikes.Item1;
            sendCollectionFull.DisLikes = Likes_disLikes.Item2;

            return sendCollectionFull;
        }


        public async Task<ICollection<SendCollectionDTO_Thumb>> GetThumbCollectionsAsync
            (int UserID, bool? IsPublic)
        {
            var collections = await _uowCollections.EntityRepo.GetAllByUserIDAsync(UserID, IsPublic);
            var output = new List<SendCollectionDTO_Thumb>();
            foreach(var e in collections)
            {
                var sentDto = _mapper.Map<SendCollectionDTO_Thumb>(e);
                sentDto.Categories =
                    await _uowCollections.EntityRepo.GetCollectionCategories(e.CollectionId);
                output.Add(sentDto);
            }
            return output;
        }

        public async Task<SendCollectionDTO_Full> PatchCollection(
            JsonPatchDocument<PatchQCollectionDTO> patchDoc, int collecID)
        {
            // Await the result of FindAsync to retrieve the actual entity
            var entity = await _uowCollections.EntityRepo.FindAsync(collecID);

            if (entity == null)
            {
                // Handle the case where the collection is not found
                throw new KeyNotFoundException($"Collection with ID {collecID} not found.");
            }

            // Map the entity to a DTO to apply the patch
            var collectionToPatch = _mapper.Map<PatchQCollectionDTO>(entity);

            // Apply the patch to the DTO
            patchDoc.ApplyTo(collectionToPatch);

            // Map the patched DTO back to the original entity
            _mapper.Map(collectionToPatch, entity);

            // Save changes
            await _uowCollections.CompleteAsync();

            // Return the updated collection as a DTO
            return _mapper.Map<CollectionsDTOs.SendCollectionDTO_Full>(entity);
        }

        public async Task<int> DeleteCollectionAsync(int id)
        {
            return await _uowCollections.EntityRepo.DeleteCollectionAsync(id);
        }

        public async Task<ICollection<CollectionsDTOs.SendCollectionDTO_Full>> GetAllCollectionsAsync
            (int UserID, bool IsPublic)
        {
            var collections = await _uowCollections.EntityRepo.GetAllByUserIDAsync(UserID, IsPublic);

            var sentDto = _mapper.Map<ICollection<CollectionsDTOs.SendCollectionDTO_Full>>(collections);

         
            return sentDto;
        }

        public async Task<IEnumerable<CollectionsDTOs.SendCollectionDTO_Full>> GetTop20Collections()
        {

            var collections = await _uowCollections.EntityRepo.GetTop20Collections();
            var sentDto = _mapper.Map<IEnumerable<CollectionsDTOs.SendCollectionDTO_Full>>(collections);

            return sentDto;

        }

        public async Task<bool> LikeAsync(int UserId, int CollectionID, bool IsLike)
        {
            await _uowCollections.EntityRepo.LikeAsync(UserId, CollectionID, IsLike);
            return await _uowCollections.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteLikeAsync(int CollectionID, int UserID)
        {
            await _uowCollections.EntityRepo.DeleteLikeAsync(CollectionID, UserID);
            return await _uowCollections.CompleteAsync() > 0;
        }

        public async Task<List<SendCollectionDTO_Search>> CollectionsSearch(string SearchText, int PageNumber, int PageSize)
        {
            var lst = await _uowCollections.EntityRepo.CollectionsSearch(SearchText, PageNumber, PageSize);

            var output = new List<SendCollectionDTO_Search>(PageSize);

            foreach (var item in lst)
            {
                output.Add(_mapper.Map<SendCollectionDTO_Search>(item)); 
            }

            return output;
        }
    }
}
