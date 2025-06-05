using Core.Authorization_Services.Interfaces;
using Core.Services.Concrete.Questions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace API_Layer.Controllers.Questions
{ 

    [Route("api/choices/chosen")]
    [ApiController]
    [Authorize]
    public class ChosenChoicesController : Controller
    {
        private readonly IChosenChoicesService _ChosenChoicesService;

        public ChosenChoicesController(IChosenChoicesService ChosenChoicesService)
        {
            _ChosenChoicesService = ChosenChoicesService;
        }



        [HttpGet("{submitionID}")]
        public async Task<IActionResult> GetChosenChoices([FromHeader] HashSet<int> QuestionIDs, int submitionID)
        {
            int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) return Unauthorized();

            return Ok(await _ChosenChoicesService.GetChosenChoices(QuestionIDs, submitionID, (int)userId));
        }





    }
}
