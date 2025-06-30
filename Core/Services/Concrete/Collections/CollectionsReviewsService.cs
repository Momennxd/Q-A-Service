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




        public async Task CreateReview(CreateCollectionsReviewDTO collectionsReviewDto, int UserID)
        {
            var entity = _mapper.Map<Collections_Reviews>(collectionsReviewDto);
            entity.UserID = UserID;
            entity.ReviewDate = DateTime.Now;
            await _unitOfWork.EntityRepo.AddItemAsync(entity);
            await _unitOfWork.CompleteAsync();

        }
        public async Task<SendCollectionsReviewsDTO> Patch(JsonPatchDocument<CreateCollectionsReviewDTO> patchDoc, int UserID, int ReviewID)
        {
            var entity = await _unitOfWork.EntityRepo.FindAsync(ReviewID);
            if (entity == null) throw new KeyNotFoundException();

            var dtoToPatch = _mapper.Map<CreateCollectionsReviewDTO>(entity);

            // Apply the patch to the DTO
            patchDoc.ApplyTo(dtoToPatch);

            // Map the patched DTO back to the original entity
            _mapper.Map(dtoToPatch, entity);

            // Save changes
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SendCollectionsReviewsDTO>(entity);
        }


        public async Task DeleteReview(int ReviewID)
        {
            await _unitOfWork.EntityRepo.DeleteItemAsync(ReviewID);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<SendCollectionsReviewsDTO>> GetAllCollectionReviewsAsync(int CollectionID, int Page)
        {
            var reviews = await _unitOfWork.EntityRepo.GetAllCollectionReviewsAsync(CollectionID, Page);
            return _mapper.Map<List<SendCollectionsReviewsDTO>>(reviews);
        }
    }
}
