using AutoMapper;
using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Data.models.Collections;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Microsoft.AspNetCore.JsonPatch;
using Core.Unit_Of_Work;

namespace Core.Services.Concrete.Collections
{
    public class CollectionService : ICollectionService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ICollectionRepo, QCollection> _unitOfWork;

        public CollectionService(IMapper mapper, IUnitOfWork<ICollectionRepo, QCollection> uowCollections)
        {
            _unitOfWork = uowCollections;
            _mapper = mapper;
        }

        public async Task<int> CreateCollectionAsync
            (CollectionsDTOs.CreateQCollectionDTO createQCollectionDTO, int UserID)
        {
            var collEntity = _mapper.Map<QCollection>(createQCollectionDTO);
            collEntity.CreatedByUserId = UserID;

            await _unitOfWork.EntityRepo.AddItemAsync(collEntity);


            return await _unitOfWork.CompleteAsync();

        }



        public async Task<CollectionsDTOs.SendCollectionDTO> PatchCollection(
            JsonPatchDocument<CollectionsDTOs.CreateQCollectionDTO> patchDoc, int collecID)
        {
            // Await the result of FindAsync to retrieve the actual entity
            var entity = await _unitOfWork.EntityRepo.FindAsync(collecID);

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
            await _unitOfWork.CompleteAsync();

            // Return the updated collection as a DTO
            return _mapper.Map<CollectionsDTOs.SendCollectionDTO>(entity);
        }



        public async Task DeleteCollectionAsync(int id)
        {
            await _unitOfWork.EntityRepo.DeleteItemAsync(id);


            // Save changes
            await _unitOfWork.CompleteAsync();
        }



        public async Task<ICollection<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync
            (int UserID, bool IsPublic)
        {
            var collections = await _unitOfWork.EntityRepo.GetAllByUserIDAsync(UserID, IsPublic);

            var sentDto = _mapper.Map<ICollection<CollectionsDTOs.SendCollectionDTO>>(collections);

            foreach (var dto in sentDto)
            {
                var categories =
                    await _unitOfWork.EntityRepo.GetAllCategoriesAsync(dto.CollectionID);

                foreach (var categ in categories)
                {
                    dto.Categories.Add(categ);
                }

            }


            return sentDto;
        }

        public async Task<ICollection<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync(int UserID)
        {
            var collections = await _unitOfWork.EntityRepo.GetAllByUserIDAsync(UserID);

            var sentDto = _mapper.Map<ICollection<CollectionsDTOs.SendCollectionDTO>>(collections);


            foreach (var dto in sentDto)
            {
                var categories =
                    await _unitOfWork.EntityRepo.GetAllCategoriesAsync(dto.CollectionID);

                foreach (var categ in categories)
                {
                    dto.Categories.Add(categ);
                }

            }


            return sentDto;
        }




        public async Task<CollectionsDTOs.SendCollectionDTO?> GetCollectionByIdAsync(int id)
        {
            var eCollection = await _unitOfWork.EntityRepo.FindAsync(id);

            if (eCollection == null) return null;

            var dto = _mapper.Map<CollectionsDTOs.SendCollectionDTO>(eCollection);

            dto.Categories = await _unitOfWork.EntityRepo.GetAllCategoriesAsync(dto.CollectionID);

            return dto;

        }













        public Task UpdateCollectionAsync(CollectionsDTOs.CreateQCollectionDTO updateQCollectionDTO)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<CollectionsDTOs.SendCollectionDTO>> GetTop20Collections()
        {

            var collections = await _unitOfWork.EntityRepo.GetTop20Collections();
            var sentDto = _mapper.Map<IEnumerable<CollectionsDTOs.SendCollectionDTO>>(collections);



            foreach (var dto in sentDto)
            {
                var categories =
                    await _unitOfWork.EntityRepo.GetAllCategoriesAsync(dto.CollectionID);

                foreach (var categ in categories)
                {
                    dto.Categories.Add(categ);
                }

            }


            return sentDto;

        }

        public async Task<bool> LikeAsync(int UserId, int CollectionID, bool IsLike)
        {
            await _unitOfWork.EntityRepo.LikeAsync(UserId, CollectionID, IsLike);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteLikeAsync(int CollectionID, int UserID)
        {
            await _unitOfWork.EntityRepo.DeleteLikeAsync(CollectionID, UserID);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
