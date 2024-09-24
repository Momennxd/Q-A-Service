using API_Layer.Security;
using AutoMapper;
using Core.Services.Concrete.Users;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Core_Layer;
using Core_Layer.models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Security.Claims;

namespace API_Layer.Controllers.Collections
{
    [Route("API/People")]
    [ApiController]
    public class TestController : ControllerBase
    {

        public TestController(IMapper mapper, UserService userService)
        {
            _mapper = mapper;


            userSerivce = userService;
        }

        //private ILogger _Logger;

        private IMapper _mapper;

        private readonly IUserService userSerivce;


        //[HttpGet]
        //[Route("GetClaims")]
        //[Authorize]
        //public IActionResult GetClaims()
        //{
        //    var claims = HttpContext.User.Identity as ClaimsIdentity;

        //    var userId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    return Ok(new { UserId = userId });
        //}
        //[HttpPost]
        //public ActionResult CreateNewToken(int userID)
        //{
        //    return Ok(clsToken.CreateToken(userID));
        //}

        //EndPoints------------------------------------------------------->


        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var userDto = await userSerivce.GetAllUsersAsync();
            return Ok(userDto);
        }


        [HttpGet("User")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var userDto = await userSerivce.GetUserByIdAsync(id);
            return Ok(userDto);
        }

    }
}
