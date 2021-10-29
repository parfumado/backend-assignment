using System.Threading.Tasks;
using ApiBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Controllers {
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase {
        private readonly ILogger<HealthController> _logger;
        private readonly IConfiguration _config;

        public HealthController(ILogger<HealthController> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
        }

        [HttpGet, Route("/status"), Route("status")]
        public async Task<object> GetStatus() {
            return await Task.FromResult<OkObjectResult>(Ok(new { status = "Service is online", version = _config[ConfigurationKeys.ApiVersion] }));
        }

        [HttpGet, Route("/ready"), Route("ready")]
        public async Task<IActionResult> GetReadiness() {
            if (Program.IsReady) {
                return await Task.FromResult<OkObjectResult>(Ok(new { status = "Service is ready" }));
            } else {
                return await Task.FromResult<IActionResult>(UnprocessableEntity(new { status = "Service NOT ready" }));
            }
        }
    }
}
