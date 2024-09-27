using AutoMapper;
using Core.DTOs.Collections;
using Core.DTOs.People;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete.Collections
{
    public class CollectionService : ICollectionService
    {

        private readonly IUnitOfWork<ICollectionRepo, QCollection> _unitOfWork;



        private readonly IMapper _mapper;



        //temp
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

            foreach(var dto in sentDto)
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
