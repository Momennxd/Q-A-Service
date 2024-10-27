using Core.DTOs.Institution;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Institutions
{
    [Route("api/Institution")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        IInstitutionServce _InstitutionServce { get; set; }

        public InstitutionController(IInstitutionServce institutionServce)
        {
            _InstitutionServce = institutionServce;
        }

        [HttpPost("Siginin")]
        public async Task<IActionResult> Siginin(InstitutionsDTOs.SigninDTO signinDTO)
        {
            await _InstitutionServce.Siginin(signinDTO);
            return Ok();
        }
    }
}
