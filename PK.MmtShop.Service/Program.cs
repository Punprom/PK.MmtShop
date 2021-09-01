using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Formatting.Json;

namespace PK.MmtShop.Service
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {

            IConfigurationSection loggingSetting;
            var logDirectory = string.Empty;
            var logFile = string.Empty;
            var logFilePath = string.Empty;

            try
            {
                loggingSetting = Configuration.GetSection("LogProfile");
                logDirectory = loggingSetting.GetValue<string>("logPath");
                logFile = loggingSetting.GetValue<string>("logFile");
                logFilePath = Path.Combine(logDirectory, logFile);

                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .WriteTo.File(new JsonFormatter(), logFilePath, shared: true)
                    .CreateLogger();

                try
                {
                    Log.Information("Api Service has started.");
                    CreateHostBuilder(args).Build().Run();
                }
                catch (Exception ex)
                {
                    Log.Fatal($"Api service has stopped unexpectedly due to exception. Message: {ex.Message}", ex);
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initiate api service. Message {ex.Message} ");
                Console.WriteLine(ex.InnerException);

            }


            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.File(new JsonFormatter(), logFilePath, shared: true)
                .CreateLogger();





        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
