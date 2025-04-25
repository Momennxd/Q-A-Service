using API_Layer.Security;
using Core.DTOs.Institution;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Institutions
{
    [Route("api/institutions")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        IInstitutionServce _InstitutionServce { get; set; }

        public InstitutionController(IInstitutionServce institutionServce)
        {
            _InstitutionServce = institutionServce;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Siginin([FromBody] InstitutionsDTOs.CreateInstitutionDTO signinDTO)
        {
            return Ok(await _InstitutionServce.CreateInstitution(signinDTO));
        }


        //get inst info
        [HttpGet("")]
        public async Task<ActionResult<InstitutionsDTOs.SendInstitutionDTO>?> GetInstitution()
        {
            int UserId = clsToken.GetUserID(HttpContext);
            var inst =  await _InstitutionServce.GetInstitutionAsync(UserId);
            if (inst == null) return NotFound();
            return Ok(inst);
        }



        //update inst info



        //delete inst
    }
}
