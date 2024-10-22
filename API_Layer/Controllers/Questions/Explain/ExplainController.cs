using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Questions.Explain
{
    [Route("api/Explain")]
    [ApiController]
    public class ExplainController : ControllerBase
    {


        [HttpPost]
        public async Task<IActionResult> AddExplain()
        {
            var result = 1;


            return Ok(result);
        }
    }
}
