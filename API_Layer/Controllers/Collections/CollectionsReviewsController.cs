using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Layer.Controllers.Collections
{
    [Route("api/collections/Reviews")]
    [ApiController]
    [Authorize]
    public class CollectionsReviewsController : ControllerBase
    {
        ICollectionsReviewsService _collectionsReviewsService;
        public CollectionsReviewsController(ICollectionsReviewsService collectionsReviewsService)
        {
            _collectionsReviewsService = collectionsReviewsService;
        }


        [HttpPost]
        public async Task<IActionResult> AddReview(CollectionsReviewsDTOs.MainCollectionsReviewDTO collectionsReviewDto)
        {
            await _collectionsReviewsService.CreateReview(collectionsReviewDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(JsonPatchDocument<CollectionsReviewsDTOs.UpdateCollectionsReviewsDTO> patchDoc, int CollectionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();
            
            return Ok(await _collectionsReviewsService.Patch(patchDoc, (int)userId, CollectionID));

        }



        [HttpDelete]
        public async Task<IActionResult> DeleteReview(int CollectionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();
            
            await _collectionsReviewsService.DeleteReview((int)userId, CollectionID);
            return Ok();
        }


        [HttpGet("all")]
        public async Task<IActionResult> getAllCollectionReviews(int CollectionID, int Page)
        {
            if (Page < 1)
                return BadRequest();
            return Ok(await _collectionsReviewsService.GetAllCollectionReviewsAsync(CollectionID, Page));
        }
    }
}
