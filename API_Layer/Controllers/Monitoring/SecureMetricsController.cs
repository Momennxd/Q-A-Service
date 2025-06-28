using API_Layer.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace API_Layer.Controllers.Monitoring
{
    [Route("api/secure-metrics")]
    [ApiController]
    [Authorize]
    public class SecureMetricsController : ControllerBase
    {
        [HttpGet]
        [CheckPermission(Permissions.CanSeeMetrises)]
        public async Task<IActionResult> GetMetrics()
        {
            Response.ContentType = "text/plain; version=0.0.4";
            await Metrics.DefaultRegistry.CollectAndExportAsTextAsync(HttpContext.Response.Body);
            return new EmptyResult();
        }
    }
}
