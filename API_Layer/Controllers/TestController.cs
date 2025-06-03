using Core.DTOs.Pictures;
using Core.Services.Interfaces;
using Core.Services.Interfaces.RefreshTokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

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
        private readonly ITokenService _tokenService;
        public TestController(ICloudinaryService cloudinary, IChoicesPicsService picsService,
            IPicsService pics, IQuestionsChoicesService questionsChoicesService, ILogger<TestController> logger
            , ITokenService tokenService)
        {
            this.PicsService = pics;
            _cloudinary = cloudinary;
            _PicsService = picsService;
            choicesService = questionsChoicesService;
            _logger = logger;
            _tokenService = tokenService;
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
            return Ok(_tokenService.CreateToken(userID));
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





        [HttpGet("TestCriticalLog", Name = "TestCriticalLog")]
        public IActionResult GetTest()
        {
            _logger.LogCritical("Test");
            return Ok();
        }




    }
}
