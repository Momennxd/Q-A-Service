using API_Layer.Security;
using Core_Layer;
using Core_Layer.Glob;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Claims;

namespace API_Layer.Controllers
{
    [Route("API/People")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        public EmailController()
        {

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
        public ActionResult CreateNewToken(int userID)
        {
            return Ok(clsToken.CreateToken(userID));
        }

        //EndPoints------------------------------------------------------->




        //[HttpPost("SendEmail")]
        //public async Task<ActionResult> SendEmail([FromBody] EmailsDTOs.SendEmailDTO emailDTO, string ToMail)
        //{
        //    if (await clsCore.SendEmailAsync(ToMail, emailDTO.Subject, emailDTO.Body))
        //        return Ok(emailDTO);
        //    else
        //        return BadRequest("Error");
        //}


    }
}
