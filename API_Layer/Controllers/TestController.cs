using API_Layer.Security;
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
using Core.DTOs.Pictures;
using Core.Services.Concrete.Questions;
using Core.DTOs.Questions;
using Microsoft.AspNetCore.Routing.Constraints;

namespace API_Layer.Controllers.Collections
{
    [Route("API/Test")]
    [ApiController]
    public class TestController : Controller
    {

        private readonly ICloudinaryService _cloudinary;
        private readonly IChoicesPicsService _PicsService;
        private readonly IPicsService PicsService;
        private readonly IQuestionsChoicesService choicesService;
        private readonly ILogger<TestController> _logger;

        public TestController(ICloudinaryService cloudinary, IChoicesPicsService picsService,
            IPicsService pics, IQuestionsChoicesService questionsChoicesService, ILogger<TestController> logger)
        {
            this.PicsService = pics;
            _cloudinary = cloudinary;
            _PicsService = picsService;
            choicesService = questionsChoicesService;
            _logger = logger;
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
        public async Task<IActionResult> UploadImageAsync
            ([FromForm] IFormFile file, string folderPath, string fileName)
        {
            if (string.IsNullOrEmpty(folderPath) || file == null)
                throw new ArgumentException("Invalid parameters. Folder path," +
                    " file stream, and file name must be provided.");

            var createDto = new PicsDTOs.CreatePicDTO()
            {
                pic = new PicsDTOs.PicDTO() { file = (FormFile)file, FileName = fileName, FolderPath = folderPath },
                Rank = 0

            };




            return Ok(await PicsService.CreatePicAsync(createDto));
        }





        [HttpGet("TestCriticalLog" ,Name = "TestCriticalLog")]
        public  IActionResult GetTest()
        {
            _logger.LogCritical("Test");
            return Ok();
        }




    }
}
