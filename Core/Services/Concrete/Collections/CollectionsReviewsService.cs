using AutoMapper;
using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.Repository.Entities_Repositories.Collections_Repo.CollectionsReviews;
using Microsoft.AspNetCore.JsonPatch;
using static Core.DTOs.Collections.CollectionsReviewsDTOs;
using static Core.DTOs.People.UsersDTOs;
namespace Core.Services.Concrete.Collections
{
    public class CollectionsReviewsService : ICollectionsReviewsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ICollectionsReviewsRepo, Collections_Reviews> _unitOfWork;
        public CollectionsReviewsService(IMapper mapper, IUnitOfWork<ICollectionsReviewsRepo, Collections_Reviews> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }




        public async Task CreateReview(CollectionsReviewsDTOs.MainCollectionsReviewDTO collectionsReviewDto)
        {
            var entity = _mapper.Map<Collections_Reviews>(collectionsReviewDto);
            await _unitOfWork.EntityRepo.AddItemAsync(entity);
            await _unitOfWork.CompleteAsync();

        }
        public async Task<CollectionsReviewsDTOs.MainCollectionsReviewDTO> Patch(JsonPatchDocument<CollectionsReviewsDTOs.UpdateCollectionsReviewsDTO> patchDoc, int UserID, int CollectionID)
        {
            var entity = await _unitOfWork.EntityRepo.FindByUserIdAndCollectionID(UserID, CollectionID);
            
            var dtoToPatch = _mapper.Map<CollectionsReviewsDTOs.UpdateCollectionsReviewsDTO>(entity);

            // Apply the patch to the DTO
            patchDoc.ApplyTo(dtoToPatch);

            // Map the patched DTO back to the original entity
            _mapper.Map(dtoToPatch, entity);

            // Save changes
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CollectionsReviewsDTOs.MainCollectionsReviewDTO>(entity);
        }
 

        public async Task DeleteReview(int UserID, int CollectionID)
        {
            var entity = await _unitOfWork.EntityRepo.FindByUserIdAndCollectionID(UserID, CollectionID);
            if(entity == null)
                throw new KeyNotFoundException($"Entity not found.");

            await _unitOfWork.EntityRepo.DeleteItemAsync(entity.CollectionReviewID);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<MainCollectionsReviewDTO>> GetAllCollectionReviewsAsync(int CollectionID)
        {
            var reviews = await _unitOfWork.EntityRepo.GetAllCollectionReviewsAsync(CollectionID);
            return _mapper.Map<List<MainCollectionsReviewDTO>>(reviews);
        }
    }
}
