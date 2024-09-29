using API_Layer.Security;
using Core.DTOs.People;
using Core.Services.Interfaces;
using Data.models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Core.DTOs.People.UsersDTOs;

namespace API_Layer.Controllers.People
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }




        [HttpPost("Sigin in")]
        public async Task<ActionResult> Signin(AddUserDTO addUserDTO)
        {
            var user = await _userService.CreateUserAsync(addUserDTO);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userService.Login(loginDTO);
            if (user == null)
                return BadRequest("Wrong username or password");

            return Ok(clsToken.CreateToken(user.UserId));
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult> UpdateUserInfo(JsonPatchDocument<User> UpdatedItem)
        {
            var user = await _userService.PatchUserAsync(UpdatedItem, clsToken.GetUserID(HttpContext));


            return Ok();
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetUser()
        {
            var user = await _userService.GetUserByIdAsync(clsToken.GetUserID(HttpContext));


            return Ok(user);
        }

    }
}
