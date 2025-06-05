using Core.Authorization_Services.Interfaces;
using Core.DTOs.Questions;
using Core.Services.Interfaces.Questions;
using Data.models.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            if (!await _authService.IsUserQuestionOwnerAsync(addAnswerExplanationDTO.QuestionID, (int)userId))
                return Unauthorized();




            return Ok(await _answerExplanationService.AddNewAsync(addAnswerExplanationDTO));
        }


        [HttpGet("Questions/{QuestionID}")]
        public async Task<IActionResult> GetAnswerExplainationByQuestionID(int QuestionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            if (!await _authService.IsUserQuestionAccessAsync(QuestionID, (int)userId))
                return Unauthorized();




            return Ok(await _answerExplanationService.GetAnswerExplanationByQuestionIDAsync(QuestionID));
        }



        [HttpGet("{ExplainID}")]
        public async Task<IActionResult> GetAnswerExplaination(int ExplainID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            var result = await _answerExplanationService.GetAnswerExplanationAsync(ExplainID);

            if (!await _authService.IsUserQuestionAccessAsync(result.QuestionID, (int)userId))
                return Unauthorized();




            return Ok(result);
        }
    }
}
