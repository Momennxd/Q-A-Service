using Core.DTOs.Questions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_Layer.Controllers.Questions
{

    [Route("API/Collections/Choices")]
    [ApiController]
    public class ChoicesController : Controller
    {
        private readonly IQuestionsChoicesService _QuestionsChoicesService;

        public ChoicesController(IQuestionsChoicesService QuestionsChoicesService)
        {
            _QuestionsChoicesService = QuestionsChoicesService;
        }




        [HttpPost("")]
        public async Task<IActionResult> AddNewChoice(List<QuestionsChoicesDTOs.CreateChoiceDTO> createDtos)
        {
            return Ok(await _QuestionsChoicesService.AddChoiceAsync(createDtos));
        }


        [HttpGet("")]
        public async Task<IActionResult> GetChoices(int questionID)
        {
             var output = await _QuestionsChoicesService.GetChoices(questionID);

            return output.Count == 0 ? StatusCode(201) : Ok(output); 
        }





    }
}
