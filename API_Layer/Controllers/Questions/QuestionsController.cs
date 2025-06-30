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
using API_Layer.Extensions;

namespace API_Layer.Controllers.Questions
{

    [Route("api/v1/questions")]
    [ApiController]
    [Authorize]
    //THIS CONTROLLER IS FULLY TESTED BY MOMEN ON 30/6/2025
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


        [HttpPost]
        public async Task<ActionResult<List<SendQuestionDTO>>> CreateQuestions(
            [FromBody] List<CreateQuestionDTO> createDtos, int CollectionID)
        {
            int userId = User.GetUserId();


            if (!await _collectionsAuthService.IsUserCollecOwnerAsync(CollectionID, (int)userId))
                return Unauthorized();


            return Ok(await _QuestionsService.CreateQuestionsAsync(createDtos, CollectionID, (int)userId));
        }


        [HttpGet]
        public async Task<ActionResult<SendQuestionDTO>> GetCollectionQuestions(int CollectionID)
        {
            int userId = User.GetUserId();


            //validate if the collection is not the users and its private then reutrn unauth
            if (!await _collectionsAuthService.IsUserCollectionAccess(CollectionID, (int)userId))
                return Unauthorized();

            return Ok(await _QuestionsService.GetAllQuestionsAsync(CollectionID));
        }







        [HttpPatch("{QuestionID}")]
        public async Task<ActionResult<SendQuestionDTO>> PatchQuestion
           ([FromBody] JsonPatchDocument<PatchQuestionDTO> patchDoc, int QuestionID)
        {

            int userId = User.GetUserId();


            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
                return Unauthorized();


            return Ok(await _QuestionsService.PatchQuestionAsync(patchDoc, QuestionID));
        }



        [HttpPatch("points/{QuestionID}")]
        public async Task<ActionResult<int>> PatchQuestionPoints(int QuestionID, int NewPointsVal)
        {

            int userId = User.GetUserId();


            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
                return Unauthorized();


            return Ok(await _QuestionsService.PatchQuestionPointsAsync(QuestionID, NewPointsVal));
        }



        [HttpDelete]
        public async Task<ActionResult<int>> DeleteQuestion(int QuestionID)
        {
            int userId = User.GetUserId();


            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, (int)userId))
                return Unauthorized();



            return Ok(await _QuestionsService.DeleteQuestionAsync(QuestionID));

        }


        [HttpGet("{QuestionID}")]
        public async Task<ActionResult<SendQuestionDTO>> GetQuestion(int QuestionID)
        {
            int userId = User.GetUserId();
            
            if (!await _collectionsAuthService.IsUserQuestionAccessAsync(QuestionID, (int)userId))
                return Unauthorized();
            
            return Ok(await _QuestionsService.GetQuestionAsync(QuestionID));
        }

        [HttpGet("random")]
        public async Task<ActionResult<List<QuestionWithChoicesDto>>> GetRandomQuestionsWithChoices([FromQuery] int? collectionId)
        {
            int userId = User.GetUserId();
            
            if (collectionId == null)
                return BadRequest("collectionId is required");

            return Ok(await _QuestionsService.GetRandomQuestionsWithChoicesAsync(collectionId.Value));
        }

    }
}
