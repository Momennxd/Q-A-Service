using API_Layer.Security;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Collections;
using Core.DTOs.Questions;
using Core.Services.Concrete.Collections;
using Core.Services.Interfaces;
using Data.models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using System.Security.Claims;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace API_Layer.Controllers.Questions
{


    /// VERY IMPORTANT READ about  choices authurization
    // the level of auth that is required to make it impossible for the user to get the choices of a question
    //that is not thiers and private will make the API slower especially when the user sends a set of choices then u have to interate through
    //the set and make sure each choices is well authurized, so that makes the API SLOWER and speaking of security it does not really matter
    //AT THIS TIME if the user could break through this API and gets something they are not AUTHORIZED to do 
    //bets choices here is making this API AS FAST AS WE CAN IN THIS RELEASE so no authrization is implemented here at all just the normal
    //AUTHENTICATION using JWT
    //this method is just used for now 29 Apirl, so in the future we might consider using full authrization here with a diff method to make it 
    //faster and more secure.
    //thanks for reading

    [Route("api/choices")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> AddNewChoices([FromBody] List<CreateChoiceDTO> createDtos, int QuestionID)
        {

            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userId == null) return Unauthorized();

            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(
                QuestionID, userId == null ? -1 : (int)userId))
            {
                return Unauthorized();
            }

            return Ok(await _QuestionsChoicesService.AddChoiceAsync(createDtos, QuestionID));
        }



        [HttpGet("questions/{questionID}")]
        public async Task<IActionResult> GetChoices(int questionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            //to understand why this is commented out READ THE ABOVE paragraph =>

            //if (!await _collectionsAuthService.IsUserQuestionAccessAsync(
            //        questionID, userId == null ? -1 : (int)userId))
            //{
            //    return Unauthorized();
            //}

            return Ok(await _QuestionsChoicesService.GetChoicesAsync(questionID));
        }

        [HttpGet]
        public async Task<IActionResult> GetChoices([FromBody] HashSet<int> setQuestionIDs)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            //to understand why this is commented out READ THE ABOVE paragraph =>

            //if (!await _collectionsAuthService.IsUserQuestionAccessAsync(
            //        setQuestionIDs, userId == null ? -1 : (int)userId))
            //{
            //    return Unauthorized();
            //}

            return Ok(await _QuestionsChoicesService.GetChoicesAsync(setQuestionIDs));
        }


        [HttpGet("answers")]
        public async Task<IActionResult> GetRightAnswers(int questionID)
        {

            //authorization:
            //1- if the caller is the creater, then no authorization needed.
            //2- if the caller is the consumer, then the consumer must answer the question first to call the API to prevent right answers leak.
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            if (!await _collectionsAuthService.IsRightsAnswersAccessAsync(questionID, (int)userId))
                return Unauthorized();

            return Ok(await _QuestionsChoicesService.GetAllRightAnswersAsync(questionID));
        }



        [HttpPatch("{ChoiceID}")]
        public async Task<IActionResult> PatchChoice
           ([FromBody] JsonPatchDocument<PatchChoiceDTO> patchDoc, int ChoiceID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            if (!await _collectionsAuthService.IsUserChoiceOwnerAsync(ChoiceID, userId))
                return Unauthorized();


            return Ok(await _QuestionsChoicesService.PatchChoiceAsync(patchDoc, ChoiceID));
        }



        [HttpDelete("{ChoiceID}")]
        public async Task<IActionResult> DeleteChoice(int ChoiceID)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            if (!await _collectionsAuthService.IsUserChoiceOwnerAsync(ChoiceID, userId))
                return Unauthorized();

            return Ok(await _QuestionsChoicesService.DeleteChoiceAsync(ChoiceID));

        }

    }
}
