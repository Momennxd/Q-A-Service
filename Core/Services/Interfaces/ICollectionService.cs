using Core.DTOs.Collections;
using Microsoft.AspNetCore.JsonPatch;
using static Core.DTOs.Collections.CollectionsDTOs;

namespace Core.Services.Interfaces
{
    public interface ICollectionService
    {



        Task<int> CreateCollectionAsync(CreateQCollectionDTO createQCollectionDTO, int UserID);


        Task<SendCollectionDTO_Full?> GetFullCollectionAsync(int id);

        public Task<ICollection<SendCollectionDTO_Thumb>> GetThumbCollectionsAsync
            (int UserID, bool? IsPublic);

        public Task<CollectionsDTOs.SendCollectionDTO_Full> PatchCollection
           (JsonPatchDocument<CollectionsDTOs.PatchQCollectionDTO> patchDoc, int CollecID);



        public Task<int> DeleteCollectionAsync(int id);















        Task<ICollection<CollectionsDTOs.SendCollectionDTO_Full>> GetAllCollectionsAsync(int UserID, bool IsPublic);
    

       
        public Task<IEnumerable<SendCollectionDTO_Full>> GetTop20Collections();

        Task<bool> LikeAsync(int UserId, int CollectionID, bool IsLike);
        Task<bool> DeleteLikeAsync(int CollectionID, int UserID);


    }
}
