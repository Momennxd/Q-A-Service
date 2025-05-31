using API_Layer.Security;
using Core.DTOs.People;
using Core.Services.Interfaces;
using Data.models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.DTOs.People.UsersDTOs;

namespace API_Layer.Controllers.People
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }




        [HttpPost("signup")]
        public async Task<ActionResult> Signup(AddUserDTO addUserDTO)
        {
            var user = await _userService.CreateUserAsync(addUserDTO);
            return StatusCode(201, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userService.Login(loginDTO);
            if (user == null)
                return BadRequest("Wrong username or password");

            return Ok(clsToken.CreateToken(user.UserId));
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult> UpdateUserInfo(JsonPatchDocument<AddUserDTO> UpdatedItem)
        {
            var user = await _userService.PatchUser(UpdatedItem, clsToken.GetUserID(HttpContext));

            return Ok(user);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetUser()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId <= 0) return BadRequest();
            var user = await _userService.GetUserByIdAsync(userId);

            return Ok(user);
        }

        //[HttpDelete]
        //[Authorize]
        //public async Task<ActionResult> Delete()
        //{
        //    return Ok(await _userService.DeleteUserAsync(clsToken.GetUserID(HttpContext)));
        //}


    }
}
