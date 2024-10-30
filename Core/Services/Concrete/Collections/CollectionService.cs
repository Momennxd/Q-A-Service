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
            (CollectionsDTOs.CreateQCollectionDTO createQCollectionDTO, int UserID)
        {
            var collEntity = _mapper.Map<QCollection>(createQCollectionDTO);
            collEntity.CreatedByUserId = UserID;

            await _uowCollections.EntityRepo.AddItemAsync(collEntity);
            if (await _uowCollections.CompleteAsync() == 0) return 0;

            //just to speed things up by skipping the proccess of validating an empty array of questions in ef core
            if (createQCollectionDTO.Questions.Count == 0) return 1;

            int rowscount = (await _QuestionsService.CreateQuestionsAsync(
                createQCollectionDTO.Questions, collEntity.CollectionId, UserID)).Count;


            return rowscount + 1;
        }
























        public async Task<CollectionsDTOs.SendCollectionDTO> PatchCollection(
            JsonPatchDocument<CollectionsDTOs.CreateQCollectionDTO> patchDoc, int collecID)
        {
            // Await the result of FindAsync to retrieve the actual entity
            var entity = await _uowCollections.EntityRepo.FindAsync(collecID);

            if (entity == null)
            {
                // Handle the case where the collection is not found
                throw new KeyNotFoundException($"Collection with ID {collecID} not found.");
            }

            // Map the entity to a DTO to apply the patch
            var collectionToPatch = _mapper.Map<CollectionsDTOs.CreateQCollectionDTO>(entity);

            // Apply the patch to the DTO
            patchDoc.ApplyTo(collectionToPatch);

            // Map the patched DTO back to the original entity
            _mapper.Map(collectionToPatch, entity);

            // Save changes
            await _uowCollections.CompleteAsync();

            // Return the updated collection as a DTO
            return _mapper.Map<CollectionsDTOs.SendCollectionDTO>(entity);
        }



        public async Task DeleteCollectionAsync(int id)
        {
            await _uowCollections.EntityRepo.DeleteItemAsync(id);


            // Save changes
            await _uowCollections.CompleteAsync();
        }



        public async Task<ICollection<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync
            (int UserID, bool IsPublic)
        {
            var collections = await _uowCollections.EntityRepo.GetAllByUserIDAsync(UserID, IsPublic);

            var sentDto = _mapper.Map<ICollection<CollectionsDTOs.SendCollectionDTO>>(collections);

          


            return sentDto;
        }

        public async Task<ICollection<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync(int UserID)
        {
            var collections = await _uowCollections.EntityRepo.GetAllByUserIDAsync(UserID);

            var sentDto = _mapper.Map<ICollection<CollectionsDTOs.SendCollectionDTO>>(collections);


            return sentDto;
        }




        public async Task<CollectionsDTOs.SendCollectionDTO?> GetCollectionByIdAsync(int id)
        {
            var eCollection = await _uowCollections.EntityRepo.FindAsync(id);

            if (eCollection == null) return null;

            var dto = _mapper.Map<CollectionsDTOs.SendCollectionDTO>(eCollection);

            return dto;

        }




        public Task UpdateCollectionAsync(CollectionsDTOs.CreateQCollectionDTO updateQCollectionDTO)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<CollectionsDTOs.SendCollectionDTO>> GetTop20Collections()
        {

            var collections = await _uowCollections.EntityRepo.GetTop20Collections();
            var sentDto = _mapper.Map<IEnumerable<CollectionsDTOs.SendCollectionDTO>>(collections);

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
    }
}
