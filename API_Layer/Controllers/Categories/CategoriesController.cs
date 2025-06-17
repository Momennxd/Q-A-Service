using Core.Authorization_Services.Interfaces;
using Core.DTOs.Categories;
using Core.DTOs.Collections;
using Core.DTOs.Questions;
using Core.Services.Concrete.Collections;
using Core.Services.Interfaces;
using Data.models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using System.Security.Claims;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace API_Layer.Controllers.Categories
{

    [Route("api/v1/categories")]
    [ApiController]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _CategoriesService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public CategoriesController(ICategoriesService QuestionCategoriesService,
            ICollectionsAuthService collectionsAuthService)
        {
            _collectionsAuthService = collectionsAuthService;
            _CategoriesService = QuestionCategoriesService;
        }



        [HttpGet("{RowsCount}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories(string CategorySumName, int RowsCount)
        {
            return Ok(await _CategoriesService.GetCategories(CategorySumName, RowsCount));
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoriesDTOs.CreateCategoryDTO createCategoryDTO)
        {
            return Ok(await _CategoriesService.AddCategory(createCategoryDTO));
        }

    }
}
