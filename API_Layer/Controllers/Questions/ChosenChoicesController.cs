﻿using API_Layer.Extensions;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Questions;
using Core.Services.Concrete.Questions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Claims;
using static Core.DTOs.Questions.chosen_choicesDTOs;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace API_Layer.Controllers.Questions
{ 
    [Route("api/v1/choices/chosen")]
    [ApiController]
    [Authorize]
    //FULLY TESTED BY MOMEN AT 10/7/2025
    public class ChosenChoicesController : Controller
    {
        private readonly IChosenChoicesService _ChosenChoicesService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public ChosenChoicesController(IChosenChoicesService ChosenChoicesService, ICollectionsAuthService collectionsAuthService)
        {
            _ChosenChoicesService = ChosenChoicesService;
            _collectionsAuthService = collectionsAuthService;
        }



        [HttpGet("submition/{submitionID}")]
        public async Task<ActionResult<Dictionary<int, send_chosen_choicesDTO>>> GetChosenChoices([FromBody] HashSet<int> QuestionIDs, int submitionID)
        {
            int userId = User.GetUserId();

            //authorization (checking if the sub is the user's) check will make the api slower and it is not really needed here at the moment

            return Ok(await _ChosenChoicesService.GetChosenChoices(QuestionIDs, submitionID, (int)userId));
        }

        [HttpPost()]
        public async Task<ActionResult<send_chosen_choicesDTO>> AddChosenChoice([FromBody] Add_chosen_choicesDTO add_Chosen_Choice)
        {
            int userId = User.GetUserId();

            //submition has to be the user's who is chosing the this choice
            if (!await _collectionsAuthService.IsUserSubmitionOwnerAsync(add_Chosen_Choice.SubmitionID, (int)userId)) return Unauthorized();

            return Ok(await _ChosenChoicesService.AddChosenChoices(add_Chosen_Choice, (int)userId));
        }



    }
}
