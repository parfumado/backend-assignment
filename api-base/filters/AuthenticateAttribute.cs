using System.Security.Claims;
using System.Threading.Tasks;
using ApiBase.Controllers;
using CommonServices.Models;
using DataAdapters.KeyValueDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace ApiBase.Attributes {
    //TODO: Implement authentication filter to decorate controllers that require authentication
    public class AuthenticateAttribute : ActionFilterAttribute {
        
    }
}