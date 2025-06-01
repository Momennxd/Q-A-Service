using API_Layer.Security;
using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddSubmission(int CollectionID)
        {

            int UserId = clsToken.GetUserID(HttpContext);
            int submissionId = await _collectionService.AddSubmission(CollectionID, UserId);
            return Ok(submissionId);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteSubmition(int SubmitionID)
        {
            int UserId = clsToken.GetUserID(HttpContext);
            bool isDeleted = await _collectionService.DeleteSubmition(SubmitionID, UserId);
            if (isDeleted)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubmission(int SubmissionID)
        {
            int UserID = 1; //clsToken.GetUserID(HttpContext);
            var submission = await _collectionService.GetBySubmissionID(SubmissionID, UserID);
            if (submission == null) return NotFound();

            return Ok(submission);
        }
    }
}
