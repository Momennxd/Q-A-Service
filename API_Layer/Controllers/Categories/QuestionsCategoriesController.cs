using API_Layer.Extensions;
using Core.Authorization_Services.Interfaces;
using Core.Services.Concrete.nsCategories;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.DTOs.nsCategories.QuestionsCategoriesDTOs;

namespace API_Layer.Controllers.Categories
{

    [Route("api/v1/categories")]
    [ApiController]
    [Authorize]
    //THIS CONTROLLER IS FULLY TESTED BY MOMEN AND IT WORKS FINE AT 10 July 2025
    public class QuestionsCategoriesController : Controller
    {


        private readonly IQuestionsCategoriesService _QuestionsCategoriesService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public QuestionsCategoriesController(IQuestionsCategoriesService QuestionsCategoriesService,
            ICollectionsAuthService collectionsAuthService)
        {
            _collectionsAuthService = collectionsAuthService;
            _QuestionsCategoriesService = QuestionsCategoriesService;
        }




        [HttpGet("questions/{QuestionID}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<SendQuestionsCategoryDTO>>> GetQuestionCategories(int QuestionID)
        {
            return Ok(await _QuestionsCategoriesService.GetQuestionCategoriesAsync(QuestionID));
        }



        [HttpPost("questions/{QuestionID}")]
        public async Task<ActionResult<int>> AddQuestionCategories
             ([FromBody] List<CreateQuestionsCategoryDTO> createQuestionsCategoryDTOs, int QuestionID)
        {

            int userId = User.GetUserId();

            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
                return Unauthorized();


            return Ok(await _QuestionsCategoriesService.AddQuestionCategoryAsync
                (createQuestionsCategoryDTOs, QuestionID));
        }


        [HttpDelete("questions/{QuestionID}")]
        public async Task<ActionResult<int>> DeleteQuestionCategories(int QuestionID)
        {

            int userId = User.GetUserId();

            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
                return Unauthorized();

            return Ok(await _QuestionsCategoriesService.DeleteQuestionCategoriesAsync(QuestionID));
        }


        [HttpDelete("{QuestionCategoryID}")]
        public async Task<ActionResult<bool>> DeleteQuestionCategory(int QuestionCategoryID)
        {

            int userId = User.GetUserId();

            var e = await _QuestionsCategoriesService.FindAsync(QuestionCategoryID);

            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(e.QuestionID, userId))
                return Unauthorized();

            return Ok(await _QuestionsCategoriesService.DeleteQuestionCategoryAsync(QuestionCategoryID));
        }





    }
}
