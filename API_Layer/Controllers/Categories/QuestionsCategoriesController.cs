using Core.Authorization_Services.Interfaces;
using Core.Services.Concrete.nsCategories;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.DTOs.nsCategories.QuestionsCategoriesDTOs;

namespace API_Layer.Controllers.Categories
{

    [Route("api/categories")]
    [ApiController]
    [Authorize]
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




        [HttpGet("Questions/{QuestionID}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetQuestionCategories(int QuestionID)
        {
            return Ok(await _QuestionsCategoriesService.GetQuestionCategoriesAsync(QuestionID));
        }



        [HttpPost("Questions/{QuestionID}")]
        public async Task<IActionResult> AddQuestionCategories
             ([FromBody]List<CreateQuestionsCategoryDTO> createQuestionsCategoryDTOs, int QuestionID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
                return Unauthorized();


            return Ok(await _QuestionsCategoriesService.AddQuestionCategoryAsync
                (createQuestionsCategoryDTOs, QuestionID));
        }


        [HttpDelete("Questions/{QuestionID}")]
        public async Task<IActionResult> DeleteQuestionCategories(int QuestionID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(QuestionID, userId))
                return Unauthorized();

            return Ok(await _QuestionsCategoriesService.DeleteQuestionCategoriesAsync(QuestionID));
        }


        [HttpDelete("{QuestionCategoryID}")]
        public async Task<IActionResult> DeleteQuestionCategory(int QuestionCategoryID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var e = await _QuestionsCategoriesService.FindAsync(QuestionCategoryID);

            if (!await _collectionsAuthService.IsUserQuestionOwnerAsync(e.QuestionID, userId))
                return Unauthorized();

            return Ok(await _QuestionsCategoriesService.DeleteQuestionCategoryAsync(QuestionCategoryID));
        }





    }
}
