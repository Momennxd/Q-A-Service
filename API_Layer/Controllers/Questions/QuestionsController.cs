using Core.Authorization_Services.Interfaces;
using Core.DTOs.Questions;
using Core.Services.Concrete.Questions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;
using System.Security.Claims;
using static Core.DTOs.Questions.QuestionsDTOs;

namespace API_Layer.Controllers.Questions
{

    [Route("API/Questions")]
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


        [HttpPost]
        public async Task<IActionResult> CreateQuestions([FromBody]List<CreateQuestionDTO> createDtos, int CollectionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null)  return Unauthorized(); 

            if (!await _collectionsAuthService.IsUserCollecOwnerAsync(CollectionID, (int)userId))
                   return Unauthorized();
                    

            return Ok(await _QuestionsService.CreateQuestions(createDtos, CollectionID, (int)userId));
        }


        [HttpGet]
        public async Task<IActionResult> GetCollectionQuestions(int CollectionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            //validate if the collection is not the users and its private then reutrn unauth
            if (!await _collectionsAuthService.IsUserCollectionAccess(CollectionID, (int)userId))
                return Unauthorized();

            return Ok(await _QuestionsService.GetAllQuestions(CollectionID));
        }




    }
}
