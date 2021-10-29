using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiBase.Controllers {
    
    public abstract class BaseController : ControllerBase {
        public IConfiguration Configuration { get; init; }
        protected BaseController(IConfiguration configuration) {
            this.Configuration = configuration;
        }

        public async Task<IActionResult> SetAuthResult(object? authResult, string? sessionToken, DateTimeOffset? expiration = null) {
            if (authResult is null && sessionToken is null) {
                return await Task.FromResult<UnauthorizedResult>(Unauthorized());
            } else {
                Response.Cookies.Append(Configuration[ConfigurationKeys.AuthCookieName], sessionToken!, new CookieOptions {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    IsEssential = true,
                    Secure = Request.Scheme == "https",
                    Expires = expiration
                });

                return await Task.FromResult<OkObjectResult>(Ok(authResult));
            }
        }
    }
}