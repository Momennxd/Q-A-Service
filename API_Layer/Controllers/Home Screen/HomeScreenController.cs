using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Core.DTOs.People.UsersDTOs;

namespace API_Layer.Controllers
{
    [Route("api/v1/home")]
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

        [HttpGet("collections/top")]
        public async Task<ActionResult<IEnumerable<CollectionsDTOs.SendCollectionDTO_Thumb>>> GetTop20Collections()
        {
            return Ok(await _collectionService.GetTop20Collections());
        }


        [HttpGet("followers/top")]
        public async Task<ActionResult<List<SendUserDTO>>> GetTop10FollowedUsers()
        {
            return Ok(await _userService.GetTopUsersAsync());
        }

    }
}
