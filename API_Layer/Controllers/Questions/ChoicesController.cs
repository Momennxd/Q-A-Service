using API_Layer.Security;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Data.models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using System.Security.Claims;

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


        [HttpPost("")]
        public async Task<IActionResult> AddNewChoice(LinkedList<QuestionsChoicesDTOs.CreateChoiceDTO> createDtos)
        {
            if (createDtos.Count == 0) return Ok();
         
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userId == null) return Unauthorized();

            Dictionary<int, bool> questionIDsMap = new();

            List<QuestionsChoicesDTOs.CreateChoiceDTO> validatedCreateDtos = new(createDtos.Count);

            foreach (var dto in createDtos) {

                if (!questionIDsMap.ContainsKey(dto.QuestionID))
                {
                    if (await _collectionsAuthService.IsUserQuestionOwnerAsync(
                     dto.QuestionID, userId == null ? -1 : (int)userId))
                    {
                        validatedCreateDtos.Add(dto); questionIDsMap.Add(dto.QuestionID, true);
                    }
                    else questionIDsMap.Add(dto.QuestionID, false);                  
                }
                else             
                    if (questionIDsMap[dto.QuestionID])  validatedCreateDtos.Add(dto);                                    
            }


            if (validatedCreateDtos.Count == 0 && createDtos.Count != 0) return Unauthorized();

            return Ok(await _QuestionsChoicesService.AddChoiceAsync(validatedCreateDtos));
        }


        [HttpGet("")]
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


        [HttpGet("Answers")]
        public async Task<IActionResult> GetRightAnswers(int questionID)
        {

            //authorization:
            //1- if the caller is the creater, then no auth needed.
            //2- if the caller is the consumer, then the consumer must answer the question first to call the API
            //to prevent asnwers leak.
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            if (!await _collectionsAuthService.IsRightsAnswersAccessAsync(questionID, (int)userId))
                return Unauthorized();

            return Ok(await _QuestionsChoicesService.GetAllRightAnswersAsync(questionID));
        }


    }
}
