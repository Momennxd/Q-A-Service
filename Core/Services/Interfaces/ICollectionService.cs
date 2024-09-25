using Core.DTOs.Collections;
using Core_Layer.models.Collections;
using Core_Layer.models.People;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICollectionService
    {

        public JsonPatchDocument<QCollection> ConvertPatchDocToDocEntity(
           JsonPatchDocument<CollectionsDTOs.CreateQCollectionDTO> DtoPatchDoc);


        Task<CollectionsDTOs.SendCollectionDTO> GetCollectionByIdAsync(int id);


        Task<IEnumerable<CollectionsDTOs.SendCollectionDTO>> GetAllCollectionsAsync();


        Task<int> CreateCollectionAsync(CollectionsDTOs.CreateQCollectionDTO createQCollectionDTO, int UserID);


        Task UpdateCollectionAsync(CollectionsDTOs.CreateQCollectionDTO updateQCollectionDTO);

        
        Task DeleteCollectionAsync(int id);

        public Task<CollectionsDTOs.SendCollectionDTO> PatchCollection
            (JsonPatchDocument<CollectionsDTOs.CreateQCollectionDTO> patchDoc, int CollecID);
       



    }
}
