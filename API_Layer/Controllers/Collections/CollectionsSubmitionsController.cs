using API_Layer.Extensions;
using Core.Authorization_Services.Interfaces;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Layer.Controllers.Collections
{


    [Route("api/v1/submitions")]
    [ApiController]
    public class CollectionsSubmitionsController : ControllerBase
    {
        ICollectionsSubmitionsService _collectionService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public CollectionsSubmitionsController(ICollectionsSubmitionsService collectionService, ICollectionsAuthService CollectionsAuthService)
        {
            _collectionService = collectionService;
            _collectionsAuthService = CollectionsAuthService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> AddSubmission(int CollectionID)
        {
            int UserId = User.GetUserId();
            if (! await _collectionsAuthService.IsUserCollecOwnerAsync(CollectionID, UserId)) return Unauthorized();
            

            int submissionId = await _collectionService.AddSubmission(CollectionID, UserId);
            return Ok(submissionId);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteSubmition(int SubmitionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();
            if (!await _collectionsAuthService.IsUserSubmitionOwnerAsync(SubmitionID, (int)userId)) return Unauthorized();

            bool isDeleted = await _collectionService.DeleteSubmition(SubmitionID, (int)userId);

            if (isDeleted)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubmission(int SubmissionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            //authorization (checking if the sub is the user's) check will make the api slower and it is not really needed here at the moment

            var submission = await _collectionService.GetBySubmissionID(SubmissionID, (int)userId);
            if (submission == null) return NotFound();

            return Ok(submission);
        }
    }
}
