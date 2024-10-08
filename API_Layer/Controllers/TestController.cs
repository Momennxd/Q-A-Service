﻿using API_Layer.Security;
using AutoMapper;
using Core.DTOs.People;
using Core.Services.Concrete.People;
using Core.Services.Concrete.Users;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Services.Concrete;
using Services.Interfaces;

namespace API_Layer.Controllers.Collections
{
    [Route("API/Test")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly ICloudinaryService _cloudinary;


        public TestController(ICloudinaryService cloudinary)
        {
            _cloudinary = cloudinary;
        }

        [HttpGet]
        [Route("GetClaims")]
        [Authorize]
        public IActionResult GetClaims()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            var userId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok(new { UserId = userId });
        }

        [HttpPost]
        [Route("CreateNewToken")]
        public ActionResult CreateNewToken(int userID)
        {
            return Ok(clsToken.CreateToken(userID));
        }


        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImageAsync([FromForm] IFormFile file, string folderPath, string fileName)
        {
            if (string.IsNullOrEmpty(folderPath) || file == null)
                throw new ArgumentException("Invalid parameters. Folder path, file stream, and file name must be provided.");

           return Ok(await _cloudinary.UploadImageAsync(file.OpenReadStream(), folderPath, fileName));
        }



    }
}
