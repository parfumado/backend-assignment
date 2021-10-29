using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService {
    public class Startup {
        public static void Configure<T>(IApplicationBuilder app, IConfiguration configuration, bool isDevelopment) where T : class {

        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration) {

        }
    }
}