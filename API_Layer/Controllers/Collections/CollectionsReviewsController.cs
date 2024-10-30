using API_Layer.Security;
using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Collections
{
    [Route("api/collectionsReviews")]
    [ApiController]
    public class CollectionsReviewsController : ControllerBase
    {
        ICollectionsReviewsService _collectionsReviewsService;
        public CollectionsReviewsController(ICollectionsReviewsService collectionsReviewsService)
        {
            _collectionsReviewsService = collectionsReviewsService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(CollectionsReviewsDTOs.MainCollectionsReviewDTO collectionsReviewDto)
        {
            await _collectionsReviewsService.CreateReview(collectionsReviewDto);
            return Ok();
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Patch(JsonPatchDocument<CollectionsReviewsDTOs.UpdateCollectionsReviewsDTO> patchDoc, int CollectionID)
        {
            int UserID = clsToken.GetUserID(HttpContext);
            return Ok(await _collectionsReviewsService.Patch(patchDoc, UserID, CollectionID));

        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int CollectionID)
        {
            int UserID = clsToken.GetUserID(HttpContext);
            await _collectionsReviewsService.DeleteReview(UserID , CollectionID);
            return Ok();
        }


        [HttpGet("{CollectionID}")]
        public async Task<IActionResult> getAllCollectionReviews(int CollectionID)
        {
            return Ok(await _collectionsReviewsService.GetAllCollectionReviewsAsync(CollectionID));
        }
    }
}
