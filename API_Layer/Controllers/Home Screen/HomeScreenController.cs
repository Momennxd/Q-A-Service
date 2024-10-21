using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/HomeScreen")]
    [ApiController]
    public class HomeScreenController : ControllerBase
    {


        private readonly ICollectionService _collectionService;

        public HomeScreenController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet("Top20Collections")]
        public async Task<IActionResult> GetTop20Collections()
        {
            return Ok(await _collectionService.GetTop20Collections());
        }
    }
}
