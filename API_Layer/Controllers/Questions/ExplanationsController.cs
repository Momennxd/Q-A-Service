using API_Layer.Extensions;
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
    [Route("api/v1/explanations")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<bool>> AddExplain(AnswerExplanationMainDTO addAnswerExplanationDTO)
        {
            int userId = User.GetUserId();

            if (!await _authService.IsUserQuestionOwnerAsync(addAnswerExplanationDTO.QuestionID, (int)userId))
                return Unauthorized();

            return Ok(await _answerExplanationService.AddNewAsync(addAnswerExplanationDTO));
        }


        [HttpGet("questions/{QuestionID}")]
        public async Task<ActionResult<List<GetAnswerExplanationDTO>>> GetAnswerExplainationByQuestionID(int QuestionID)
        {
            int userId = User.GetUserId();
            if (!await _authService.IsUserQuestionAccessAsync(QuestionID, (int)userId))
                return Unauthorized();
            return Ok(await _answerExplanationService.GetAnswerExplanationByQuestionIDAsync(QuestionID));
        }



        [HttpGet("{ExplainID}")]
        public async Task<ActionResult<GetAnswerExplanationDTO>> GetAnswerExplaination(int ExplainID)
        {
            int userId = User.GetUserId();


            var result = await _answerExplanationService.GetAnswerExplanationAsync(ExplainID);

            if (!await _authService.IsUserQuestionAccessAsync(result.QuestionID, (int)userId))
                return Unauthorized();

            return Ok(result);
        }
    }
}
