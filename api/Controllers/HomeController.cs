using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using ApiBase;
using ApiBase.Controllers;
using AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Controllers {
    [ApiController]
    [Route("/")]
    public class HomeController : BaseController {
        private readonly ILogger<AuthController> _logger;

        public HomeController(IConfiguration config, ILogger<AuthController> logger) : base(config) {
            _logger = logger;
        }


        [HttpGet("dhstnbblasydgt6as5dfs432")]
        public async Task<OkObjectResult> GetSettings() {
            string result = string.Empty;
            foreach (FieldInfo setting in typeof(ConfigurationKeys).GetFields()) {
                string settingKey = (string)setting.GetValue(setting)!;
                if (!(settingKey).Contains("secret")) {
                    result += $"{settingKey}: {Configuration[(string)setting.GetValue(setting)!]}{Environment.NewLine}";
                }
            }

            return await Task.FromResult<OkObjectResult>(Ok(result));
        }

    }
}
