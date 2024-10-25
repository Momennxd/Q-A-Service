using API_Layer.Security;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Questions;
using Core.Services.Interfaces.Questions;
using Data.models.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Core.DTOs.Questions.AnswerExplanationDTOs;

namespace API_Layer.Controllers.Questions
{
    [Route("api/explanations")]
    [ApiController]
    public class ExplanationsController : ControllerBase
    {
        IAnswerExplanationService _answerExplanationService;
        ICollectionsAuthService _authService;

        public ExplanationsController(IAnswerExplanationService answerExplanationService, ICollectionsAuthService authService)
        {
            _answerExplanationService = answerExplanationService;
            _authService = authService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExplain(AnswerExplanationMainDTO addAnswerExplanationDTO)
        {
            int? UserId = clsToken.GetUserID(HttpContext);
            if (UserId == null) return Unauthorized();

            if (!await _authService.IsUserQuestionOwnerAsync(addAnswerExplanationDTO.QuestionID, (int)UserId))
                return Unauthorized();




            return Ok(await _answerExplanationService.AddNewAsync(addAnswerExplanationDTO));
        }


        [HttpGet("Questions/{QuestionID}")]
        public async Task<IActionResult> GetAnswerExplainationByQuestionID(int QuestionID)
        {
            int? UserId = clsToken.GetUserID(HttpContext);
            if (UserId == null) return Unauthorized();

            if (!await _authService.IsUserQuestionAccessAsync(QuestionID, (int)UserId))
                return Unauthorized();




            return Ok(await _answerExplanationService.GetAnswerExplanationByQuestionIDAsync(QuestionID));
        }



        [HttpGet("{ExplainID}")]
        public async Task<IActionResult> GetAnswerExplaination(int ExplainID)
        {
            int? UserId = clsToken.GetUserID(HttpContext);
            if (UserId == null) return Unauthorized();

            var result = await _answerExplanationService.GetAnswerExplanationAsync(ExplainID);

            if (!await _authService.IsUserQuestionAccessAsync(result.QuestionID, (int)UserId))
                return Unauthorized();




            return Ok(result);
        }
    }
}
