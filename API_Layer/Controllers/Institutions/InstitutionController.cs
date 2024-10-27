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
        public async Task<IActionResult> Siginin(InstitutionsDTOs.CreateInstitutionDTO signinDTO)
        {
            return Ok(await _InstitutionServce.CreateInstitution(signinDTO));   
        }
    }
}
