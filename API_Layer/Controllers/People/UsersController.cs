using API_Layer.Security;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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


    }
}
