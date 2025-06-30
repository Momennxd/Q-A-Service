using API_Layer.Extensions;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.DTOs.Collections.CollectionsReviewsDTOs;

namespace API_Layer.Controllers.Collections
{
    [Route("api/v1/collections/reviews")]
    [ApiController]
    [Authorize]
    public class CollectionsReviewsController : ControllerBase
    {
        ICollectionsReviewsService _collectionsReviewsService;
        ICollectionsAuthService _CollectionsAuthService;

        public CollectionsReviewsController(ICollectionsReviewsService collectionsReviewsService, ICollectionsAuthService CollectionsAuthService)
        {
            _collectionsReviewsService = collectionsReviewsService;
            _CollectionsAuthService = CollectionsAuthService;
        }


        [HttpPost]
        //Tested by Momen on June 28, 2025
        public async Task<IActionResult> AddReview(CreateCollectionsReviewDTO collectionsReviewDto)
        {
            int? userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            await _collectionsReviewsService.CreateReview(collectionsReviewDto, (int)userId);
            return Ok();
        }

        [HttpPatch]
        //Tested by Momen on June 29, 2025
        public async Task<ActionResult<SendCollectionsReviewsDTO>> Patch(JsonPatchDocument<CreateCollectionsReviewDTO> patchDoc, int ReviewId)
        {
            int? userId = User.GetUserId();
            if (userId == null || !(await _CollectionsAuthService.IsUserReviewOwnerAsync(ReviewId, (int)userId))) return Unauthorized();
            return Ok(await _collectionsReviewsService.Patch(patchDoc, (int)userId, ReviewId));

        }



        [HttpDelete]
        //Tested by Momen on June 29, 2025
        public async Task<IActionResult> DeleteReview(int ReviewID)
        {
            int? userId = User.GetUserId();
            if (userId == null || !(await _CollectionsAuthService.IsUserReviewOwnerAsync(ReviewID, (int)userId))) return Unauthorized();
            
            await _collectionsReviewsService.DeleteReview(ReviewID);
            return Ok();
        }


        [HttpGet("{CollectionID}")]
        [AllowAnonymous]
        //Tested by Momen on June 29, 2025
        public async Task<ActionResult<List<SendCollectionsReviewsDTO>>> getAllCollectionReviews(int CollectionID, int Page)
        {
            if (Page < 1) return BadRequest();
            return Ok(await _collectionsReviewsService.GetAllCollectionReviewsAsync(CollectionID, Page));
        }
    }
}
