using Core.Authorization_Services.Interfaces;
using Core.DTOs.Questions;
using Core.Services.Concrete.Questions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;
using System.Security.Claims;
using static Core.DTOs.Questions.QuestionsDTOs;
using Microsoft.AspNetCore.JsonPatch;
using System.Diagnostics;

namespace API_Layer.Controllers.Questions
{

    [Route("api/questions")]
    [ApiController]
    [Authorize]
    public class QuestionsController : Controller
    {

        private readonly IQuestionsService _QuestionsService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public QuestionsController(IQuestionsService questionsService,
          ICollectionsAuthService collectionsAuthService)
        {
            _collectionsAuthService = collectionsAuthService;
            _QuestionsService = questionsService;
        }


        //[HttpPost]
        //public async Task<IActionResult> CreateQuestions(
        //    [FromBody] List<CreateQuestionDTO> createDtos, int CollectionID)
        //{
        //    //int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //    //if (userId == null) return Unauthorized();

        //    //if (!await _collectionsAuthService.IsUserCollecOwnerAsync(CollectionID, (int)userId))
        //    //    return Unauthorized();


        //    return Ok(await _QuestionsService.CreateQuestionsAsync(createDtos, CollectionID, 1/* (int)userId*/));
        //}


        //[HttpGet]
        //public async Task<IActionResult> GetCollectionQuestions(int CollectionID)
        //{
        //    int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //    if (userId == null) return Unauthorized();

        //    //validate if the collection is not the users and its private then reutrn unauth
        //    if (!await _collectionsAuthService.IsUserCollectionAccess(CollectionID, (int)userId))
        //        return Unauthorized();

        //    return Ok(await _QuestionsService.GetAllQuestionsAsync(CollectionID));
        //}


        //[HttpPatch("{QuestionID}")]
        //public async Task<IActionResult> PatchQuestion
        //   ([FromBody] JsonPatchDocument<PatchQuestionDTO> patchDoc, int QuestionID)
        //{

        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


        //    if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
        //        return Unauthorized();


        //    return Ok(await _QuestionsService.PatchQuestionAsync(patchDoc, QuestionID));
        //}



        //[HttpPatch("points/{QuestionID}")]
        //public async Task<IActionResult> PatchQuestionPoints(int QuestionID, int NewPointsVal)
        //{

        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


        //    if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
        //        return Unauthorized();


        //    return Ok(await _QuestionsService.PatchQuestionPointsAsync(QuestionID, NewPointsVal));
        //}



        //[HttpDelete]
        //public async Task<IActionResult> DeleteQuestion(int QuestionID)
        //{
        //    int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        //    if (userId == null) return Unauthorized();

        //    if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, (int)userId))
        //        return Unauthorized();



        //    return Ok(await _QuestionsService.DeleteQuestionAsync(QuestionID));

        //}


        //[HttpGet("{QuestionID}")]
        //public async Task<IActionResult> GetQuestion(int QuestionID)
        //{
        //    int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //    if (userId == null) return Unauthorized();
        //    if (!await _collectionsAuthService.IsUserQuestionAccessAsync(QuestionID, (int)userId))
        //        return Unauthorized();
        //    return Ok(await _QuestionsService.GetQuestionAsync(QuestionID));
        //}

        [HttpGet("randomQuestion")]
        public async Task<ActionResult<List<QuestionsDTOs.QuestionWithChoicesDto>>> GetRandomQuestionsWithChoices([FromQuery] int? collectionId)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();
            if (collectionId == null)
                return BadRequest("collectionId is required");

            return Ok(await _QuestionsService.GetRandomQuestionsWithChoicesAsync(collectionId.Value));
        }

    }
}
