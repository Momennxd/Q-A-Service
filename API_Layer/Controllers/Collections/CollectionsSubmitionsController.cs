using API_Layer.Extensions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Layer.Controllers.Collections
{
    [Route("api/collectionsSubmitions")]
    [ApiController]
    public class CollectionsSubmitionsController : ControllerBase
    {
        ICollectionsSubmitionsService _collectionService;

        public CollectionsSubmitionsController(ICollectionsSubmitionsService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> AddSubmission(int CollectionID)
        {

            int UserId = User.GetUserId();
            int submissionId = await _collectionService.AddSubmission(CollectionID, UserId);
            return Ok(submissionId);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteSubmition(int SubmitionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

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

            var submission = await _collectionService.GetBySubmissionID(SubmissionID, (int)userId);
            if (submission == null) return NotFound();

            return Ok(submission);
        }
    }
}
