using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApiBase;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api {
    public class Program {
        public static bool IsReady = true;
        public static JsonSerializerOptions JsonSerializationDefaults;
        static Program() {
            JsonSerializerOptions options = (JsonSerializerOptions)typeof(JsonSerializerOptions)
                .GetField("s_defaultOptions",
                    System.Reflection.BindingFlags.Static |
                    System.Reflection.BindingFlags.NonPublic)!.GetValue(null)!;

            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new DateTimeConverter());
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            Program.JsonSerializationDefaults = options;
        }

        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();
            var life = host.Services.GetRequiredService<IHostApplicationLifetime>();
            life.ApplicationStopping.Register(() => {
                IsReady = false;
                Console.WriteLine("Application is shutting down...");
                //Grace period for last requests to finish
                Task.Delay(2000).Wait();
            });
            life.ApplicationStopped.Register(() => {
                Console.WriteLine("Application is shut down");
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder
                        //.UseUrls("http://0.0.0.0:5000", "https://localhost:5001")
                        .UseKestrel()
                        .UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostingContext, configuration) => {
                    configuration.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    configuration
                        .AddJsonFile(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "settings/api-settings.json"), optional: false, reloadOnChange: true)
                        .AddJsonFile(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "secrets/api-secrets.json"), optional: false, reloadOnChange: true)
                        .AddJsonFile(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "settings/auth-settings.json"), optional: false, reloadOnChange: true)
                        .AddJsonFile(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "secrets/auth-secrets.json"), optional: false, reloadOnChange: true);

                    configuration.AddEnvironmentVariables(prefix: "PLATFORM_");

                    IConfigurationRoot configurationRoot = configuration.Build();
                });
    }
}
