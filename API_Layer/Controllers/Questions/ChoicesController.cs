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

    [Route("API/Choices")]
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
        public async Task<IActionResult> AddNewChoices([FromBody]List<CreateChoiceDTO> createDtos, int QuestionID)
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


        [HttpGet("Questions/{questionID}")]
        public async Task<IActionResult> GetChoices(int questionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();
            if (!await _collectionsAuthService.IsUserQuestionAccessAsync(
                    questionID, userId == null ? -1 : (int)userId)) {
                return Unauthorized();
            }

            return Ok(await _QuestionsChoicesService.GetChoicesAsync(questionID));
        }

        [HttpGet]
        public async Task<IActionResult> GetChoices([FromBody] HashSet<int> setQuestionIDs)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            if (!await _collectionsAuthService.IsUserQuestionAccessAsync(
                    setQuestionIDs, userId == null ? -1 : (int)userId))
            {
                return Unauthorized();
            }

            return Ok(await _QuestionsChoicesService.GetChoicesAsync(setQuestionIDs));
        }


        [HttpGet("Answers")]
        public async Task<IActionResult> GetRightAnswers(int questionID)
        {

            //authorization:
            //1- if the caller is the creater, then no authorization needed.
            //2- if the caller is the consumer, then the consumer must answer the question first to call the API
            //to prevent right answers leak.
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


            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(ChoiceID, userId))
                return Unauthorized();


            return Ok(await _QuestionsChoicesService.PatchChoice(patchDoc, ChoiceID));
        }



        [HttpDelete("{ChoiceID}")]
        public async Task<IActionResult> DeleteChoice(int ChoiceID)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(ChoiceID, userId))
                return Unauthorized();

            return await _QuestionsChoicesService.DeleteChoice(ChoiceID) == true ? Ok(1) : BadRequest();

        }

    }
}
