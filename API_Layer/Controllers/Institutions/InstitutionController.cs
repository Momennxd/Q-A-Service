//using API_Layer.Security;
//using Core.DTOs.Institution;
//using Core.Services.Concrete.Institutions;
//using Core.Services.Concrete.Users;
//using Core.Services.Interfaces;
//using Data.models.Institutions;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using static Core.DTOs.People.UsersDTOs;

//namespace API_Layer.Controllers.Institutions
//{
//    [Route("api/institutions")]
//    [ApiController]
//    public class InstitutionController : ControllerBase
//    {
//        IInstitutionServce _InstitutionServce { get; set; }

//        public InstitutionController(IInstitutionServce institutionServce)
//        {
//            _InstitutionServce = institutionServce;
//        }

//        [HttpPost("signin")]
//        public async Task<IActionResult> Siginin([FromBody] InstitutionsDTOs.CreateInstitutionDTO signinDTO)
//        {
//            return Ok(await _InstitutionServce.CreateInstitution(signinDTO));
//        }


//        //get inst info
//        [HttpGet("")]
//        [Authorize]
//        public async Task<ActionResult<InstitutionsDTOs.SendInstitutionDTO>?> GetInstitution()
//        {

//            int UserId = clsToken.GetUserID(HttpContext);
//            var inst =  await _InstitutionServce.GetInstitutionAsync(UserId);
//            if (inst == null) return NotFound();
//            return Ok(inst);
//        }



//        //update inst info 
//        [HttpPatch]
//        [Authorize]
//        public async Task<ActionResult> UpdateInstitutionInfo(JsonPatchDocument<InstitutionsDTOs.PatchInstitutionDTO> UpdatedItem)
//        {
//            var ent = await _InstitutionServce.GetInstitutionByUserIDAsync(clsToken.GetUserID(HttpContext));
//            if (ent == null) return NotFound();
//            var user = await _InstitutionServce.PatchInst(UpdatedItem, ent.InstitutionID);
//            return Ok(user);
//        }


//        //delete inst
//    }
//}
