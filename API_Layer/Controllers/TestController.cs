﻿using API_Layer.Security;
using AutoMapper;
using Core.DTOs.People;
using Core.Services.Concrete.People;
using Core.Services.Concrete.Users;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Security.Claims;

namespace API_Layer.Controllers.Collections
{
    [Route("API/Test")]
    [ApiController]
    public class TestController : ControllerBase
    {

        //public TestController(UserService userService, PeopleService ps)
        //{
        //    PeopleService = ps;
        //    userSerivce = userService;
        //}

        ////private ILogger _Logger;


        //private readonly IUserService userSerivce;
        //private readonly IPersonService PeopleService;


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

     


    }
}
