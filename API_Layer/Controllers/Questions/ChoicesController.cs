using API_Layer.Security;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Data.models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Claims;

namespace API_Layer.Controllers.Questions
{

    [Route("API/Choices")]
    [ApiController]
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
        [Authorize]
        public async Task<IActionResult> AddNewChoice(List<QuestionsChoicesDTOs.CreateChoiceDTO> createDtos)
        {
            if (createDtos.Count == 0) return Ok();

             
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userId == null) return Unauthorized();

            HashSet<int> questionIDsSet = new HashSet<int>();
            foreach(var dto in createDtos) { questionIDsSet.Add(dto.QuestionID); }

            foreach(var id in  questionIDsSet) {
                if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(
                    id, userId == null ? -1 : (int)userId)) return Unauthorized();
            }
           

            return Ok(await _QuestionsChoicesService.AddChoiceAsync(createDtos));
        }


        [HttpGet("")]
        public async Task<IActionResult> GetChoices(int questionID)
        {

            //authorization ---> TODO



            return Ok(await _QuestionsChoicesService.GetChoicesAsync(questionID));
        }


        [HttpGet("Answers")]
        public async Task<IActionResult> GetRightAnswers(int questionID)
        {

            //authuntication ---> TODO



            return Ok(await _QuestionsChoicesService.GetAllRightAnswersAsync(questionID));
        }


    }
}
