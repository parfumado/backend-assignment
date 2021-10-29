using System;
using System.Threading.Tasks;
using ApiBase;
using ApiBase.Attributes;
using ApiBase.Controllers;
using AuthService;
using DataAdapters.DocumentDb;
using DataAdapters.KeyValueDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CommonUser = CommonServices.Models.User;

namespace Api.Controllers {
    [ApiController]
    [Route("/api/user")]
    public class UserController : BaseController {
        private readonly ILogger<UserController> _logger;
        private readonly IDocumentAdapter _documentAdapter;
        private readonly IKeyValueAdapter _keyValueAdapter;

        public UserController(IConfiguration config, ILogger<UserController> logger, IDocumentAdapter documentAdapter, IKeyValueAdapter keyValueAdapter)
            : base(config) {
            _logger = logger;
            _documentAdapter = documentAdapter;
            _keyValueAdapter = keyValueAdapter;
        }

        //Get currently logged in user.
        [HttpGet, Authenticate]
        public async Task<OkObjectResult> Get() {
            return await Task.FromResult<OkObjectResult>(Ok(HttpContext.Items[ContextKeys.CommonUser] as CommonUser));
        }

        //Todo: Create user
        [HttpPost("credentials")]
        public async Task<IActionResult> PostCredentials([FromBody] UserCreate userCreateRequest) {
            throw new NotImplementedException();
        }

        //TODO: Update user
        [HttpPatch, Authenticate]
        public async Task<IActionResult> PatchUpdate([FromBody] UpdateUserRequest updateUser) {
            throw new NotImplementedException();
        }
        
        //TODO: Signout
        [HttpPost("signout")]
        public async Task<IActionResult> PostSignOut() {
            throw new NotImplementedException();
        }
    }
}