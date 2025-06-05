using Core.DTOs.Collections;
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
        private readonly IUserService _userService;

        public HomeScreenController(ICollectionService collectionService, IUserService userService)
        {
            _collectionService = collectionService;
            _userService = userService;
        }

        [HttpGet("Top20Collections")]
        public async Task<ActionResult<IEnumerable<CollectionsDTOs.SendCollectionDTO_Thumb>>> GetTop20Collections()
        {
            return Ok(await _collectionService.GetTop20Collections());
        }


        [HttpGet("Top10FollowedUsers")]
        public async Task<IActionResult> GetTop10FollowedUsers()
        {
            return Ok(await _userService.GetTopUsersAsync());
        }

    }
}
