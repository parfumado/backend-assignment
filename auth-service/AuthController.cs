using System;
using System.Threading.Tasks;
using ApiBase.Controllers;
using AuthService;
using DataAdapters.DocumentDb;
using DataAdapters.KeyValueDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.Controllers {
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : BaseController {

        private readonly ILogger<AuthController> _logger;
        private readonly IDocumentAdapter _documentAdapter;
        private readonly IKeyValueAdapter _keyValueAdapter;

        public AuthController(IConfiguration config, IDocumentAdapter documentAdapter, IKeyValueAdapter keyValueAdapter, ILogger<AuthController> logger)
            : base(config) {
            _keyValueAdapter = keyValueAdapter;
            _documentAdapter = documentAdapter;
            _logger = logger;
        }

        //TODO: Sign-in with username and password
        [HttpPost("credentials")]
        public async Task<IActionResult> Post([FromBody] UserRequest user) {
            throw new NotImplementedException();
        }

        //TODO: Signs-in with an expirable access token
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TokenRequest token) {
            throw new NotImplementedException();
        }

    }
}
