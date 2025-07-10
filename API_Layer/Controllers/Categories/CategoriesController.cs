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
using static Core.DTOs.Categories.CategoriesDTOs;
using static Core.DTOs.Questions.QuestionsChoicesDTOs;

namespace API_Layer.Controllers.Categories
{

    [Route("api/v1/categories")]
    [ApiController]
    [Authorize]
    //THIS CONTROLLER IS FULLY TESTED BY MOMEN AND IT WORKS FINE AT 10 July 2025

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
        public async Task<ActionResult<List<SendCategoryDTO>>> GetCategories(string CategorySumName, int RowsCount)
        {
            return Ok(await _CategoriesService.GetCategories(CategorySumName, RowsCount));
        }


        [HttpPost]
        public async Task<ActionResult<SendCategoryDTO>> AddCategory(CategoriesDTOs.CreateCategoryDTO createCategoryDTO)
        {
            return Ok(await _CategoriesService.AddCategory(createCategoryDTO));
        }

    }
}
