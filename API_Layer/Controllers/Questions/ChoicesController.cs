using API_Layer.Extensions;
using Core.Authorization_Services.Interfaces;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace API_Layer.Controllers.Questions
{


    /// VERY IMPORTANT READ about  choices authurization
    // the level of auth that is required to make it impossible for the user to get the choices of a question
    //that is not thiers and private will make the API slower especially when the user sends a set of choices then u have to interate through
    //the set and make sure each choice is well authurized, so that makes the API SLOWER and speaking of security it does not really matter
    //AT THIS TIME if the user could break through this API and gets something they are not AUTHORIZED to do 
    //best choice here is making this API AS FAST AS WE CAN IN THIS RELEASE so no authrization is implemented here at all just the normal
    //AUTHENTICATION using JWT
    //this method is just used for now 29 Apirl 2025, so in the future we might consider using full authrization here with a diff method to make it 
    //faster and more secure.
    //thanks for reading

    [Route("api/v1/choices")]
    [ApiController]
    [Authorize]
    //THIS CONTROLLER IS FULLY TESTED BY MOMEN AND IT WORKS FINE AT 10 July 2025
    public class ChoicesController : Controller
    {
        private readonly IQuestionsChoicesService _QuestionsChoicesService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public ChoicesController(IQuestionsChoicesService QuestionsChoicesService,
            ICollectionsAuthService collectionsAuthService)
        {
            _collectionsAuthService = collectionsAuthService;
            _QuestionsChoicesService = QuestionsChoicesService;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SendChoiceDTO>>> AddNewChoices([FromBody] List<CreateChoiceDTO> createDtos, int QuestionID)
        {
            int userId = User.GetUserId();
            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
                return Unauthorized();
            return Ok(await _QuestionsChoicesService.AddChoiceAsync(createDtos, QuestionID));
        }




        [HttpGet("questions/{questionID}")]
        public async Task<ActionResult<List<SendChoiceDTO>>> GetChoices(int questionID)
        {
            int userId = User.GetUserId();

            //to understand why this is commented out READ THE ABOVE paragraph =>

            //if (!await _collectionsAuthService.IsUserQuestionAccessAsync(
            //        questionID, userId == null ? -1 : (int)userId))
            //{
            //    return Unauthorized();
            //}

            return Ok(await _QuestionsChoicesService.GetChoicesAsync(questionID));
        }




        [HttpGet]
        public async Task<ActionResult<Dictionary<int, List<SendChoiceDTO>>>> GetChoices([FromBody] HashSet<int> setQuestionIDs)
        {
            //to understand why this is commented out READ THE ABOVE paragraph =>
            //int userId = User.GetUserId();

            //if (!await _collectionsAuthService.IsUserQuestionAccessAsync(
            //        setQuestionIDs, userId == null ? -1 : (int)userId))
            //{
            //    return Unauthorized();
            //}

            return Ok(await _QuestionsChoicesService.GetChoicesAsync(setQuestionIDs));
        }


        [HttpGet("answers/{questionID}")]
        public async Task<ActionResult<List<SendChoiceDTO>>> GetRightAnswers(int questionID)
        {

            //authorization:
            //1- if the caller is the creater, then no authorization needed.
            //2- if the caller is the consumer, then the consumer must answer the question first to call the API to prevent right answers leak.
            int userId = User.GetUserId();

            if (!await _collectionsAuthService.IsRightsAnswersAccessAsync(questionID, (int)userId))
                return Unauthorized();

            return Ok(await _QuestionsChoicesService.GetAllRightAnswersAsync(questionID));
        }





        [HttpPatch("{ChoiceID}")]
        public async Task<ActionResult<SendChoiceDTO>> PatchChoice
           ([FromBody] JsonPatchDocument<PatchChoiceDTO> patchDoc, int ChoiceID)
        {

            int userId = User.GetUserId();


            if (!await _collectionsAuthService.IsUserChoiceOwnerAsync(ChoiceID, userId))
                return Unauthorized();


            return Ok(await _QuestionsChoicesService.PatchChoiceAsync(patchDoc, ChoiceID));
        }





        [HttpDelete("{ChoiceID}")]
        public async Task<ActionResult<int>> DeleteChoice(int ChoiceID)
        {
            int userId = User.GetUserId();


            if (!await _collectionsAuthService.IsUserChoiceOwnerAsync(ChoiceID, userId))
                return Unauthorized();

            return Ok(await _QuestionsChoicesService.DeleteChoiceAsync(ChoiceID));

        }




        ///TERMINATED FOR NOW BY MOMEN (There is not point of this endpoint + it does not even work right)
        //[HttpGet("explanation/{choiceId:int}/{questionId:int}")]
        //public async Task<ActionResult<SendExplanationWithRightAnswerDTO>> GetChoiceWithExplanation(
        //    [FromRoute] int choiceId,
        //    [FromRoute] int questionId
        //    )
        //{
        //    int userId = User.GetUserId();
        //    var choiceWithExplanation = await _QuestionsChoicesService.GetRightChoiceWithExplanationAsync(choiceId, questionId);
        //    if (choiceWithExplanation == null)
        //        return NotFound();
        //    return Ok(choiceWithExplanation);

        //}
    }
}
