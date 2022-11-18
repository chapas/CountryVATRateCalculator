using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace CountryVATCalculator
{
    public class Program
    {
        private static string Application = "VAT rate calculator";
        private static string Version = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetName().Version.ToString();

        private static IConfigurationRoot _configuration;

        public static int Main(string[] args)
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration() 
                .ReadFrom.Configuration(_configuration)
                .Enrich.WithProperty(nameof(Application), Application)
                .Enrich.WithProperty(nameof(Version), Version)
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                Log.Information($"{Application} - {Version} - Starting...");
                CreateHostBuilder(args).Build().Run();
                Log.Information($"{Application} - {Version} -  Stopping...");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{Application} - {Version} Failed to start correctly!!");
                return -1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
