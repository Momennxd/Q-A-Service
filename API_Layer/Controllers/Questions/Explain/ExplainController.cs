using API_Layer.Security;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Questions;
using Core.Services.Interfaces.Questions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Questions.Explain
{
    [Route("api/Explain")]
    [ApiController]
    public class ExplainController : ControllerBase
    {
        IAnswerExplanationService _answerExplanationService;
        ICollectionsAuthService _authService;

        public ExplainController(IAnswerExplanationService answerExplanationService, ICollectionsAuthService authService)
        {
            _answerExplanationService = answerExplanationService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> AddExplain(AnswerExplanationDTOs.AddAnswerExplanationDTO addAnswerExplanationDTO)
        {
            int? UserId = 1;// clsToken.GetUserID(HttpContext);
            if (UserId == null) return NotFound();
            
            if(! await _authService.IsUserQuestionOwnerAsync(addAnswerExplanationDTO.QuestionID, (int)UserId))
                return Unauthorized();




            return Ok(await _answerExplanationService.AddNewAsync(addAnswerExplanationDTO));
        }
    }
}
