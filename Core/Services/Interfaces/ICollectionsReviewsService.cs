using Core.DTOs.Collections;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Collections.CollectionsReviewsDTOs;

namespace Core.Services.Interfaces
{
    public interface ICollectionsReviewsService
    {
        Task CreateReview(CreateCollectionsReviewDTO collectionsReviewDto, int UserID);



        Task<CreateCollectionsReviewDTO> Patch(JsonPatchDocument<CreateCollectionsReviewDTO> updateCollectionsReviewsDTO
                    , int UserID, int CollectionID);


        public Task DeleteReview(int UserID, int CollectionID);

        Task<List<SendCollectionsReviewsDTO>> GetAllCollectionReviewsAsync(int CollectionID, int Page);
    }
}
