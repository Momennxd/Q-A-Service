﻿//using API_Layer.Authorization;
//using Core.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace API_Layer.Controllers.Admin.People
//{
//    [Route("api/UsersAdmin")]
//    [ApiController]
//    public class UsersAdminController : ControllerBase
//    {
//        IUserService _userService;

//        public UsersAdminController(IUserService userService)
//        {
//            _userService = userService;
//        }

//        [HttpGet("GetUser/{UserID}")]
//        [Authorize]
//        [CheckPermission(Permissions.GetUser)]
//        public async Task<IActionResult> GetUser(int UserID)
//        {
//            var user = await _userService.GetUser(UserID);
//            if (user == null)
//                return NotFound();

//            return Ok(user);
//        }
//        [HttpGet("GetUserByUsername/{Username}")]
//        [Authorize]
//        [CheckPermission(Permissions.GetUser)]
//        public async Task<IActionResult> GetUserByUsername(string Username)
//        {
//            var user = await _userService.GetUser(Username);
//            if (user == null)
//                return NotFound();

//            return Ok(user);
//        }
//    }
//}
