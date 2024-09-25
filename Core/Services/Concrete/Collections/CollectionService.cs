using AutoMapper;
using Core.DTOs.Collections;
using Core.DTOs.People;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Core_Layer.AppDbContext;
using Core_Layer.models.Collections;
using Core_Layer.models.People;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete.Collections
{
    public class CollectionService : ICollectionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CollectionService(ILogger<IUnitOfWork> logger, AppDbContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(logger, context);
            _mapper = mapper;
        }

        public async Task<int> CreateCollectionAsync
            (CollectionsDTOs.CreateQCollectionDTO createQCollectionDTO, int UserID)
        {
            var collEntity = _mapper.Map<QCollection>(createQCollectionDTO);
            collEntity.CreatedByUserId = UserID;

            await _unitOfWork.Collections.AddItemAsync(collEntity);


            return await _unitOfWork.CompleteAsync();
          
        }



        public async Task<CollectionsDTOs.SendCollectionDTO> PatchCollection(
            JsonPatchDocument<CollectionsDTOs.CreateQCollectionDTO> patchDoc, int collecID)
        {
            // Await the result of FindAsync to retrieve the actual entity
            var entity = await _unitOfWork.Collections.FindAsync(collecID);

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
            await _unitOfWork.Collections.DeleteItemAsync(id);


            // Save changes
            await _unitOfWork.CompleteAsync();
        }












        public async Task<IEnumerable<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync
            (int UserID, bool IsPublic)
        {
            var collections = await _unitOfWork.Collections.GetAllByUserIDAsync(UserID, IsPublic);

            return _mapper.Map<IEnumerable<CollectionsDTOs.SendCollectionDTO>>(collections);
        }

        public async Task<IEnumerable<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync(int UserID)
        {
            var collections = await _unitOfWork.Collections.GetAllByUserIDAsync(UserID);

            return _mapper.Map<IEnumerable<CollectionsDTOs.SendCollectionDTO>>(collections);
        }


        public Task<CollectionsDTOs.SendCollectionDTO> GetCollectionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCollectionAsync(CollectionsDTOs.CreateQCollectionDTO updateQCollectionDTO)
        {
            throw new NotImplementedException();
        }



    }
}
