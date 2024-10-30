using Core.DTOs.Collections;
using Data.models.Collections;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Collections.CollectionsDTOs;

namespace Core.Services.Interfaces
{
    public interface ICollectionService
    {



        Task<int> CreateCollectionAsync(CreateQCollectionDTO createQCollectionDTO, int UserID);












        Task<CollectionsDTOs.SendCollectionDTO?> GetCollectionByIdAsync(int id);
        Task<ICollection<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync(int UserID, bool IsPublic);
        public Task<ICollection<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync(int UserID);   
        Task UpdateCollectionAsync(CollectionsDTOs.CreateQCollectionDTO updateQCollectionDTO);
    
        Task DeleteCollectionAsync(int id);

        public Task<CollectionsDTOs.SendCollectionDTO> PatchCollection
            (JsonPatchDocument<CollectionsDTOs.CreateQCollectionDTO> patchDoc, int CollecID);
        public Task<IEnumerable<SendCollectionDTO>> GetTop20Collections();

        Task<bool> LikeAsync(int UserId, int CollectionID, bool IsLike);
        Task<bool> DeleteLikeAsync(int CollectionID, int UserID);


    }
}
